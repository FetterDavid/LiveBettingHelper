using LiveBettingHelper.Model;
using LiveBettingHelper.Services;
using LiveBettingHelper.Utilities;
using LiveBettingHelper.Views.Popups;
using System.Collections.ObjectModel;

namespace LiveBettingHelper.ViewModels
{
    public class LiveMatchesPageModel : BaseViewModel
    {
        /// <summary>
        /// Ez a command fut le ha kiválasztiml egy élő mecset
        /// </summary>
        public Command OnSelectLiveMatch { get; set; }
        /// <summary>
        /// A megfigyelt élő mecsek listája
        /// </summary>
        public ObservableCollection<LiveMatch> LiveMatches { get; set; } = new();
        private List<LiveMatch> _selectedMatches { get; set; } = new();

        public LiveMatchesPageModel()
        {
            OnSelectLiveMatch = new Command(OpenNewBetPopup);
        }

        /// <summary>
        /// Újra tölti a figyelt élő mecsek listáját
        /// </summary>
        public async Task ReloadDesiredLiveMatchesAsync()
        {
            List<LiveMatch> matches = await LiveMatchService.GetAllLiveFixturesAsync();
            _selectedMatches.Clear();
            List<Task> tasks = new List<Task>();
            foreach (LiveMatch match in matches)
            {
                tasks.Add(GetLiveMatchCheckingTaskAsync(match));
            }
            await Task.WhenAll(tasks);
            _selectedMatches = _selectedMatches.OrderBy(match => match.Date).ToList();
            CheckSelectedLiveMatchesForOpportunity();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                LiveMatches.Clear();
                foreach (LiveMatch match in _selectedMatches)
                {
                    LiveMatches.Add(match);
                }
            });
            IsBusy = false;
        }
        /// <summary>
        /// Egy mecs vizsgálatát adja vissza Task-ként, hogy aszinkron modon tudjunk vizsgáli egyszerre több mecset
        /// </summary>
        private async Task GetLiveMatchCheckingTaskAsync(LiveMatch match)
        {
            List<BetType> betTypes = match.GetPossibleBetTypes();
            match.RecommendedBetType = BetType.NoBet;
            if (betTypes.Count == 0) return;
            else
            {
                foreach (BetType betType in betTypes)
                {
                    double odds = await OddsService.GetOdsByBetType(match, betType);
                    if (odds > 0)
                    {
                        match.RecommendedBetType = betType;
                        match.RecommendedBetOdds = odds;
                        break;
                    }
                }
                if (match.RecommendedBetOdds == 0) return;
                if (match.RecommendedBetType == BetType.NoBet) match.RecommendedBetType = betTypes[0];

            }
            _selectedMatches.Add(match);
        }
        /// <summary>
        /// Megnézi hogy elérte-e már a minimum ods-ot a fogadás, ha igen küld egy értesítést
        /// </summary>
        private void CheckSelectedLiveMatchesForOpportunity()
        {
            foreach (LiveMatch liveMatch in _selectedMatches)
            {
                if (liveMatch.RecommendedBetOdds >= App.SettingsManager.MySettings.MinOdds)
                {
                    Static.CreateNotificationRequest(liveMatch.Id, $"{liveMatch.HomeTeamName} - {liveMatch.AwayTeamName}", $"{liveMatch.RecommendedBetType} : {liveMatch.RecommendedBetOdds}");
                }
            }
        }
        /// <summary>
        /// Megnyitja egy fogadás készítő popupot a kiválasztott élő meccsel
        /// </summary>
        private async void OpenNewBetPopup(object obj)
        {
            LiveMatch match = obj as LiveMatch;
            if (match == null) return;
            // fogadási tipus ellemörzése
            List<BetType> betTypes = match.GetPossibleBetTypes();
            if (betTypes.Count == 0)
            {
                App.PopupManager.ShowPopup(new InfoPopup("There are no suitable betting options."));
                return;
            }
            // odds ellenörzése
            double odds = 0;
            foreach (BetType betType in betTypes)
            {
                odds = await OddsService.GetOdsByBetType(match, betTypes[0]);
                if (odds > 0) break;
            }
            if (odds == 0)
            {
                App.PopupManager.ShowPopup(new InfoPopup("There is no available odds for the match."));
                return;
            }
            BetType bt = betTypes[0];
            PreBet preBet = App.MM.PreBetRepo.GetItem(x => x.FixtureId == match.Id && x.BettingType == bt);
            if (preBet == null)
            {
                App.Logger.Error("There is no available prebet for tha match.");
                return;
            }
            // fogadás létrehozzása
            Bet bet = new Bet
            {
                FixtureId = match.Id,
                LeagueId = match.LeagueId,
                LeagueName = match.LeagueName,
                LeagueCountry = match.LeagueCountry,
                LeagueSeason = match.LeagueSeason,
                HomeTeamId = match.HomeTeamId,
                HomeTeamName = match.HomeTeamName,
                AwayTeamId = match.AwayTeamId,
                AwayTeamName = match.AwayTeamName,
                Date = match.Date,
                Odds = odds,
                BetMinute = match.ElapsedTime,
                Probability = preBet.Probability,
                BetValue = App.SettingsManager.MySettings.DefaultBetStake,
                BettingType = preBet.BettingType
            };
            App.PopupManager.ShowPopup(new BetPopup(bet));
        }
    }
}

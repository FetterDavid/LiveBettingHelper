using LiveBettingHelper.Model;
using LiveBettingHelper.Repositories;
using LiveBettingHelper.Services;
using LiveBettingHelper.Utilities;
using System.Collections.ObjectModel;

namespace LiveBettingHelper.ViewModels
{
    public class LiveMatchesPageModel : BaseViewModel
    {
        public ObservableCollection<LiveMatch> LiveMatches { get; set; } = new();
        private List<LiveMatch> _selectedMatches { get; set; } = new();

        public async Task ReloadDesiredLiveMatches()
        {
            List<LiveMatch> matches = await LiveMatchService.GetAllLiveFixturesAsync();
            _selectedMatches.Clear();
            List<Task> tasks = new List<Task>();
            foreach (LiveMatch match in matches)
            {
                tasks.Add(GetLiveMatchCheckingTask(match));
            }
            await Task.WhenAll(tasks);
            _selectedMatches = _selectedMatches.OrderBy(match => match.Date).ToList();
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

        private async Task GetLiveMatchCheckingTask(LiveMatch match)
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
    }
}

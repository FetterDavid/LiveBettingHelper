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
        private BaseRepository<PreBet> _preBetRepo;

        public LiveMatchesPageModel(BaseRepository<PreBet> preBetRepo)
        {
            this._preBetRepo = preBetRepo;
        }

        public async Task ReloadDesiredLiveMatches()
        {
            List<LiveMatch> matches = await LiveMatchService.GetAllLiveFixturesAsync();
            List<LiveMatch> selectedMatches = new();
            foreach (LiveMatch match in matches)
            {
                //PreBet preBet = _preBetRepo.GetItem(x => x.FixtureId == match.Id);
                //if (preBet == null) continue;
                //if (preBet.BetType == BetType.FirstHalfOver && match.ElapsedTime > 45) continue;
                //if (preBet.BetType == BetType.SecondHalfOver && match.ElapsedTime <= 45) continue;
                //if (match.ElapsedTime < 50) continue;
                //if (match.HomeTeamGoals == 0 && match.AwayTeamGoals == 0)
                //{
                match.Odds = await OddsService.GetOverSecondHalfOddAsync(match);
                //if (match.Odds == 0) continue;
                selectedMatches.Add(match);
                //}
            }
            selectedMatches = selectedMatches.OrderByDescending(match => match.ElapsedTime).ToList();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                LiveMatches.Clear();
                foreach (LiveMatch match in selectedMatches)
                {
                    LiveMatches.Add(match);
                }
            });
            IsBusy = false;
        }
    }
}

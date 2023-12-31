using LiveBettingHelper.Model;
using LiveBettingHelper.Repositories;
using LiveBettingHelper.Services;
using LiveBettingHelper.Utilities;
using System.Collections.ObjectModel;

namespace LiveBettingHelper.ViewModels
{
    public class BetHistoryViewModel
    {
        public ObservableCollection<BetHistory> BetHistories { get; set; } = new();
        private BaseRepository<BetHistory> _betHistoryRepo;
        private BaseRepository<PreBet> _preBetRepo;

        public BetHistoryViewModel(BaseRepository<BetHistory> betHistoryRepo, BaseRepository<PreBet> preBetRepo)
        {
            this._betHistoryRepo = betHistoryRepo;
            this._preBetRepo = preBetRepo;
        }

        public async void CheckPreBetsAndLoad()
        {
            DateTime tmp = DateTime.Now.AddMinutes(-45);//TODO ez nem így lesz a fogadás csak megtett fogadás lehet
            List<PreBet> preBets = _preBetRepo.GetItems(x => x.Date < tmp);
            List<Task> tasks = new List<Task>();
            foreach (PreBet preBet in preBets)
            {
                tasks.Add(Task.Run(async () =>
                {
                    MatchResult result = await MatchResultService.GetMatchResByIdAsync(preBet.FixtureId, preBet.BetType);
                    if (result == null) return;
                    bool isWon = false;
                    switch (preBet.BetType)
                    {
                        case BetType.FirstHalfOver:
                            isWon = result.FirstHalfResult != (0, 0);
                            break;
                        case BetType.SecondHalfOver:
                            isWon = result.FullTimeResult != result.FirstHalfResult;
                            break;
                        default:
                            break;
                    }
                    _betHistoryRepo.AddItem(Static.ConvertPreBetToBetHistory(preBet, isWon));
                    _preBetRepo.DeleteItem(preBet);
                }));
            }
            await Task.WhenAll(tasks);
            LoadBetHistories();
        }

        private void LoadBetHistories()
        {
            BetHistories.Clear();
            List<BetHistory> bets = _betHistoryRepo.GetItems();
            foreach (BetHistory bet in bets)
            {
                BetHistories.Add(bet);
            }
        }
    }
}

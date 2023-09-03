using LiveBettingHelper.Model;
using LiveBettingHelper.Repositories;
using LiveBettingHelper.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.ViewModels
{
    public class BetHistoryPageModel
    {
        public ObservableCollection<BetHistory> BetHistories { get; set; } = new();
        private BaseRepository<BetHistory> _betHistoryRepo;
        private BaseRepository<PreBet> _preBetRepo;

        public BetHistoryPageModel(BaseRepository<BetHistory> betHistoryRepo, BaseRepository<PreBet> preBetRepo)
        {
            this._betHistoryRepo = betHistoryRepo;
            this._preBetRepo = preBetRepo;
        }

        public async void CheckPreBetsAndLoad()
        {
            List<PreBet> preBets = _preBetRepo.GetItems().Where(x => x.Date.AddMinutes(45) < DateTime.Now).ToList();
            foreach (PreBet preBet in preBets)
            {
                MatchResult result = await ApiManager.GetMatchResByIdAsync(preBet.FixtureId,preBet.BetType);
                if (result == null) continue;
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
            }

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

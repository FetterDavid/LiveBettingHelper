using LiveBettingHelper.Services;
using LiveBettingHelper.Utilities;
using SQLite;

namespace LiveBettingHelper.Model
{
    public partial class Bet
    {
        [Ignore]
        public ImageSource StatusImg
        {
            get
            {
                if (!Finished) return ImageSource.FromFile("in_progress.png");
                if (Winned) return ImageSource.FromFile("win.png");
                return ImageSource.FromFile("lose.png");
            }
        }
        /// <summary>
        /// újra számolja a lehetséges nyereményt
        /// </summary>
        public void SetPossibleWinning()
        {
            PossibleWinning = Math.Round(BetValue * Odds, 0);
        }
        /// <summary>
        /// Megprobálja kiértékelni a mecset ha már vége, ekkor true-t adunk vissza elenkező esetben false-t
        /// </summary>
        public async Task<bool> TryDetermineOutcome()
        {
            if (Finished) return true;
            else if (await MatchResultService.IsMatchFinished(FixtureId))
            {
                Finished = true;
                Winned = await GetOutcome();
                if (Winned) App.BankManager.Deposit(PossibleWinning);
                App.MM.BetRepo.UpdateItem(this);
                return true;
            }
            else return false;
        }
        /// <summary>
        /// Vissza adja a fogadás eredménylt
        /// </summary>
        private async Task<bool> GetOutcome()
        {
            MatchResult result = await MatchResultService.GetMatchResultByIdAsync(FixtureId, BettingType);
            if (result == null) return false;
            bool isWon = false;
            switch (BettingType)
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
            return isWon;
        }
    }
}

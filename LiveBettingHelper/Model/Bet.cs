﻿using LiveBettingHelper.Services;
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
            else if (await MatchResultService.IsMatchFinishedOrOnHalfTime(FixtureId))
            {
                await DetermineOutcome();
                return true;
            }
            return false;
        }

        private async Task DetermineOutcome()
        {
            Finished = true;
            Winned = await MatchResultService.GetOutcome(FixtureId, BettingType);
            AddPossibleWinningsToTheBank();
            App.MM.BetRepo.UpdateItem(this);
        }

        private void AddPossibleWinningsToTheBank()
        {
            if (Winned)
            {
                App.BankManager.Deposit(PossibleWinning);
                App.BankManager.AddBankTransactionRecord(Math.Round(BetValue * Odds, 0));
            }
            else App.BankManager.AddBankTransactionRecord(BetValue * -1);
        }
    }
}

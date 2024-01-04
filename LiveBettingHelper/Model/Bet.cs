using CommunityToolkit.Mvvm.ComponentModel;

namespace LiveBettingHelper.Model
{
    public partial class Bet
    {
        [ObservableProperty]
        private double _possibleWinning;

        public void SetPossibleWinning()
        {
            PossibleWinning = Math.Round(BetValue * Odds, 0);
        }
    }
}

using SQLite;

namespace LiveBettingHelper.Model
{
    public partial class BetHistory : BetBase
    {
        public bool IsWon { get; set; }

        [Ignore]
        public ImageSource IsWonImage => IsWon ? ImageSource.FromFile("win.png") : ImageSource.FromFile("lose.png");
    }
}

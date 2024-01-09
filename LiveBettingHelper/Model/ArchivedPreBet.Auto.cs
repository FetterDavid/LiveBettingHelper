using SQLite;

namespace LiveBettingHelper.Model
{
    public partial class ArchivedPreBet : PreBet
    {
        public bool IsWon { get; set; }

        [Ignore]
        public ImageSource IsWonImage => IsWon ? ImageSource.FromFile("win.png") : ImageSource.FromFile("lose.png");
    }
}

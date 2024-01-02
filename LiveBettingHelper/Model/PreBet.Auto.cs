using SQLite;

namespace LiveBettingHelper.Model
{
    public partial class PreBet : BetBase
    {
        public DateTime Date { get; set; }

        [Ignore]
        public string StartStr => Date.ToString("HH:mm");
    }
}

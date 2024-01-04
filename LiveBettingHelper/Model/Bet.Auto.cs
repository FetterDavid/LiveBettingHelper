namespace LiveBettingHelper.Model
{
    public partial class Bet : BetBase
    {
        public DateTime Date { get; set; }
        public double Odds { get; set; }
        public double BetValue { get; set; }
    }
}

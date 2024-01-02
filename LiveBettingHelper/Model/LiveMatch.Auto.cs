namespace LiveBettingHelper.Model
{
    public partial class LiveMatch : MatchBase
    {
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
        public double ElapsedTime { get; set; }
        public double Odds { get; set; }
        public (int, int) FirstHalfResult { get; set; } = (0, 0);
    }
}

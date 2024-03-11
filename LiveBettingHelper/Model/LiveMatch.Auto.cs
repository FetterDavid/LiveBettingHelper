using LiveBettingHelper.Utilities;

namespace LiveBettingHelper.Model
{
    public partial class LiveMatch : MatchBase
    {
        public int? HomeTeamGoals { get; set; }
        public int? AwayTeamGoals { get; set; }
        public int ElapsedTime { get; set; }
        public (int?, int?) FirstHalfResult { get; set; } = (0, 0);
        public BetType RecommendedBetType { get; set; }
        public double RecommendedBetOdds { get; set; }

    }
}

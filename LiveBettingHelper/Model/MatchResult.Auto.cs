using LiveBettingHelper.Abstractions;

namespace LiveBettingHelper.Model
{
    public partial class MatchResult : ApiBaseModel
    {
        public (int, int) FirstHalfResult { get; set; }
        public (int, int) FullTimeResult { get; set; }
    }
}

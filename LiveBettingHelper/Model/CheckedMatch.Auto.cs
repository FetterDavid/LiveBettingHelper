using LiveBettingHelper.Abstractions;

namespace LiveBettingHelper.Model
{
    public partial class CheckedMatch : LocalBaseModel
    {
        public int FixtureId { get; set; }
        public DateTime CheckDate { get; set; }
    }
}

using LiveBettingHelper.Abstractions;

namespace LiveBettingHelper.Model
{
    public partial class Settings : LocalBaseModel
    {
        public double DefaultBetStake { get; set; }
        public double SelectionSystemMinProbability { get; set; }
        public double MinOdds { get; set; }

    }
}

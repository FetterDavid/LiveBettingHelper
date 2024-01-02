using SQLite;

namespace LiveBettingHelper.Model
{
    [Table("prematch")]
    public partial class PreMatch : MatchBase
    {
        public double HomeTeamFHOverPercent { get; set; }
        public double HomeTeamSHOverPercent { get; set; }
        public double AwayTeamFHOverPercent { get; set; }
        public double AwayTeamSHOverPercent { get; set; }
        public DateTime Date { get; set; }
    }
}

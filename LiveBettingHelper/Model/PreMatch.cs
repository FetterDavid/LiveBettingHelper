using LiveBettingHelper.Abstractions;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Model
{
    [Table("prematch")]
    public class PreMatch : MatchBase
    {
        public double HomeTeamFHOverPercent { get; set; }
        public double HomeTeamSHOverPercent { get; set; }
        public double AwayTeamFHOverPercent { get; set; }
        public double AwayTeamSHOverPercent { get; set; }
        public DateTime Date { get; set; }
    }
}

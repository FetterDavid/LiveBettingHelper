using LiveBettingHelper.Abstractions;
using LiveBettingHelper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Model
{
    public class BetBase : LocalBaseModel
    {
        public BetType BetType { get; set; }
        public double Probability { get; set; }
        public int FixtureId { get; set; }
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public string LeagueCountry { get; set; }
        public int LeagueSeason { get; set; }
        public int HomeTeamId { get; set; }
        public string HomeTeamName { get; set; }
        public int AwayTeamId { get; set; }
        public string AwayTeamName { get; set; }
    }
}

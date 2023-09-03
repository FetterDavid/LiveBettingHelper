using LiveBettingHelper.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Model
{
    public class LiveMatch : MatchBase
    {
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
        public double ElapsedTime { get; set; }
        public double Odds { get; set; }
        public (int, int) FirstHalfResult { get; set; } = (0, 0);
    }
}

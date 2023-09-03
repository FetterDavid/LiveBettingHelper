using LiveBettingHelper.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Model
{
    public class MatchResult : ApiBaseModel
    {
        public (int, int) FirstHalfResult { get; set; }
        public (int, int) FullTimeResult { get; set; }
    }
}

using LiveBettingHelper.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Model
{
    public class CheckedMatch : LocalBaseModel
    {
        public int FixtureId { get; set; }
        public DateTime CheckDate { get; set; }
    }
}

using LiveBettingHelper.Abstractions;
using LiveBettingHelper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Model
{
    public class LastCheck : LocalBaseModel
    {
        public CheckType checkType { get; set; }
        public DateTime checkDate { get; set; }
    }
}

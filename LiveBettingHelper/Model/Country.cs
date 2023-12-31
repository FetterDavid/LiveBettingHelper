using LiveBettingHelper.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Model
{
    public class Country : LocalBaseModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}

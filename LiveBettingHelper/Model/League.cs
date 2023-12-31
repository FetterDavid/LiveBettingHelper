using LiveBettingHelper.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Model
{
    public class League : LocalBaseModel
    {
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string Type { get; set; }
    }
}

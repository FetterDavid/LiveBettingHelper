using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Model.ApiSchemas
{
    public class FixturesRootObject
    {
        public object[] errors { get; set; }
        public int results { get; set; }
        public FixtureObject[] response { get; set; }
    }
}

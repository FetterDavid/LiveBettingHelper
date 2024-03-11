using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Model.ApiSchemas
{
    public class Fixture
    {
        public int id { get; set; }
        public string referee { get; set; }
        public string timezone { get; set; }
        public DateTime date { get; set; }
        public int timestamp { get; set; }
        public Periods periods { get; set; }
        public Venue venue { get; set; }
        public Status status { get; set; }
    }
}

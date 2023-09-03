using LiveBettingHelper.Utilities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Model
{
    public class PreBet : BetBase
    {
        public DateTime Date { get; set; }

        [Ignore]
        public string StartStr => Date.ToString("HH:mm");
    }
}

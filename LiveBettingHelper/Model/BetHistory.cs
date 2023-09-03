using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Model
{
    public class BetHistory : BetBase
    {
        public bool IsWon { get; set; }

        [Ignore]
        public ImageSource IsWonImage => IsWon ? ImageSource.FromFile("win.png") : ImageSource.FromFile("lose.png");
    }
}

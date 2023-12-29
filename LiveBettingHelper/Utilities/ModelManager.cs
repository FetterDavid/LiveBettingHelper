using LiveBettingHelper.Model;
using LiveBettingHelper.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Utilities
{
    public class ModelManager
    {
        public BaseRepository<PreBet> PreBetRepo { get; set; } = new();
        public BaseRepository<BetHistory> BetHistoryRepo { get; set; } = new();
        public BaseRepository<CheckedMatch> CheckedMatchRepo { get; set; } = new();
        public LastCheckRepository LastCheckRepo { get; set; } = new();
    }
}

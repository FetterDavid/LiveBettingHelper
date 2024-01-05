using LiveBettingHelper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Model
{
    public partial class LiveMatch
    {
        public List<BetType> GetPossibleBetTypes()
        {
            List<BetType> betTypes = new();
            if (ElapsedTime < 45 && HomeTeamGoals == 0 && AwayTeamGoals == 0) betTypes.Add(BetType.FirstHalfOver);
            if (ElapsedTime > 45 && HomeTeamGoals == FirstHalfResult.Item1 && AwayTeamGoals == FirstHalfResult.Item2) betTypes.Add(BetType.SecondHalfOver);
            return betTypes;
        }
    }
}

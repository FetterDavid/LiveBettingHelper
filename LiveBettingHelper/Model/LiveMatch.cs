using LiveBettingHelper.Utilities;

namespace LiveBettingHelper.Model
{
    public partial class LiveMatch
    {
        public List<BetType> GetPossibleBetTypes()
        {
            List<BetType> betTypes = new();
            if (ElapsedTime < 45 && HomeTeamGoals == 0 && AwayTeamGoals == 0)//
                betTypes.Add(BetType.FirstHalfOver);
            if (ElapsedTime > 45 && HomeTeamGoals == FirstHalfResult.Item1 && AwayTeamGoals == FirstHalfResult.Item2)
                betTypes.Add(BetType.SecondHalfOver);
            return betTypes;
        }

        public bool CanBet(BetType betType)
        {
            if (App.MM.PreBetRepo.GetItem(x => x.FixtureId == Id && x.BettingType == betType) == null) return false; // Ha nincs ilyen betType-ú Prebet lementve a mecsről akkor nem lehet rá fogadni
            if (App.MM.BetRepo.GetItem(x => x.FixtureId == Id && x.BettingType == betType) != null) return false; // Ha már ilyen betType-ú Bet van lementve a mecsről akkor nem lehet rá fogadni újra
            switch (betType)
            {
                case BetType.FirstHalfOver:
                    if (ElapsedTime <= 25) return true;
                    break;
                case BetType.SecondHalfOver:
                    if (ElapsedTime <= 70) return true;
                    break;
                default:
                    return false;
            }
            return false;
        }
    }
}

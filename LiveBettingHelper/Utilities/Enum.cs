﻿namespace LiveBettingHelper.Utilities
{
    public enum BetType
    {
        FirstHalfOver = 'F',
        SecondHalfOver = 'S'
    }

    public enum CheckType
    {
        NextMatchesCheck = 'N',
        CountryCheck = 'C',
        LeagueCheck = 'L',
        CompletedBetsCheck = 'M'
    }

    public enum SelectType
    {
        Selected = 'S',
        NotSelected = 'N',
        PartiallySelected = 'P'
    }
}

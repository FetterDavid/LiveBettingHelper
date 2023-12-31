﻿using LiveBettingHelper.Model;
using LiveBettingHelper.Repositories;

namespace LiveBettingHelper.Utilities
{
    public class ModelManager
    {
        public BaseRepository<PreBet> PreBetRepo { get; set; } = new();
        public BaseRepository<BetHistory> BetHistoryRepo { get; set; } = new();
        public BaseRepository<CheckedMatch> CheckedMatchRepo { get; set; } = new();
        public BaseRepository<Country> CountryRepo { get; set; } = new();
        public BaseRepository<League> LeagueRepo { get; set; } = new();
        public LastCheckRepository LastCheckRepo { get; set; } = new();
    }
}

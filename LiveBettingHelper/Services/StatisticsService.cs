using LiveBettingHelper.Model;
using LiveBettingHelper.Utilities;
using Newtonsoft.Json;

namespace LiveBettingHelper.Services
{
    public static class StatisticsService
    {
        /// <summary>
        /// Vissza adja az adott mecs csapatai közötti tablella távolságot
        /// </summary>
        public static async Task<int> GetRankDifferenceAsync(LiveMatch match)
        {
            string json = await GetStandingsJsonAsync(match.LeagueId, match.LeagueSeason);
            if (string.IsNullOrEmpty(json))
            {
                App.Logger.Error("No data received from the API.");
                return 0;
            }
            try
            {
                int homeTeamRank = 0;
                int awayTeamRank = 0;
                dynamic data = JsonConvert.DeserializeObject(json);
                foreach (var response in data.response)
                {
                    foreach (var standings in response.league.standings)
                    {
                        foreach (var standing in standings)
                        {
                            if (standing.team.id == match.HomeTeamId) homeTeamRank = standing.rank;
                            if (standing.team.id == match.AwayTeamId) awayTeamRank = standing.rank;
                        }

                    }
                }

                return Math.Abs(homeTeamRank - awayTeamRank);
            }
            catch (JsonException ex)
            {
                App.Logger.Exception(ex, "JSON deserialization error: ");
                return 0;
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex);
                return 0;
            }
        }
        /// <summary>
        /// Vissza adja egy csapat 1. és 2. félidő over százalékát "(double, double)" formában
        /// </summary>
        public static async Task<(double, double)> GetFirstAndSecondHalfPercentAsync(int leagueId, int season, int teamId, string side)
        {
            string json = await GetPrevMatchesJsonAsync(leagueId, season, teamId);
            if (string.IsNullOrEmpty(json))
            {
                App.Logger.Error("No data received from the API.");
                return (0, 0);
            }
            try
            {
                dynamic data = JsonConvert.DeserializeObject(json);
                int firstHalfGoalCount = 0;
                int secondHalfGoalCount = 0;
                int matchCount = 0;
                foreach (var response in data.response)
                {
                    MatchStatus status = Enum.TryParse((string)response["fixture"]["status"]["short"], out MatchStatus stat) ? stat : MatchStatus.Error;
                    if (Static.IsFinishingStatus(status) == false) continue;
                    if (response["teams"][side]["id"] == teamId)
                    {
                        int homeFHG = response["score"]["halftime"]["home"] == null ? 0 : response["score"]["halftime"]["home"];
                        int awayFHG = response["score"]["halftime"]["away"] == null ? 0 : response["score"]["halftime"]["away"];
                        int homeFTG = response["score"]["fulltime"]["home"] == null ? 0 : response["score"]["fulltime"]["home"];
                        int awayFTG = response["score"]["fulltime"]["away"] == null ? 0 : response["score"]["fulltime"]["away"];
                        int homeSTG = homeFTG - homeFHG;
                        int awaySTG = awayFTG - awayFHG;
                        firstHalfGoalCount += homeFHG + awayFHG;
                        secondHalfGoalCount += homeSTG + awaySTG;
                        matchCount++;
                    }
                }
                if (matchCount < 8) return (0, 0);
                double firstHalf = Math.Round((1 - PoissonDistribution.PoissonPMF((double)firstHalfGoalCount / matchCount, 0)) * 100, 0);
                double secondHalf = Math.Round((1 - PoissonDistribution.PoissonPMF((double)secondHalfGoalCount / matchCount, 0)) * 100, 0);
                return (firstHalf, secondHalf);
            }
            catch (JsonException ex)
            {
                App.Logger.Exception(ex, "JSON deserialization error: ");
                return (0, 0);
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex);
                return (0, 0);
            }
        }
        /// <summary>
        /// Vissza adja egy liga standing json-jét
        /// </summary>
        private static async Task<string> GetStandingsJsonAsync(int league, int season)
        {
            return await ApiManager.RequestJsonAsync($"standings?league={league}&season={season}");
        }
        /// <summary>
        /// Vissza adja a csapat előző mecsei json-jét az adott ligában és szezonban
        /// </summary>
        private static async Task<string> GetPrevMatchesJsonAsync(int league, int season, int team)
        {
            return await ApiManager.RequestJsonAsync($"fixtures?league={league}&season={season}&team={team}");
        }
    }
}

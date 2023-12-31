﻿

using LiveBettingHelper.Model;
using LiveBettingHelper.Utilities;
using Newtonsoft.Json;

namespace LiveBettingHelper.Services
{
    public static class PreMatchService
    {
        /// <summary>
        /// Vissza adja az adott nap mecseit
        /// </summary>
        public static async Task<List<PreMatch>> GetMatchesByDateAsync(DateTime date)
        {
            string json = await GetMatchesJsonBydateAsync(date);
            if (string.IsNullOrEmpty(json))
            {
                App.Logger.Error("No data received from the API.");
                return [];
            }
            try
            {
                dynamic data = JsonConvert.DeserializeObject(json);
                List<PreMatch> matches = new List<PreMatch>();
                foreach (var response in data.response)
                {
                    var match = new PreMatch
                    {
                        Id = response["fixture"]["id"],
                        LeagueId = response["league"]["id"],
                        LeagueName = response["league"]["name"],
                        LeagueCountry = response["league"]["country"],
                        LeagueSeason = response["league"]["season"],
                        HomeTeamId = response["teams"]["home"]["id"],
                        HomeTeamName = response["teams"]["home"]["name"],
                        AwayTeamId = response["teams"]["away"]["id"],
                        AwayTeamName = response["teams"]["away"]["name"],
                        Date = response["fixture"]["date"]
                    };
                    matches.Add(match);
                }
                return matches;
            }
            catch (JsonException ex)
            {
                App.Logger.Exception(ex, "JSON deserialization error: ");
                return [];
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex);
                return [];
            }
        }
        /// <summary>
        /// Vissza adja a következő 24 óra összes mecsét
        /// </summary>
        public static async Task<List<PreMatch>> GetNext24HourMatchesAsync()
        {
            List<PreMatch> nextMatches = new();
            List<PreMatch> todayMatches = await GetMatchesByDateAsync(DateTime.Now);
            List<PreMatch> tomorrowMatches = await GetMatchesByDateAsync(DateTime.Now.AddDays(1));
            nextMatches.AddRange(todayMatches.Where(x => x.Date > DateTime.Now));
            nextMatches.AddRange(tomorrowMatches.Where(x => x.Date < DateTime.Now.AddDays(1)));
            return nextMatches;
        }
        /// <summary>
        /// Vissza adj az adott nap összes meccsének a json-jét
        /// </summary>
        private static async Task<string> GetMatchesJsonBydateAsync(DateTime date)
        {
            return await ApiManager.RequestJsonAsync($"fixtures?date={date.ToString("yyyy-MM-dd")}&timezone=Europe%2FBudapest");
        }
    }
}
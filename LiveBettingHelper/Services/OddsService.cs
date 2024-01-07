using LiveBettingHelper.Model;
using LiveBettingHelper.Utilities;
using Newtonsoft.Json;

namespace LiveBettingHelper.Services
{
    public static class OddsService
    {
        /// <summary>
        /// Vissza adja az adott mecs 1. félidő over ods-at
        /// </summary>
        public static async Task<double> GetOverFirstHalfOddAsync(int fixtureId)
        {
            string json = await GetOverFirstHalfOdsJsonAsync(fixtureId);
            if (string.IsNullOrEmpty(json))
            {
                App.Logger.Error("No data received from the API.");
                return 0;
            }
            try
            {
                dynamic data = JsonConvert.DeserializeObject(json);
                foreach (var response in data.response)
                {
                    foreach (var odd in response.odds)
                    {
                        foreach (var value in odd.values)
                        {
                            if (value.value == "Over" && value.handicap == 0.5)
                            {
                                return value.odd;
                            }
                        }

                    }
                }
                return 0;
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
        /// Vissza adja az adott mecs 2. félidő over ods-at
        /// </summary>
        public static async Task<double> GetOverSecondHalfOddAsync(LiveMatch liveMatch)
        {
            string json = await GetOverSecondtHalfOdsJsonAsync(liveMatch.Id);
            if (string.IsNullOrEmpty(json))
            {
                App.Logger.Error("No data received from the API.");
                return 0;
            }
            try
            {
                dynamic data = JsonConvert.DeserializeObject(json);
                foreach (var response in data.response)
                {
                    foreach (var odd in response.odds)
                    {
                        foreach (var value in odd.values)
                        {
                            if (value.value == "Over" && value.handicap == (liveMatch.FirstHalfResult.Item1 + liveMatch.FirstHalfResult.Item2 + 0.5))
                            {
                                return value.odd;
                            }
                        }

                    }
                }
                return 0;
            }
            catch (JsonException ex)
            {
                App.Logger.Exception(ex, "JSON deserialization error:");
                return 0;
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex);
                return 0;
            }
        }
        /// <summary>
        /// Vissza adja az adott mecshez tartozó odds-ot a megadott fogadási tipus alapján 
        /// </summary>
        public static async Task<double> GetOdsByBetType(LiveMatch liveMatch, BetType betType)
        {
            switch (betType)
            {
                case BetType.FirstHalfOver:
                    return await GetOverFirstHalfOddAsync(liveMatch.Id);
                case BetType.SecondHalfOver:
                    return await GetOverSecondHalfOddAsync(liveMatch);
                default:
                    throw new Exception("Unknown bet type");
            }
        }
        /// <summary>
        /// Vissza adja az első félidő over Odd json-jét
        /// </summary>
        /// <returns>Ha nem elérhető ilyen odd akkor 0-val tér vissza</returns>
        private static async Task<string> GetOverFirstHalfOdsJsonAsync(int fixtureId)
        {
            return await ApiManager.RequestJsonAsync($"odds/live?fixture={fixtureId}&bet=49");
        }
        /// <summary>
        /// Vissza adja a második félidő over Odd json-jét
        /// </summary>
        /// <returns>Ha nem elérhető ilyen odd akkor 0-val tér vissza</returns>
        private static async Task<string> GetOverSecondtHalfOdsJsonAsync(int fixtureId)
        {
            return await ApiManager.RequestJsonAsync($"odds/live?fixture={fixtureId}&bet=36");
        }
        /// <summary>
        /// Vissza adja egy meccs összes odds json-jét id alapján
        /// </summary>
        private static async Task<string> GetMatchOddJsonByIdAsync(int id)
        {
            return await ApiManager.RequestJsonAsync($"odds?fixture={id}");
        }
    }
}



using LiveBettingHelper.Model;
using LiveBettingHelper.Utilities;
using Newtonsoft.Json;

namespace LiveBettingHelper.Services
{
    public static class LeaguesService
    {
        /// <summary>
        /// Vissza adja az összes ligát
        /// </summary>
        public static async Task<List<League>> GetAllLeaguesAsync()
        {
            string json = await GetAllLeaguesJsonAsync();
            if (string.IsNullOrEmpty(json))
            {
                App.Logger.Error("No data received from the API.");
                return [];
            }
            try
            {
                dynamic data = JsonConvert.DeserializeObject(json);
                List<League> leagues = new List<League>();
                foreach (var response in data.response)
                {
                    var league = new League
                    {
                        Name = response["league"]["name"],
                        CountryCode = response["country"]["code"],
                        Type = response["league"]["type"]
                    };
                    leagues.Add(league);
                }
                return leagues;
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
        /// Vissza adja az összes ligát
        /// </summary>
        public static async Task<List<League>> GetLeaguesByCountryCodeAsync(string countryCode)
        {
            string json = await GetLeaguesJsonByCountryCodeAsync(countryCode);
            if (string.IsNullOrEmpty(json))
            {
                App.Logger.Error("No data received from the API.");
                return [];
            }
            try
            {
                dynamic data = JsonConvert.DeserializeObject(json);
                List<League> leagues = new List<League>();
                foreach (var response in data.response)
                {
                    var league = new League
                    {
                        Name = response["league"]["name"],
                        CountryCode = response["country"]["code"],
                        Type = response["league"]["type"]
                    };
                    leagues.Add(league);
                }
                return leagues;
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
        /// Vissza adja az összes liga json-jét
        /// </summary>
        private static async Task<string> GetAllLeaguesJsonAsync()
        {
            return await ApiManager.RequestJsonAsync("leagues");
        }
        /// <summary>
        /// Vissza adja országkód alapján a ligál json-jét
        /// </summary>
        private static async Task<string> GetLeaguesJsonByCountryCodeAsync(string countryCode)
        {
            return await ApiManager.RequestJsonAsync($"leagues?code={countryCode}");
        }
    }
}



using LiveBettingHelper.Model;
using LiveBettingHelper.Utilities;
using Newtonsoft.Json;

namespace LiveBettingHelper.Services
{
    public static class CountryService
    {
        /// <summary>
        /// Vissza adja az összes országot
        /// </summary>
        public static async Task<List<Country>> GetAllCountriesAsync()
        {
            string json = await GetAllCountriesJsonAsync();
            if (string.IsNullOrEmpty(json))
            {
                App.Logger.Error("No data received from the API.");
                return [];
            }
            try
            {
                dynamic data = JsonConvert.DeserializeObject(json);
                List<Country> countries = new List<Country>();
                foreach (var response in data.response)
                {
                    var country = new Country
                    {
                        Name = response["name"],
                        Code = response["code"]
                    };
                    countries.Add(country);
                }
                return countries;
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
        /// Vissza adja az összes ország json-jét
        /// </summary>
        private static async Task<string> GetAllCountriesJsonAsync()
        {
            return await ApiManager.RequestJsonAsync("countries");
        }
    }
}

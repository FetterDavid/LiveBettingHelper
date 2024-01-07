using LiveBettingHelper.Model;
using Newtonsoft.Json;

namespace LiveBettingHelper.Utilities
{
    /// <summary>
    /// [ NE HASZNÁLD!!!!! ] A "Football Betting Odds" Api-t használja
    /// </summary>
    public class OddsManager
    {
        public string OddsJson;


        public async Task UpdateOddsJson()
        {
            OddsJson = await GetAllLiveMatchOddsJsonAsync();
        }

        /// <summary>
        /// Vissza adja az adott mecs 2. félidő over ods-at
        /// </summary>
        public double GetOverSecondHalfOddAsync(LiveMatch liveMatch)
        {
            if (string.IsNullOrEmpty(OddsJson))
            {
                App.Logger.Error("No data received from the API.");
                return 0;
            }
            try
            {
                dynamic data = JsonConvert.DeserializeObject(OddsJson);
                foreach (var item in data)
                {
                    string home = item.Value["home"];
                    string away = item.Value["away"];
                    if (home.ToLower().Replace(" fc", "") == liveMatch.HomeTeamName.ToLower().Replace(" fc", "")
                        && away.ToLower().Replace(" fc", "") == liveMatch.AwayTeamName.ToLower().Replace(" fc", ""))
                        if (item.Value["odds"]["2nd-over-0-5"] != null)
                            return item.Value["odds"]["2nd-over-0-5"];
                        else return 0;
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
        /// Vissza adja az adott mecs 1. félidő over ods-at
        /// </summary>
        public double GetOverFirstHalfOddAsync(LiveMatch liveMatch)
        {
            if (string.IsNullOrEmpty(OddsJson))
            {
                App.Logger.Error("No data received from the API.");
                return 0;
            }
            try
            {
                dynamic data = JsonConvert.DeserializeObject(OddsJson);
                foreach (var item in data)
                {
                    string home = item.Value["home"];
                    string away = item.Value["away"];
                    if (home.ToLower().Replace(" fc", "") == liveMatch.HomeTeamName.ToLower().Replace(" fc", "")
                        && away.ToLower().Replace(" fc", "") == liveMatch.AwayTeamName.ToLower().Replace(" fc", ""))
                        if (item.Value["odds"]["1st-over-0-5"] != null)
                            return item.Value["odds"]["1st-over-0-5"];
                        else return 0;
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
        /// Vissza adja az adott mecshez tartozó odds-ot a megadott fogadási tipus alapján 
        /// </summary>
        public double GetOdsByBetType(LiveMatch liveMatch, BetType betType)
        {
            switch (betType)
            {
                case BetType.FirstHalfOver:
                    return GetOverFirstHalfOddAsync(liveMatch);
                case BetType.SecondHalfOver:
                    return GetOverSecondHalfOddAsync(liveMatch);
                default:
                    throw new Exception("Unknown bet type");
            }
        }
        /// <summary>
        /// Vissza adja az öszzes élő mecset és a hozzá tartozó odds-ok json-ját
        /// </summary>
        public static async Task<string> GetAllLiveMatchOddsJsonAsync()
        {
            return await ApiManager.RequestJsonFromOddsAsync($"provider1/live/inplaying");
        }
    }
}

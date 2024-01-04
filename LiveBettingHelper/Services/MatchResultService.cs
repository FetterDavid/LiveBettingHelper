

using LiveBettingHelper.Model;
using LiveBettingHelper.Utilities;
using Newtonsoft.Json;

namespace LiveBettingHelper.Services
{
    public static class MatchResultService
    {
        /// <summary>
        /// Vissza adja egy mecs eredményét id alapján
        /// </summary>
        public static async Task<MatchResult> GetMatchResultByIdAsync(int id, BetType betType)
        {
            string json = await GetMatcheJsonByIdAsync(id);
            if (string.IsNullOrEmpty(json))
            {
                App.Logger.Error("No data received from the API.");
                return null;
            }
            try
            {
                dynamic data = JsonConvert.DeserializeObject(json);
                if (data["response"] == null) return null;
                MatchResult res = null;
                if (betType == BetType.FirstHalfOver)
                {
                    if (data["response"][0]["score"]["halftime"]["home"] == null) return null;
                    res = new MatchResult
                    {
                        Id = data["response"][0]["fixture"]["id"],
                        FirstHalfResult = (data["response"][0]["score"]["halftime"]["home"], data["response"][0]["score"]["halftime"]["away"]),
                        FullTimeResult = (-1, -1)
                    };
                }
                else if (betType == BetType.SecondHalfOver)
                {
                    if (data["response"][0]["score"]["fulltime"]["home"] == null) return null;
                    res = new MatchResult
                    {
                        Id = data["response"][0]["fixture"]["id"],
                        FirstHalfResult = (data["response"][0]["score"]["halftime"]["home"], data["response"][0]["score"]["halftime"]["away"]),
                        FullTimeResult = (data["response"][0]["score"]["fulltime"]["home"], data["response"][0]["score"]["fulltime"]["away"])
                    };
                }
                return res;
            }
            catch (JsonException ex)
            {
                App.Logger.Exception(ex, "JSON deserialization error: ");
                return null;
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex);
                return null;
            }
        }
        /// <summary>
        /// Vissza adja egy mecs státuszát id alapján
        /// </summary>
        public static async Task<MatchStatus> GetMatchStatus(int id)
        {
            string json = await GetMatcheJsonByIdAsync(id);
            if (string.IsNullOrEmpty(json))
            {
                App.Logger.Error("No data received from the API.");
                return MatchStatus.Error;
            }
            try
            {
                dynamic data = JsonConvert.DeserializeObject(json);
                if (data["response"] == null) return MatchStatus.Error;
                MatchResult res = null;
                string statusCode = data["response"][0]["fixture"]["status"]["short"];
                if (Enum.TryParse(statusCode, out MatchStatus status)) return status;
                else return MatchStatus.Error;
            }
            catch (JsonException ex)
            {
                App.Logger.Exception(ex, "JSON deserialization error: ");
                return MatchStatus.Error;
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex);
                return MatchStatus.Error;
            }
        }
        /// <summary>
        /// Vissza adja hogy vége van-e egy mecsnek id alapján
        /// </summary>
        public static async Task<bool> IsMatchFinished(int id)
        {
            MatchStatus status = await GetMatchStatus(id);
            switch (status)
            {
                case MatchStatus.FT:
                case MatchStatus.AET:
                case MatchStatus.FT_PEN:
                case MatchStatus.WO:
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>
        /// Vissza ad egy meccs json-jét id alapján
        /// </summary>
        private static async Task<string> GetMatcheJsonByIdAsync(int id)
        {
            return await ApiManager.RequestJsonAsync($"fixtures?id={id}");
        }
    }
}

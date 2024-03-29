﻿

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
                        Id = (int)data["response"][0]["fixture"]["id"],
                        FirstHalfResult = ((int)data["response"][0]["score"]["halftime"]["home"], (int)data["response"][0]["score"]["halftime"]["away"]),
                        FullTimeResult = ((int)data["response"][0]["score"]["fulltime"]["home"], (int)data["response"][0]["score"]["fulltime"]["away"])
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
                string statusCode = (string)data["response"][0]["fixture"]["status"]["short"];
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
            return Static.IsFinishingStatus(status);
        }
        /// <summary>
        /// Vissza adja hogy féidő van-e az adott mecsen id alapján
        /// </summary>
        public static async Task<bool> IsMatchOnHalfTime(int id)
        {
            MatchStatus status = await GetMatchStatus(id);
            return status == MatchStatus.HT;
        }
        /// <summary>
        /// Vissza adja hogy vége van-e vagy féidő van-e az adott mecsen id alapján
        /// </summary>
        public static async Task<bool> IsMatchFinishedOrOnHalfTime(int id)
        {
            MatchStatus status = await GetMatchStatus(id);
            return (status == MatchStatus.HT || Static.IsFinishingStatus(status));
        }
        /// <summary>
        /// Vissza adja a fogadás eredménylt
        /// </summary>
        public static async Task<bool> GetOutcome(int fixtureId, BetType betType)
        {
            MatchResult result = await GetMatchResultByIdAsync(fixtureId, betType);
            if (result == null) return false;
            bool isWon = false;
            switch (betType)
            {
                case BetType.FirstHalfOver:
                    isWon = result.FirstHalfResult != (0, 0);
                    break;
                case BetType.SecondHalfOver:
                    isWon = result.FullTimeResult != result.FirstHalfResult;
                    break;
                default:
                    break;
            }
            return isWon;
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

using LiveBettingHelper.Model;
using LiveBettingHelper.Utilities;
using Newtonsoft.Json;

namespace LiveBettingHelper.Services
{
    public static class LiveMatchService
    {
        /// <summary>
        /// Vissza adja az élő mecseket
        /// </summary>
        public static async Task<List<LiveMatch>> GetAllLiveFixturesAsync()
        {
            string json = await GetLiveMatchesJsonAsync();
            if (string.IsNullOrEmpty(json))
            {
                App.Logger.Error("No data received from the API.");
                return [];
            }
            try
            {
                dynamic data = JsonConvert.DeserializeObject(json);
                List<LiveMatch> liveMatches = new List<LiveMatch>();
                foreach (var response in data.response)
                {
                    var match = new LiveMatch
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
                        HomeTeamGoals = response["goals"]["home"],
                        AwayTeamGoals = response["goals"]["away"],
                        ElapsedTime = response["fixture"]["status"]["elapsed"]
                    };
                    if (response["score"]["halftime"]["home"] != null)
                        match.FirstHalfResult = (response["score"]["halftime"]["home"], response["score"]["halftime"]["away"]);
                    liveMatches.Add(match);
                }
                return liveMatches;
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
        /// Vissza adja az élő mecsek lekérdezés json-jét
        /// </summary>
        private static async Task<string> GetLiveMatchesJsonAsync()
        {
            return await ApiManager.RequestJsonAsync("fixtures?live=all");
        }
    }
}

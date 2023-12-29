using LiveBettingHelper.Model;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Timers;

namespace LiveBettingHelper.Utilities
{
    public static class ApiManager
    {
        private const int MAX_REQUESTS_PER_MINUTE = 450;
        public static bool CanRequest => _requestsInLastMinute < MAX_REQUESTS_PER_MINUTE;
        private static int _requestsInLastMinute;
        private static DateTime _requestCurrentMinute;
        private static System.Timers.Timer _requestLimitTimer;

        /// <summary>
        /// Vissza adja az élő mecseket
        /// </summary>
        public static async Task<List<LiveMatch>> GetAllLiveFixturesAsync()
        {
            string json = await GetLiveMatchesJsonAsync();
            if (string.IsNullOrEmpty(json))
            {
                App.Logger.Error("No data received from the API.");
                return new List<LiveMatch>();
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
                return new List<LiveMatch>();
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex);
                return new List<LiveMatch>();
            }
        }
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
                            if (value.value == "Over" && value.handicap == 1)
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
            string json = await GetOverFirstHalfOdsJsonAsync(liveMatch.Id);
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
                            if (value.value == "Over" && value.handicap == (liveMatch.FirstHalfResult.Item1 + liveMatch.FirstHalfResult.Item2 + 1))
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
        /// Vissza adja az adott mecs 1. félidő over 0.5 ods-at
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
        public static async Task<(double, double)> GetFirstAndSecondHalfPercentAsync(int league, int season, int team, string side)
        {
            string json = await GetPrevMatchesJsonAsync(league, season, team);
            if (string.IsNullOrEmpty(json))
            {
                App.Logger.Error("No data received from the API.");
                return (0, 0);
            }
            try
            {
                dynamic data = JsonConvert.DeserializeObject(json);
                int firstHalfOverMatchCount = 0;
                int secondHalfOverMatchCount = 0;
                int matchCount = 0;
                foreach (var response in data.response)
                {
                    if (response["fixture"]["status"]["elapsed"] == null || response["fixture"]["status"]["elapsed"] < 90) continue;
                    if (response["teams"][side]["id"] == team)
                    {
                        if (response["score"]["halftime"]["home"] > 0 || response["score"]["halftime"]["away"] > 0)
                            firstHalfOverMatchCount++;
                        if (response["score"]["halftime"]["home"] < response["score"]["fulltime"]["home"] || response["score"]["halftime"]["away"] < response["score"]["fulltime"]["away"])
                            secondHalfOverMatchCount++;
                        matchCount++;
                    }
                }
                if (matchCount < 8) return (0, 0);
                double firstHalf = Math.Round(((double)firstHalfOverMatchCount / matchCount) * 100, 0);
                double secondHalf = Math.Round(((double)secondHalfOverMatchCount / matchCount) * 100, 0);
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
        /// Vissza adja az mai mecseket
        /// </summary>
        public static async Task<List<PreMatch>> GetTodayMatchesAsync()
        {
            string json = await GetTodayMatchesJsonAsync();
            if (string.IsNullOrEmpty(json))
            {
                App.Logger.Error("No data received from the API.");
                return new List<PreMatch>();
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
                return new List<PreMatch>();
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex);
                return new List<PreMatch>();
            }
        }
        /// <summary>
        /// Vissza adja egy mecs eredményét id alapján
        /// </summary>
        public static async Task<MatchResult> GetMatchResByIdAsync(int id, BetType betType)
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
        /// Vissza adja hogy lehet-e az adott mecsre fogadni
        /// </summary>
        public static async Task<bool> CanBetOnMatchAsync(int id)
        {
            string json = await GetMatchOddJsonByIdAsync(id);
            if (string.IsNullOrEmpty(json))
            {
                App.Logger.Error("No data received from the API.");
                return false;
            }
            try
            {
                dynamic data = JsonConvert.DeserializeObject(json);
                if (data != null && data.response != null && data.response.Count > 0)
                {
                    var firstMatchResult = data.response[0];
                    if (firstMatchResult != null && firstMatchResult.bookmakers != null && firstMatchResult.bookmakers.Count > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (JsonException ex)
            {
                App.Logger.Exception(ex, "JSON deserialization error: ");
                return false;
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex);
                return false;
            }
        }
        /// <summary>
        /// Beállítja _requestLimitTimer kezdőértékeit és elinditja azt
        /// </summary>
        public static void SetupRequestLimitTimer()
        {
            _requestsInLastMinute = 0;
            _requestCurrentMinute = DateTime.Now;
            _requestLimitTimer = new System.Timers.Timer(1000);
            _requestLimitTimer.Elapsed += CheckRequestTimer;
            _requestLimitTimer.Enabled = true;
        }
        /// <summary>
        /// Vissza adja egy  HTTP request json-jét (host: api-football-v1.p.rapidapi.com)
        /// </summary>
        private static async Task<string> RequestJsonAsync(string request)
        {
            while (!CanRequest) await Task.Delay(50);
            _requestsInLastMinute++;
            string json = "";
            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"https://api-football-v1.p.rapidapi.com/v3/{request}");
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "5a714a4005mshc90ae5b388aad58p15b512jsncf4294509ce5");
                try
                {
                    var result = await client.GetAsync(endpoint);
                    if (result.StatusCode == System.Net.HttpStatusCode.TooManyRequests) // "Too Many Requests"
                    {
                        _requestsInLastMinute = MAX_REQUESTS_PER_MINUTE;
                        return await RequestJsonAsync(request);
                    }
                    result.EnsureSuccessStatusCode();
                    json = await result.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException ex)
                {
                    App.Logger.Error($"Error during HTTP request: {ex.Message}");
                }
                catch (Exception ex)
                {
                    App.Logger.Exception(ex);
                }
            }
            return json;
        }
        /// <summary>
        /// Vissza adja az élő mecsek lekérdezés json-jét
        /// </summary>
        private static async Task<string> GetLiveMatchesJsonAsync()
        {
            return await RequestJsonAsync("fixtures?live=all");
        }
        /// <summary>
        /// Vissza adja az első félidő over Odd json-jét
        /// </summary>
        /// <returns>Ha nem elérhető ilyen odd akkor 0-val tér vissza</returns>
        private static async Task<string> GetOverFirstHalfOdsJsonAsync(int fixtureId)
        {
            return await RequestJsonAsync($"odds/live?fixture={fixtureId}&bet=49");
        }
        /// <summary>
        /// Vissza adja a második félidő over Odd json-jét
        /// </summary>
        /// <returns>Ha nem elérhető ilyen odd akkor 0-val tér vissza</returns>
        private static async Task<string> GetOverSecondtHalfOdsJsonAsync(int fixtureId)
        {
            return await RequestJsonAsync($"odds/live?fixture={fixtureId}&bet=36");
        }
        /// <summary>
        /// Vissza adja egy liga standing json-jét
        /// </summary>
        private static async Task<string> GetStandingsJsonAsync(int league, int season)
        {
            return await RequestJsonAsync($"standings?league={league}&season={season}");
        }
        /// <summary>
        /// Vissza adja a csapat előző mecsei json-jét az adott ligában és szezonban
        /// </summary>
        private static async Task<string> GetPrevMatchesJsonAsync(int league, int season, int team)
        {
            return await RequestJsonAsync($"fixtures?league={league}&season={season}&team={team}");
        }
        /// <summary>
        /// Vissza adj az összes mai meccsek json-jét
        /// </summary>
        private static async Task<string> GetTodayMatchesJsonAsync()
        {
            return await RequestJsonAsync($"fixtures?date={DateTime.Now.ToString("yyyy-MM-dd")}&timezone=Europe%2FBudapest");
        }
        /// <summary>
        /// Vissza ad egy meccs json-jét id alapján
        /// </summary>
        private static async Task<string> GetMatcheJsonByIdAsync(int id)
        {
            return await RequestJsonAsync($"fixtures?id={id}");
        }
        /// <summary>
        /// Vissza ad egy meccs odds json-jét id alapján
        /// </summary>
        private static async Task<string> GetMatchOddJsonByIdAsync(int id)
        {
            return await RequestJsonAsync($"odds?fixture={id}");
        }
        /// <summary>
        /// kinullázza a request számlálót 1 percenként 
        /// </summary>
        private static void CheckRequestTimer(Object source, ElapsedEventArgs e)
        {
            if (_requestCurrentMinute.Minute != DateTime.Now.Minute)
            {
                _requestsInLastMinute = 0;
                _requestCurrentMinute = DateTime.Now;
            }
        }
    }
}

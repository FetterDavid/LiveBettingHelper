using LiveBettingHelper.Model;
using Newtonsoft.Json;
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
        /// Vissza adja egy HTTP request json-jét (host: api-football-v1.p.rapidapi.com)
        /// </summary>
        public static async Task<string> RequestJsonAsync(string request)
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
        /// (CSAK AZ ODDS-OKHOZ HASZNÁLD) Vissza adja egy HTTP request json-jét .(host: football-betting-odds1.p.rapidapi.com)
        /// </summary>
        public static async Task<string> RequestJsonFromOddsAsync(string request)
        {
            string json = "";
            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"https://football-betting-odds1.p.rapidapi.com/{request}");
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "football-betting-odds1.p.rapidapi.com");
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "5a714a4005mshc90ae5b388aad58p15b512jsncf4294509ce5");
                try
                {
                    var result = await client.GetAsync(endpoint);
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

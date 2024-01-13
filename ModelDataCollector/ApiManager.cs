using System.Timers;

namespace ModelDataCollector
{
    public static class ApiManager
    {
        private const int MAX_REQUESTS_PER_SECONDE = 4;
        public static bool CanRequest => _requestsInLastSeconde < MAX_REQUESTS_PER_SECONDE;
        private static int _requestsInLastSeconde;
        private static DateTime _requestCurrentSeconde;
        private static System.Timers.Timer _requestLimitTimer;
        /// <summary>
        /// Beállítja _requestLimitTimer kezdőértékeit és elinditja azt
        /// </summary>
        public static void SetupRequestLimitTimer()
        {
            _requestsInLastSeconde = 0;
            _requestCurrentSeconde = DateTime.Now;
            _requestLimitTimer = new System.Timers.Timer(100);
            _requestLimitTimer.Elapsed += CheckRequestTimer;
            _requestLimitTimer.Enabled = true;
        }
        /// <summary>
        /// Vissza adja egy HTTP request json-jét (host: api-football-v1.p.rapidapi.com)
        /// </summary>
        public static async Task<string> RequestJsonAsync(string request)
        {
            while (!CanRequest) await Task.Delay(50);
            _requestsInLastSeconde++;
            string json = "";
            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"https://soccer-football-info.p.rapidapi.com/{request}");
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "soccer-football-info.p.rapidapi.com");
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "5a714a4005mshc90ae5b388aad58p15b512jsncf4294509ce5");
                try
                {
                    var result = await client.GetAsync(endpoint);
                    if (result.StatusCode == System.Net.HttpStatusCode.TooManyRequests) // "Too Many Requests"
                    {
                        _requestsInLastSeconde = MAX_REQUESTS_PER_SECONDE;
                        return await RequestJsonAsync(request);
                    }
                    result.EnsureSuccessStatusCode();
                    json = await result.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException ex)
                {
                    Console.Error.WriteLine($"Error during HTTP request: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"{ex.Message}");
                }
            }
            return json;
        }
        /// <summary>
        /// kinullázza a request számlálót 1 percenként 
        /// </summary>
        private static void CheckRequestTimer(Object source, ElapsedEventArgs e)
        {
            if (_requestCurrentSeconde.Second != DateTime.Now.Second)
            {
                _requestsInLastSeconde = 0;
                _requestCurrentSeconde = DateTime.Now;
            }
        }
    }
}

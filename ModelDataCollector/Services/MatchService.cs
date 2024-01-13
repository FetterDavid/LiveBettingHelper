using ModelDataCollector.Model;
using Newtonsoft.Json;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ModelDataCollector.Services
{
    public class MatchService
    {
        /// <summary>
        /// Vissza adja az összes ligát
        /// </summary>
        public static async Task<List<FootballMatch>> GetAllMatchesByDateAsync(string date, int maxGoalMinute)
        {
            string json = await GetAllMatchesJsonByDateAsync(date);
            if (string.IsNullOrEmpty(json))
            {
                Console.Error.WriteLine("No data received from the API.");
                return [];
            }
            try
            {
                dynamic data = JsonConvert.DeserializeObject(json);
                List<FootballMatch> leagues = new();
                foreach (var result in data.result)
                {
                    bool foundData = false;
                    bool isFitting = true;
                    bool isOver = false;
                    if (result["events"] == null) continue;
                    foreach (var item in result.events)
                    {
                        foundData = true;
                        string type = item.type;
                        if (type == "goal")
                        {
                            string s = item.timer;
                            if (int.TryParse(s.Split('+')[0], out int minute))
                            {
                                if (minute < maxGoalMinute)
                                {
                                    isFitting = false;
                                    continue;
                                }
                                if (minute <= 45)
                                {
                                    isOver = true;
                                }
                            }
                        }
                    }
                    if (!isFitting || !foundData) continue;
                    FootballMatch match = new FootballMatch
                    {
                        Id = result["id"],
                        IsOver = Convert.ToInt32(isOver)
                    };
                    leagues.Add(match);
                }
                return leagues;
            }
            catch (JsonException ex)
            {
                Console.Error.WriteLine($"JSON deserialization error: {ex.Message}");
                return [];
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"{ex.Message}");
                return [];
            }
        }
        /// <summary>
        /// Vissza adja az összes ligát
        /// </summary>
        public static async Task<MatchStatistics> GetMatchStatisticsByIdAsync(string id, string time, int isOver)
        {
            string json = await GetMatchStatsByIdJsonAsync(id);
            if (string.IsNullOrEmpty(json))
            {
                Console.Error.WriteLine("No data received from the API.");
                return null;
            }
            try
            {
                dynamic data = JsonConvert.DeserializeObject(json);
                foreach (var result in data.result)
                {
                    string timer = result["timer"];
                    if (timer != time) continue;
                    return new MatchStatistics
                    {
                        IsOver = isOver,
                        // Home Team Statistics
                        HomePossession = int.TryParse(result["teamA"]?["possession"]?.ToString(), out int homePossession) ? homePossession : 0,
                        HomeAttacksNormal = int.TryParse(result["teamA"]?["attacks"]?["n"]?.ToString(), out int homeAttacksNormal) ? homeAttacksNormal : 0,
                        HomeAttacksDangerous = int.TryParse(result["teamA"]?["attacks"]?["d"]?.ToString(), out int homeAttacksDangerous) ? homeAttacksDangerous : 0,
                        HomeShootsTotal = int.TryParse(result["teamA"]?["shoots"]?["t"]?.ToString(), out int homeShootsTotal) ? homeShootsTotal : 0,
                        HomeShootsOnTarget = int.TryParse(result["teamA"]?["shoots"]?["on"]?.ToString(), out int homeShootsOnTarget) ? homeShootsOnTarget : 0,
                        HomeShootsOffTarget = int.TryParse(result["teamA"]?["shoots"]?["off"]?.ToString(), out int homeShootsOffTarget) ? homeShootsOffTarget : 0,
                        HomeGoalAssists = int.TryParse(result["teamA"]?["shoots"]?["g_a"]?.ToString(), out int homeGoalAssists) ? homeGoalAssists : 0,
                        HomePenalties = int.TryParse(result["teamA"]?["penalties"]?.ToString(), out int homePenalties) ? homePenalties : 0,
                        HomeCorners = int.TryParse(result["teamA"]?["corners"]?.ToString(), out int homeCorners) ? homeCorners : 0,
                        HomeFoulsTotal = int.TryParse(result["teamA"]?["fouls"]?["t"]?.ToString(), out int homeFoulsTotal) ? homeFoulsTotal : 0,
                        HomeFoulsYellowCards = int.TryParse(result["teamA"]?["fouls"]?["y_c"]?.ToString(), out int homeFoulsYellowCards) ? homeFoulsYellowCards : 0,
                        HomeFoulsYellowToRedCards = int.TryParse(result["teamA"]?["fouls"]?["y_t_r_c"]?.ToString(), out int homeFoulsYellowToRedCards) ? homeFoulsYellowToRedCards : 0,
                        HomeFoulsRedCards = int.TryParse(result["teamA"]?["fouls"]?["r_c"]?.ToString(), out int homeFoulsRedCards) ? homeFoulsRedCards : 0,
                        HomeSubstitutions = int.TryParse(result["teamA"]?["substitutions"]?.ToString(), out int homeSubstitutions) ? homeSubstitutions : 0,
                        HomeOffSides = int.TryParse(result["teamA"]?["off_sides"]?.ToString(), out int homeOffSides) ? homeOffSides : 0,
                        HomeThrowIns = int.TryParse(result["teamA"]?["throwins"]?.ToString(), out int homeThrowIns) ? homeThrowIns : 0,
                        HomeInjuries = int.TryParse(result["teamA"]?["injuries"]?.ToString(), out int homeInjuries) ? homeInjuries : 0,
                        HomeDominanceIndex = double.TryParse(result["teamA"]?["dominance"]?["index"]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double homeDominanceIndex) ? homeDominanceIndex : 0.0,
                        HomeDominanceAverageOver25 = double.TryParse(result["teamA"]?["dominance"]?["avg_2_5"]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double homeDominanceAverageOver25) ? homeDominanceAverageOver25 : 0.0,
                        // Away Team Statistics
                        AwayPossession = int.TryParse(result["teamB"]?["possession"]?.ToString(), out int awayPossession) ? awayPossession : 0,
                        AwayAttacksNormal = int.TryParse(result["teamB"]?["attacks"]?["n"]?.ToString(), out int awayAttacksNormal) ? awayAttacksNormal : 0,
                        AwayAttacksDangerous = int.TryParse(result["teamB"]?["attacks"]?["d"]?.ToString(), out int awayAttacksDangerous) ? awayAttacksDangerous : 0,
                        AwayShootsTotal = int.TryParse(result["teamB"]?["shoots"]?["t"]?.ToString(), out int awayShootsTotal) ? awayShootsTotal : 0,
                        AwayShootsOnTarget = int.TryParse(result["teamB"]?["shoots"]?["on"]?.ToString(), out int awayShootsOnTarget) ? awayShootsOnTarget : 0,
                        AwayShootsOffTarget = int.TryParse(result["teamB"]?["shoots"]?["off"]?.ToString(), out int awayShootsOffTarget) ? awayShootsOffTarget : 0,
                        AwayGoalAssists = int.TryParse(result["teamB"]?["shoots"]?["g_a"]?.ToString(), out int awayGoalAssists) ? awayGoalAssists : 0,
                        AwayPenalties = int.TryParse(result["teamB"]?["penalties"]?.ToString(), out int awayPenalties) ? awayPenalties : 0,
                        AwayCorners = int.TryParse(result["teamB"]?["corners"]?.ToString(), out int awayCorners) ? awayCorners : 0,
                        AwayFoulsTotal = int.TryParse(result["teamB"]?["fouls"]?["t"]?.ToString(), out int awayFoulsTotal) ? awayFoulsTotal : 0,
                        AwayFoulsYellowCards = int.TryParse(result["teamB"]?["fouls"]?["y_c"]?.ToString(), out int awayFoulsYellowCards) ? awayFoulsYellowCards : 0,
                        AwayFoulsYellowToRedCards = int.TryParse(result["teamB"]?["fouls"]?["y_t_r_c"]?.ToString(), out int awayFoulsYellowToRedCards) ? awayFoulsYellowToRedCards : 0,
                        AwayFoulsRedCards = int.TryParse(result["teamB"]?["fouls"]?["r_c"]?.ToString(), out int awayFoulsRedCards) ? awayFoulsRedCards : 0,
                        AwaySubstitutions = int.TryParse(result["teamB"]?["substitutions"]?.ToString(), out int awaySubstitutions) ? awaySubstitutions : 0,
                        AwayOffSides = int.TryParse(result["teamB"]?["off_sides"]?.ToString(), out int awayOffSides) ? awayOffSides : 0,
                        AwayThrowIns = int.TryParse(result["teamB"]?["throwins"]?.ToString(), out int awayThrowIns) ? awayThrowIns : 0,
                        AwayInjuries = int.TryParse(result["teamB"]?["injuries"]?.ToString(), out int awayInjuries) ? awayInjuries : 0,
                        AwayDominanceIndex = double.TryParse(result["teamB"]?["dominance"]?["index"]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double awayDominanceIndex) ? awayDominanceIndex : 0.0,
                        AwayDominanceAverageOver25 = double.TryParse(result["teamB"]?["dominance"]?["avg_2_5"]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double awayDominanceAverageOver25) ? awayDominanceAverageOver25 : 0.0
                    };
                }
                return null;
            }
            catch (JsonException ex)
            {
                Console.Error.WriteLine($"JSON deserialization error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"{ex.Message}");
                return null;
            }
        }
        /// <summary>
        /// Vissza adja az összes liga json-jét
        /// </summary>
        public static async Task<string> GetAllMatchesJsonByDateAsync(string date)
        {
            return await ApiManager.RequestJsonAsync($"matches/day/basic/?d={date}&p=1&l=en_US");
        }
        /// <summary>
        /// Vissza adja az összes liga json-jét
        /// </summary>
        public static async Task<string> GetMatchStatsByIdJsonAsync(string id)
        {
            return await ApiManager.RequestJsonAsync($"matches/view/progressive/?i={id}&l=en_US");
        }
    }
}

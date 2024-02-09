using ModelDataCollector.Model;
using ModelDataCollector.Services;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ModelDataCollector
{
    /// <summary>
    /// Hasznlaaton kivüli projekt tesztelés céljából jött létre, ne használd
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            ApiManager.SetupRequestLimitTimer();
        }
        static async Task SaveMatches()
        {
            List<FootballMatch> matches = new();
            for (int i = 0; i < 1095; i++)
            {
                string date = DateTime.Now.AddDays(i * -1).ToString("yyyyMMdd");
                matches.AddRange(await MatchService.GetAllMatchesByDateAsync(date, 20));
                Console.WriteLine($"{i}: {matches.Count}db");
                await Task.Delay(250);
            }
            string filePath = "matchies.txt";

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var match in matches)
                {
                    await writer.WriteLineAsync($"{match.Id},{match.IsOver}");
                }
            }
            Console.WriteLine($"--------------------------- Done -----------------------------");
        }

        static async Task SaveStats()
        {
            List<FootballMatch> matches = await LoadMatchesFromFileAsync("teszt2.txt");

            List<MatchStatistics> matchStatistics = new();
            int i = 0;
            foreach (var item in matches)
            {
                MatchStatistics m = await MatchService.GetMatchStatisticsByIdAsync(item.Id, "30:00", item.IsOver);
                Console.WriteLine(i);
                i++;
                if (m == null || m.HomePossession == 0 || m.AwayPossession == 0) continue;
                matchStatistics.Add(m);
            }
            string filePath = "tesztStats302.txt";

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var match in matchStatistics)
                {
                    await writer.WriteLineAsync($"{match.IsOver},{match.HomePossession},{match.HomeAttacksNormal},{match.HomeAttacksDangerous},{match.HomeShootsTotal},{match.HomeShootsOnTarget},{match.HomeShootsOffTarget}," +
                $"{match.HomeGoalAssists},{match.HomePenalties},{match.HomeCorners},{match.HomeFoulsTotal},{match.HomeFoulsYellowCards},{match.HomeFoulsYellowToRedCards},{match.HomeFoulsRedCards}," +
                $"{match.HomeSubstitutions},{match.HomeOffSides},{match.HomeThrowIns},{match.HomeInjuries},{match.HomeDominanceIndex.ToString(CultureInfo.InvariantCulture)},{match.HomeDominanceAverageOver25.ToString(CultureInfo.InvariantCulture)}," +
                $"{match.AwayPossession},{match.AwayAttacksNormal},{match.AwayAttacksDangerous},{match.AwayShootsTotal},{match.AwayShootsOnTarget},{match.AwayShootsOffTarget},{match.AwayGoalAssists}," +
                $"{match.AwayPenalties},{match.AwayCorners},{match.AwayFoulsTotal},{match.AwayFoulsYellowCards},{match.AwayFoulsYellowToRedCards},{match.AwayFoulsRedCards},{match.AwaySubstitutions}," +
                $"{match.AwayOffSides},{match.AwayThrowIns},{match.AwayInjuries},{match.AwayDominanceIndex.ToString(CultureInfo.InvariantCulture)},{match.AwayDominanceAverageOver25.ToString(CultureInfo.InvariantCulture)}");
                }
            }
            Console.WriteLine($"--------------------------- Done -----------------------------");
        }

        public static async Task<List<FootballMatch>> LoadMatchesFromFileAsync(string filePath)
        {
            var matches = new List<FootballMatch>();

            try
            {
                string[] lines = await File.ReadAllLinesAsync(filePath);

                foreach (var line in lines)
                {
                    var parts = line.Split(',');

                    if (parts.Length == 2 && int.TryParse(parts[1], out int isOver))
                    {
                        matches.Add(new FootballMatch
                        {
                            Id = parts[0],
                            IsOver = isOver
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }

            return matches;
        }

        public static async Task<List<MatchStatistics>> LoadMatchStatisticsFromFileAsync(string filePath)
        {
            var matches = new List<MatchStatistics>();
            try
            {
                string[] lines = await File.ReadAllLinesAsync(filePath);

                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    matches.Add(new MatchStatistics
                    {
                        IsOver = int.Parse(parts[0]),
                        HomePossession = int.Parse(parts[1]),
                        HomeAttacksNormal = int.Parse(parts[2]),
                        HomeAttacksDangerous = int.Parse(parts[3]),
                        HomeShootsTotal = int.Parse(parts[4]),
                        HomeShootsOnTarget = int.Parse(parts[5]),
                        HomeShootsOffTarget = int.Parse(parts[6]),
                        HomeGoalAssists = int.Parse(parts[7]),
                        HomePenalties = int.Parse(parts[8]),
                        HomeCorners = int.Parse(parts[9]),
                        HomeFoulsTotal = int.Parse(parts[10]),
                        HomeFoulsYellowCards = int.Parse(parts[11]),
                        HomeFoulsYellowToRedCards = int.Parse(parts[12]),
                        HomeFoulsRedCards = int.Parse(parts[13]),
                        HomeSubstitutions = int.Parse(parts[14]),
                        HomeOffSides = int.Parse(parts[15]),
                        HomeThrowIns = int.Parse(parts[16]),
                        HomeInjuries = int.Parse(parts[17]),
                        HomeDominanceIndex = double.Parse(parts[18], NumberStyles.Any, CultureInfo.InvariantCulture),
                        HomeDominanceAverageOver25 = double.Parse(parts[19], NumberStyles.Any, CultureInfo.InvariantCulture),
                        // Away Team Statistics
                        AwayPossession = int.Parse(parts[20]),
                        AwayAttacksNormal = int.Parse(parts[21]),
                        AwayAttacksDangerous = int.Parse(parts[22]),
                        AwayShootsTotal = int.Parse(parts[23]),
                        AwayShootsOnTarget = int.Parse(parts[24]),
                        AwayShootsOffTarget = int.Parse(parts[25]),
                        AwayGoalAssists = int.Parse(parts[26]),
                        AwayPenalties = int.Parse(parts[27]),
                        AwayCorners = int.Parse(parts[28]),
                        AwayFoulsTotal = int.Parse(parts[29]),
                        AwayFoulsYellowCards = int.Parse(parts[30]),
                        AwayFoulsYellowToRedCards = int.Parse(parts[31]),
                        AwayFoulsRedCards = int.Parse(parts[32]),
                        AwaySubstitutions = int.Parse(parts[33]),
                        AwayOffSides = int.Parse(parts[34]),
                        AwayThrowIns = int.Parse(parts[35]),
                        AwayInjuries = int.Parse(parts[36]),
                        AwayDominanceIndex = double.Parse(parts[37], NumberStyles.Any, CultureInfo.InvariantCulture),
                        AwayDominanceAverageOver25 = double.Parse(parts[38], NumberStyles.Any, CultureInfo.InvariantCulture)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }

            return matches;
        }

        public static async Task NormalizeStatics(List<MatchStatistics> statistics)
        {
            double maxAttacksNormal = Math.Max(statistics.Max(x => x.HomeAttacksNormal), statistics.Max(x => x.AwayAttacksNormal));
            double maxAttacksDangerous = Math.Max(statistics.Max(x => x.HomeAttacksDangerous), statistics.Max(x => x.AwayAttacksDangerous));
            double maxShootsTotal = Math.Max(statistics.Max(x => x.HomeShootsTotal), statistics.Max(x => x.AwayShootsTotal));
            double maxShootsOnTarget = Math.Max(statistics.Max(x => x.HomeShootsOnTarget), statistics.Max(x => x.AwayShootsOnTarget));
            double maxShootsOffTarget = Math.Max(statistics.Max(x => x.HomeShootsOffTarget), statistics.Max(x => x.AwayShootsOffTarget));
            double maxPenalties = Math.Max(statistics.Max(x => x.HomePenalties), statistics.Max(x => x.AwayPenalties));
            double maxCorners = Math.Max(statistics.Max(x => x.HomeCorners), statistics.Max(x => x.AwayCorners));
            double maxFoulsTotal = Math.Max(statistics.Max(x => x.HomeFoulsTotal), statistics.Max(x => x.AwayFoulsTotal));
            double maxFoulsYellowCards = Math.Max(statistics.Max(x => x.HomeFoulsYellowCards), statistics.Max(x => x.AwayFoulsYellowCards));
            double maxFoulsRedCards = Math.Max(statistics.Max(x => x.HomeFoulsRedCards), statistics.Max(x => x.AwayFoulsRedCards));
            double maxSubstitutions = Math.Max(statistics.Max(x => x.HomeSubstitutions), statistics.Max(x => x.AwaySubstitutions));
            double maxOffSides = Math.Max(statistics.Max(x => x.HomeOffSides), statistics.Max(x => x.AwayOffSides));
            double maxThrowIns = Math.Max(statistics.Max(x => x.HomeThrowIns), statistics.Max(x => x.AwayThrowIns));
            double maxInjuries = Math.Max(statistics.Max(x => x.HomeInjuries), statistics.Max(x => x.AwayInjuries));
            double maxDominanceIndex = Math.Max(statistics.Max(x => x.HomeDominanceIndex), statistics.Max(x => x.AwayDominanceIndex));
            double maxDominanceAverageOver25 = Math.Max(statistics.Max(x => x.HomeDominanceAverageOver25), statistics.Max(x => x.AwayDominanceAverageOver25));
            List<MatchStatistics> normStatistics = new();
            foreach (var stat in statistics)
            {
                stat.HomePossession = Math.Round(stat.HomePossession / 100.0d, 3);
                stat.HomeAttacksNormal = Math.Round(stat.HomeAttacksNormal / maxAttacksNormal, 3);
                stat.HomeAttacksDangerous = Math.Round(stat.HomeAttacksDangerous / maxAttacksDangerous, 3);
                stat.HomeShootsTotal = Math.Round(stat.HomeShootsTotal / maxShootsTotal, 3);
                stat.HomeShootsOnTarget = Math.Round(stat.HomeShootsOnTarget / maxShootsOnTarget, 3);
                stat.HomeShootsOffTarget = Math.Round(stat.HomeShootsOffTarget / maxShootsOffTarget, 3);
                stat.HomePenalties = Math.Round(stat.HomePenalties / maxPenalties, 3);
                stat.HomeCorners = Math.Round(stat.HomeCorners / maxCorners, 3);
                stat.HomeFoulsTotal = Math.Round(stat.HomeFoulsTotal / maxFoulsTotal, 3);
                stat.HomeFoulsYellowCards = Math.Round(stat.HomeFoulsYellowCards / maxFoulsYellowCards, 3);
                stat.HomeFoulsRedCards = Math.Round(stat.HomeFoulsRedCards / maxFoulsRedCards, 3);
                stat.HomeSubstitutions = Math.Round(stat.HomeSubstitutions / maxSubstitutions, 3);
                stat.HomeOffSides = Math.Round(stat.HomeOffSides / maxOffSides, 3);
                stat.HomeThrowIns = Math.Round(stat.HomeThrowIns / maxThrowIns, 3);
                stat.HomeInjuries = Math.Round(stat.HomeInjuries / maxInjuries, 3);
                stat.HomeDominanceIndex = Math.Round(stat.HomeDominanceIndex / maxDominanceIndex, 3);
                stat.HomeDominanceAverageOver25 = Math.Round(stat.HomeDominanceAverageOver25 / maxDominanceAverageOver25, 3);
                // Away Team Statistics
                stat.AwayPossession = Math.Round(stat.AwayPossession / 100, 3);
                stat.AwayAttacksNormal = Math.Round(stat.AwayAttacksNormal / maxAttacksNormal, 3);
                stat.AwayAttacksDangerous = Math.Round(stat.AwayAttacksDangerous / maxAttacksDangerous, 3);
                stat.AwayShootsTotal = Math.Round(stat.AwayShootsTotal / maxShootsTotal, 3);
                stat.AwayShootsOnTarget = Math.Round(stat.AwayShootsOnTarget / maxShootsOnTarget, 3);
                stat.AwayShootsOffTarget = Math.Round(stat.AwayShootsOffTarget / maxShootsOffTarget, 3);
                stat.AwayPenalties = Math.Round(stat.AwayPenalties / maxPenalties, 3);
                stat.AwayCorners = Math.Round(stat.AwayCorners / maxCorners, 3);
                stat.AwayFoulsTotal = Math.Round(stat.AwayFoulsTotal / maxFoulsTotal, 3);
                stat.AwayFoulsYellowCards = Math.Round(stat.AwayFoulsYellowCards / maxFoulsYellowCards, 3);
                stat.AwayFoulsRedCards = Math.Round(stat.AwayFoulsRedCards / maxFoulsRedCards, 3);
                stat.AwaySubstitutions = Math.Round(stat.AwaySubstitutions / maxSubstitutions, 3);
                stat.AwayOffSides = Math.Round(stat.AwayOffSides / maxOffSides, 3);
                stat.AwayThrowIns = Math.Round(stat.AwayThrowIns / maxThrowIns, 3);
                stat.AwayInjuries = Math.Round(stat.AwayInjuries / maxInjuries, 3);
                stat.AwayDominanceIndex = Math.Round(stat.AwayDominanceIndex / maxDominanceIndex, 3);
                stat.AwayDominanceAverageOver25 = Math.Round(stat.AwayDominanceAverageOver25 / maxDominanceAverageOver25, 3);
                normStatistics.Add(stat);
            }

            string filePath = "tesztNormStats30.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var stat in normStatistics)
                {
                    await writer.WriteLineAsync(
                    $"{stat.IsOver}," +

                    $"{stat.HomePossession.ToString("0.000", CultureInfo.InvariantCulture)}," +
                    $"{stat.HomeAttacksNormal.ToString("0.000", CultureInfo.InvariantCulture)}," +
                    $"{stat.HomeAttacksDangerous.ToString("0.000", CultureInfo.InvariantCulture)}," +
                    $"{stat.HomeShootsTotal.ToString("0.000", CultureInfo.InvariantCulture)}," +
                    $"{stat.HomeShootsOnTarget.ToString("0.000", CultureInfo.InvariantCulture)}," +
                    $"{stat.HomeShootsOffTarget.ToString("0.000", CultureInfo.InvariantCulture)}," +
                    $"{stat.HomeCorners.ToString("0.000", CultureInfo.InvariantCulture)}," +
                    $"{stat.HomeFoulsTotal.ToString("0.000", CultureInfo.InvariantCulture)}," +
                    $"{stat.HomeDominanceIndex.ToString("0.000", CultureInfo.InvariantCulture)}," +
                    $"{stat.HomeDominanceAverageOver25.ToString("0.000", CultureInfo.InvariantCulture)}," +

                    $"{stat.AwayPossession.ToString("0.000", CultureInfo.InvariantCulture)}," +
                    $"{stat.AwayAttacksNormal.ToString("0.000", CultureInfo.InvariantCulture)}," +
                    $"{stat.AwayAttacksDangerous.ToString("0.000", CultureInfo.InvariantCulture)}," +
                    $"{stat.AwayShootsTotal.ToString("0.000", CultureInfo.InvariantCulture)}," +
                    $"{stat.AwayShootsOnTarget.ToString("0.000", CultureInfo.InvariantCulture)}," +
                    $"{stat.AwayShootsOffTarget.ToString("0.000", CultureInfo.InvariantCulture)}," +
                    $"{stat.AwayCorners.ToString("0.000", CultureInfo.InvariantCulture)}," +
                    $"{stat.AwayFoulsTotal.ToString("0.000", CultureInfo.InvariantCulture)}," +
                    $"{stat.AwayDominanceIndex.ToString("0.000", CultureInfo.InvariantCulture)}," +
                    $"{stat.AwayDominanceAverageOver25.ToString("0.000", CultureInfo.InvariantCulture)}");
                }
            }
        }
    }
}

using LiveBettingHelper.Model;
using Microcharts;

namespace LiveBettingHelper.ViewModels
{
    public partial class BetStatisticsViewModel : BaseViewModel
    {
        public DonutChart BetWinDonutChart { get; set; }
        public DonutChart FirstHalfOverWinDonutChart { get; set; }
        public DonutChart SecondHalfOverWinDonutChart { get; set; }
        public double AvgOdds { get; set; }
        public double AvgWinnedBetProb { get; set; }
        public double AvgLosedBetProb { get; set; }
        // First half
        public double AvgOddsFHO { get; set; }
        public double AvgBetMinuteFHO { get; set; }
        public double AvgWinBetMinuteFHO { get; set; }
        public double AvgLoseBetMinuteFHO { get; set; }
        public double AvgWinnedBetProbFH { get; set; }
        public double AvgLosedBetProbFH { get; set; }
        // Second Half
        public double AvgOddsSHO { get; set; }
        public double AvgBetMinuteSHO { get; set; }
        public double AvgWinBetMinuteSHO { get; set; }
        public double AvgLoseBetMinuteSHO { get; set; }
        public double AvgWinnedBetProbSH { get; set; }
        public double AvgLosedBetProbSH { get; set; }

        public BetStatisticsViewModel()
        {
            List<Bet> bets = App.MM.BetRepo.GetItems();
            LoadCharts(bets);
            LoadStatistics(bets);
        }

        private void LoadStatistics(List<Bet> bets)
        {
            try
            {
                if (bets.Count == 0) return;
                AvgOdds = Math.Round(bets.Select(x => x.Odds).Average(), 2);
                if (bets.Where(x => x.Winned).Count()>0) 
                    AvgWinnedBetProb = Math.Round(bets.Where(x => x.Winned).Select(x => x.Probability).Average(), 0);
                if (bets.Where(x => !x.Winned).Count() > 0)
                    AvgLosedBetProb = Math.Round(bets.Where(x => !x.Winned).Select(x => x.Probability).Average(), 0);
                List<Bet> fhBets = bets.Where(x => x.BettingType == Utilities.BetType.FirstHalfOver).ToList();
                if (fhBets.Count > 0)
                {
                    AvgOddsFHO = Math.Round(fhBets.Select(x => x.Odds).Average(), 2);
                    AvgBetMinuteFHO = Math.Round(fhBets.Select(x => x.BetMinute).Average(), 0);
                    if (fhBets.Where(x => x.Winned).Count() > 0)
                    {
                        AvgWinBetMinuteFHO = Math.Round(fhBets.Where(x => x.Winned).Select(x => x.BetMinute).Average(), 0);
                        AvgWinnedBetProbFH = Math.Round(fhBets.Where(x => x.Winned).Select(x => x.Probability).Average(), 0);
                    }
                    if (fhBets.Where(x => !x.Winned).Count() > 0)
                    {
                        AvgLoseBetMinuteFHO = Math.Round(fhBets.Where(x => !x.Winned).Select(x => x.BetMinute).Average(), 0);
                        AvgLosedBetProbFH = Math.Round(fhBets.Where(x => !x.Winned).Select(x => x.Probability).Average(), 0);
                    }
                }
                List<Bet> shBets = bets.Where(x => x.BettingType == Utilities.BetType.SecondHalfOver).ToList();
                if (shBets.Count > 0)
                {
                    AvgOddsSHO = Math.Round(shBets.Select(x => x.Odds).Average(), 2);
                    AvgBetMinuteSHO = Math.Round(shBets.Select(x => x.BetMinute).Average(), 0);
                    if (shBets.Where(x => x.Winned).Count() > 0)
                    {
                        AvgWinBetMinuteSHO = Math.Round(shBets.Where(x => x.Winned).Select(x => x.BetMinute).Average(), 0);
                        AvgWinnedBetProbSH = Math.Round(shBets.Where(x => x.Winned).Select(x => x.Probability).Average(), 0);
                    }
                    if (shBets.Where(x => !x.Winned).Count() > 0)
                    {
                        AvgLoseBetMinuteSHO = Math.Round(shBets.Where(x => !x.Winned).Select(x => x.BetMinute).Average(), 0);
                        AvgLosedBetProbSH = Math.Round(shBets.Where(x => !x.Winned).Select(x => x.Probability).Average(), 0);
                    }
                }
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, "An error occurred while trying to load the bet statistics");
            }
        }

        private void LoadCharts(List<Bet> bets)
        {
            BetWinDonutChart = GetWinLoseDonutChart(bets.Where(x => x.Winned).Count(), bets.Where(x => !x.Winned).Count(), bets.Count());
            List<Bet> fhBets = bets.Where(x => x.BettingType == Utilities.BetType.FirstHalfOver).ToList();
            FirstHalfOverWinDonutChart = GetWinLoseDonutChart(fhBets.Where(x => x.Winned).Count(), fhBets.Where(x => !x.Winned).Count(), fhBets.Count(), 40);
            List<Bet> shBets = bets.Where(x => x.BettingType == Utilities.BetType.SecondHalfOver).ToList();
            SecondHalfOverWinDonutChart = GetWinLoseDonutChart(shBets.Where(x => x.Winned).Count(), shBets.Where(x => !x.Winned).Count(), shBets.Count(), 40);
        }

        private DonutChart GetWinLoseDonutChart(float win, float lose, float all, float labelTextSize = 60)
        {
            ChartEntry[] entries =
           {
                new ChartEntry(win) { Label = "Win", Color = SkiaSharp.SKColor.Parse("6AAF6A"), ValueLabel=$"{Math.Round(win / all * 100)}%", ValueLabelColor= SkiaSharp.SKColor.Parse("C4C4C4"), TextColor= SkiaSharp.SKColor.Parse("6AAF6A")},
                new ChartEntry(lose) { Label = "Lose", Color = SkiaSharp.SKColor.Parse("D8524B"), ValueLabel=$"{Math.Round(lose / all * 100)}%",ValueLabelColor= SkiaSharp.SKColor.Parse("C4C4C4"), TextColor= SkiaSharp.SKColor.Parse("D8524B")}
            };
            if (entries.Count() == 0) return new DonutChart();
            return new DonutChart()
            {
                Entries = entries,
                LabelTextSize = labelTextSize,
                LabelMode = LabelMode.RightOnly,
                BackgroundColor = SkiaSharp.SKColor.Parse("585858"),
            };
        }
    }
}

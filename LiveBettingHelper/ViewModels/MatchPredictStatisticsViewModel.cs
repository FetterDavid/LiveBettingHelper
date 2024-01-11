using LiveBettingHelper.Model;
using LiveBettingHelper.Utilities;
using Microcharts;

namespace LiveBettingHelper.ViewModels
{
    public partial class MatchPredictStatisticsViewModel
    {
        public DonutChart MatchPredictSuccessDonutChart { get; set; }
        public DonutChart FirstHalfOverWinDonutChart { get; set; }
        public DonutChart SecondHalfOverWinDonutChart { get; set; }

        public MatchPredictStatisticsViewModel()
        {

            LoadCharts();
        }

        private void LoadCharts()
        {
            List<ArchivedPreBet> bets = App.MM.ArchivedPreBetRepo.GetItems();
            MatchPredictSuccessDonutChart = GetWinLoseDonutChart(bets.Where(x => x.IsWon).Count(), bets.Where(x => !x.IsWon).Count(), bets.Count());
            List<ArchivedPreBet> fhBets = bets.Where(x => x.BettingType == BetType.FirstHalfOver).ToList();
            FirstHalfOverWinDonutChart = GetWinLoseDonutChart(fhBets.Where(x => x.IsWon).Count(), fhBets.Where(x => !x.IsWon).Count(), fhBets.Count(), 40);
            List<ArchivedPreBet> shBets = bets.Where(x => x.BettingType == BetType.SecondHalfOver).ToList();
            SecondHalfOverWinDonutChart = GetWinLoseDonutChart(shBets.Where(x => x.IsWon).Count(), shBets.Where(x => !x.IsWon).Count(), shBets.Count(), 40);
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

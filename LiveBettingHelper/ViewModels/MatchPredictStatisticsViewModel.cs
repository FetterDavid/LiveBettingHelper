using Microcharts;

namespace LiveBettingHelper.ViewModels
{
    public partial class MatchPredictStatisticsViewModel
    {
        public DonutChart MatchPredictSuccessDonutChart { get; set; }

        public MatchPredictStatisticsViewModel()
        {
            LoadMatchPredictSuccessData();
        }


        private void LoadMatchPredictSuccessData()
        {
            float winCount = App.MM.ArchivedPreBetRepo.GetItems(x => x.IsWon).Count();
            float loseCount = App.MM.ArchivedPreBetRepo.GetItems(x => !x.IsWon).Count();
            float betCount = winCount + loseCount;
            ChartEntry[] entries =
            {
                new ChartEntry(winCount) { Label = "Win", Color = SkiaSharp.SKColor.Parse("6AAF6A"), ValueLabel=$"{Math.Round(winCount / betCount * 100)}%", ValueLabelColor = SkiaSharp.SKColor.Parse("C4C4C4"), TextColor= SkiaSharp.SKColor.Parse("6AAF6A")},
                new ChartEntry(loseCount) { Label = "Lose", Color = SkiaSharp.SKColor.Parse("D8524B"), ValueLabel=$"{Math.Round(loseCount / betCount * 100)}%", ValueLabelColor = SkiaSharp.SKColor.Parse("C4C4C4"), TextColor= SkiaSharp.SKColor.Parse("D8524B")}
            };
            if (entries.Count() == 0) return;
            MatchPredictSuccessDonutChart = new DonutChart()
            {
                Entries = entries,
                LabelTextSize = 60,
                LabelMode = LabelMode.RightOnly,
                BackgroundColor = SkiaSharp.SKColor.Parse("585858"),
            };
        }
    }
}

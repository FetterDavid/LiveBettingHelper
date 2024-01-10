using LiveBettingHelper.Model;
using Microcharts;

namespace LiveBettingHelper.ViewModels
{
    public partial class StatisticsViewModel : BaseViewModel
    {
        public DonutChart BetWinDonutChart { get; set; }
        public DonutChart MatchPredictSuccessDonutChart { get; set; }
        public LineChart BankLineChart { get; set; }

        public StatisticsViewModel()
        {
            LoadBetWinData();
            LoadMatchPredictSuccessData();
            LoadBankData();
        }

        private void LoadBetWinData()
        {
            float winCount = App.MM.BetRepo.GetItems(x => x.Winned).Count();
            float loseCount = App.MM.BetRepo.GetItems(x => !x.Winned).Count();
            float betCount = winCount + loseCount;
            ChartEntry[] entries =
            {
                new ChartEntry(winCount) { Label = "Win", Color = SkiaSharp.SKColor.Parse("6AAF6A"), ValueLabel=$"{Math.Round(winCount / betCount * 100)}%", ValueLabelColor= SkiaSharp.SKColor.Parse("C4C4C4"), TextColor= SkiaSharp.SKColor.Parse("6AAF6A")},
                new ChartEntry(loseCount) { Label = "Lose", Color = SkiaSharp.SKColor.Parse("D8524B"), ValueLabel=$"{Math.Round(loseCount / betCount * 100)}%",ValueLabelColor= SkiaSharp.SKColor.Parse("C4C4C4"), TextColor= SkiaSharp.SKColor.Parse("D8524B")}
            };
            if (entries.Count() == 0) return;
            BetWinDonutChart = new DonutChart()
            {
                Entries = entries,
                LabelTextSize = 60,
                LabelMode = LabelMode.RightOnly,
                BackgroundColor = SkiaSharp.SKColor.Parse("585858"),
            };
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

        private void LoadBankData()
        {
            List<BankTransactionRecord> records = App.BankManager.GetRecords().Take(20).ToList();
            List<ChartEntry> entries = new();
            foreach (BankTransactionRecord record in records)
            {
                entries.Add(new ChartEntry((float)record.BalanceAfterTransaction)
                {
                    Label = record.Date.ToString("MM.dd"),
                    ValueLabel = $"{record.BalanceAfterTransaction}",
                    ValueLabelColor = SkiaSharp.SKColor.Parse("C4C4C4"),
                    TextColor = SkiaSharp.SKColor.Parse("6AAF6A"),
                    Color = SkiaSharp.SKColor.Parse("23a3a3")
                });
            }
            if (entries.Count() == 0) return;
            BankLineChart = new LineChart()
            {
                Entries = entries,
                LabelTextSize = 25,
                BackgroundColor = SkiaSharp.SKColor.Parse("585858"),
            };
        }
    }
}

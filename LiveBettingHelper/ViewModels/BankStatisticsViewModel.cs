using LiveBettingHelper.Model;
using Microcharts;

namespace LiveBettingHelper.ViewModels
{
    public partial class BankStatisticsViewModel : BaseViewModel
    {
        public LineChart BankLineChart { get; set; }

        public BankStatisticsViewModel()
        {
            LoadBankData();
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

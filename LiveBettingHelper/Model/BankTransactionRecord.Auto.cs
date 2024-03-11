using LiveBettingHelper.Abstractions;

namespace LiveBettingHelper.Model
{
    public partial class BankTransactionRecord : LocalBaseModel
    {
        public double BalanceAfterTransaction { get; set; }
        public double ChangeAmount { get; set; }
        public DateTime Date { get; set; }
    }
}

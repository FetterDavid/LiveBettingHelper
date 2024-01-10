using LiveBettingHelper.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Model
{
    public partial class BankTransactionRecord : LocalBaseModel
    {
        public double BalanceAfterTransaction { get; set; }
        public double ChangeAmount { get; set; }
        public DateTime Date { get; set; }
    }
}

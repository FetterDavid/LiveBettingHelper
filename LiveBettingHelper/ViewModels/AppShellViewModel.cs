using LiveBettingHelper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        public BankManager BankManager { get; set; }

        public AppShellViewModel(BankManager bankManager)
        {
            this.BankManager = bankManager;
        }
    }
}

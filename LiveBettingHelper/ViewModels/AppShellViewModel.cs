using LiveBettingHelper.Utilities;

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

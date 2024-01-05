using LiveBettingHelper.Utilities;

namespace LiveBettingHelper.ViewModels
{
    public partial class MenuViewModel : BaseViewModel
    {
        public BankManager BankManager { get; set; }

        public MenuViewModel(BankManager bankManager)
        {
            this.BankManager = bankManager;
        }
    }
}

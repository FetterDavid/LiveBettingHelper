using LiveBettingHelper.Utilities;

namespace LiveBettingHelper.ViewModels
{
    public class SettingsViewModel
    {
        public BankManager BankManager { get; set; }

        public SettingsViewModel(BankManager bankManager)
        {
            this.BankManager = bankManager;
        }
    }
}

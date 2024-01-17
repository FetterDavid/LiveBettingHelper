using LiveBettingHelper.Utilities;

namespace LiveBettingHelper.ViewModels
{
    public class SettingsViewModel
    {
        public SettingsManager SettingsManager { get; set; }

        public SettingsViewModel(SettingsManager settingsManager)
        {
            this.SettingsManager = settingsManager;
        }
    }
}

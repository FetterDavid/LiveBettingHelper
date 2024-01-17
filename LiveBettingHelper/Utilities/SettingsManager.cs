using LiveBettingHelper.Model;
using LiveBettingHelper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Utilities
{
    public class SettingsManager
    {
        public Settings MySettings { get; set; }
        private BaseRepository<Settings> _settingsRepo { get; set; } = new();

        public SettingsManager()
        {
            LoadMySettings();
        }

        public void Update()
        {
            _settingsRepo.UpdateItem(MySettings);
        }

        private void LoadMySettings()
        {
            List<Settings> settings = _settingsRepo.GetItems();
            if (settings.Count > 1) App.Logger.Error("There cannot be more than 1 settings in the repository.");
            else if (settings.Count == 1) MySettings = settings[0];
            else
            {
                MySettings = new Settings { DefaultBetStake = 300, MinOdds = 1.5, SelectionSystemMinProbability = 70 };
                _settingsRepo.AddItem(MySettings);
            }
        }
    }
}

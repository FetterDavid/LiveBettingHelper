using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveBettingHelper.Model;
using System.Collections.ObjectModel;


namespace LiveBettingHelper.ViewModels
{
    public partial class BetViewModel : BaseViewModel
    {
        /// <summary>
        /// Folyamatban lévő fogadások
        /// </summary>
        public ObservableCollection<Bet> UnsettledBets;
        /// <summary>
        /// Végetért fogadások
        /// </summary>
        public ObservableCollection<Bet> SettledBets;
        /// <summary>
        /// Aktiv-e a folyamatban lévő fogadások tab
        /// </summary>
        [ObservableProperty]
        private bool _isUnsettledBetsTabActive;
        /// <summary>
        /// Aktiv-e a végetért fogadások tab
        /// </summary>
        [ObservableProperty]
        private bool _isSettledBetsTabActive;
        /// <summary>
        /// Ez a parancs fut le ha kiválasztjuk a folyamatban lévő fogadások tab-ot
        /// </summary>
        public Command OnSelectUnsettledBetsTab { get; set; }
        /// <summary>
        /// Ez a parancs fut le ha kiválasztjuk a végetért fogadások tab-ot
        /// </summary>
        public Command OnSelectSettledBetsTab { get; set; }

        public BetViewModel()
        {
            UnsettledBets = new();
            SettledBets = new();
            OnSelectUnsettledBetsTab = new Command(SelectUnsettledBetsTab);
            OnSelectSettledBetsTab = new Command(SelectSettledBetsTab);
            IsUnsettledBetsTabActive = true;
        }
        /// <summary>
        /// Újra tölti a fogadásokat és ellenörzi az állapotukat
        /// </summary>
        public async Task ReloadBets()
        {
            IsBusy = true;
            await CheckBetsStatus();
            LoadBets();
            IsBusy = false;
        }
        /// <summary>
        /// A még nem lezárt fogadásokat ellenörzi és lezárja ha lehet
        /// </summary>
        public async Task CheckBetsStatus()
        {
            List<Bet> bets = App.MM.BetRepo.GetItems(x => x.Finished == false);
            foreach (var bet in bets) await bet.TryDetermineOutcome();
        }
        /// <summary>
        /// Újra tölti a fogadásokat és státusza alapján 2 listába osztja szét (SettledBets/UnsettledBets)
        /// </summary>
        public void LoadBets()
        {
            List<Bet> bets = App.MM.BetRepo.GetItems();
            UnsettledBets.Clear();
            SettledBets.Clear();
            foreach (var bet in bets)
            {
                if (bet.Finished) SettledBets.Add(bet);
                else UnsettledBets.Add(bet);
            }
            UnsettledBets = UnsettledBets.OrderBy(x => x.Date).ToObservableCollection();
        }

        private void SelectUnsettledBetsTab()
        {
            IsSettledBetsTabActive = false;
            IsUnsettledBetsTabActive = true;
        }

        private void SelectSettledBetsTab()
        {
            IsUnsettledBetsTabActive = false;
            IsSettledBetsTabActive = true;
        }
    }
}

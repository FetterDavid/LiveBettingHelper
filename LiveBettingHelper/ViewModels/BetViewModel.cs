using CommunityToolkit.Mvvm.ComponentModel;
using LiveBettingHelper.Model;


namespace LiveBettingHelper.ViewModels
{
    public partial class BetViewModel : BaseViewModel
    {
        /// <summary>
        /// Folyamatban lévő fogadások
        /// </summary>
        [ObservableProperty]
        private List<Bet> _unsettledBets;
        /// <summary>
        /// Végetért fogadások
        /// </summary>
        [ObservableProperty]
        private List<Bet> _settledBets;
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
            await CheckBetsStatus();
            LoadBets();
        }

        public async Task CheckBetsStatus()
        {
            List<Bet> bets = App.MM.BetRepo.GetItems(x => x.Finished == false);
            foreach (var bet in bets) await bet.TryDetermineOutcome();
        }

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
            UnsettledBets = UnsettledBets.OrderBy(x => x.Date).ToList();
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

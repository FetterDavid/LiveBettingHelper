using LiveBettingHelper.Model;

namespace LiveBettingHelper.ViewModels
{
    public partial class CountrySelectorViewModel : BaseViewModel
    {
        /// <summary>
        /// Az országok listája
        /// </summary>
        public List<Country> Countries { get; set; }
        /// <summary>
        /// Ez a parancs fut le ha kijelölünk egy országot a listában
        /// </summary>
        public Command OnSelectACountry { get; set; }
        /// <summary>
        /// Ez a parancs fut le ha megszüntetjük egy ország kijelőlését a listában
        /// </summary>
        public Command OnDeselectACountry { get; set; }

        public CountrySelectorViewModel()
        {
            OnSelectACountry = new Command(SelectACountry);
            OnDeselectACountry = new Command(DeselectACountry);
            LoadCountries();
        }

        private void LoadCountries()
        {
            Countries = App.MM.CountryRepo.GetItems().ToList();
        }
        /// <summary>
        /// Újra tölti a listában lévő országok kijelölésének tipusát (a kiválasztott ligák alapján)
        /// </summary>
        public void SetCountrySelections()
        {
            foreach (var country in Countries)
                country.SetLeageSelection();
        }

        private void SelectACountry(object obj)
        {
            string countryCode = obj as string;
            List<League> leagues = App.MM.LeagueRepo.GetItems(x => x.CountryCode == countryCode && x.Selected == false);
            foreach (var league in leagues) league.Selected = true;
            App.MM.LeagueRepo.UpdateItems(leagues);
        }

        private void DeselectACountry(object obj)
        {
            string countryCode = obj as string;
            List<League> leagues = App.MM.LeagueRepo.GetItems(x => x.CountryCode == countryCode && x.Selected == true);
            foreach (var league in leagues) league.Selected = false;
            App.MM.LeagueRepo.UpdateItems(leagues);
        }
    }
}

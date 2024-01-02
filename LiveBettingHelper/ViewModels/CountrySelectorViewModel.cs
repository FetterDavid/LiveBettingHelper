using CommunityToolkit.Maui.Core.Extensions;
using LiveBettingHelper.Model;
using System.Collections.ObjectModel;

namespace LiveBettingHelper.ViewModels
{
    public partial class CountrySelectorViewModel : BaseViewModel
    {
        public List<Country> Countries { get; set; }
        public Command OnSelect { get; set; }
        public Command OnDeselect { get; set; }
        public CountrySelectorViewModel()
        {
            OnSelect = new Command(SelectACountry);
            OnDeselect = new Command(DeselectACountry);
            LoadCountries();
        }

        private void LoadCountries()
        {
            Countries = App.MM.CountryRepo.GetItems().ToList();
        }

        public void SetCountrySelections()
        {
            foreach (var country in Countries)
                country.SetSelections();
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

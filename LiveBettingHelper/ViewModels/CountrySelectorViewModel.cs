using CommunityToolkit.Maui.Core.Extensions;
using LiveBettingHelper.Model;
using System.Collections.ObjectModel;

namespace LiveBettingHelper.ViewModels
{
    public class CountrySelectorViewModel : BaseViewModel
    {
        public ObservableCollection<Country> Countries { get; set; }
        public CountrySelectorViewModel()
        {
            LoadCountries();
        }

        private void LoadCountries()
        {
            Countries = App.MM.CountryRepo.GetItems().ToObservableCollection();
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using LiveBettingHelper.Model;

namespace LiveBettingHelper.ViewModels
{
    [QueryProperty(nameof(Country), "Country")]
    public partial class LeagueSelectorViewModel : BaseViewModel
    {
        [ObservableProperty]
        private List<League> _leagues;
        [ObservableProperty]
        private Country _country;

        partial void OnCountryChanged(Country value)
        {
            LoadCountries();
        }

        private void LoadCountries()
        {
            if (Country == null) App.Logger.Error("Null");
            else Leagues = App.MM.LeagueRepo.GetItems(x => x.CountryCode == Country.Code);
        }
    }
}

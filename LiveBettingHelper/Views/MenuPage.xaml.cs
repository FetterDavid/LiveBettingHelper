using LiveBettingHelper.ViewModels;

namespace LiveBettingHelper.Views;

public partial class MenuPage : ContentPage
{
    public MenuPage(MenuViewModel menuViewModel)
    {
        InitializeComponent();
        BindingContext = menuViewModel;
    }

    private async void LeagueSelectingBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CountrySelectorPage));
    }

    private async void SettingsBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SettingsPage));
    }

    private async void StaticticsBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(StatisticsPage));
    }
}
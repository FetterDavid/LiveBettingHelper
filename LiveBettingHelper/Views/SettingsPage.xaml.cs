using LiveBettingHelper.ViewModels;

namespace LiveBettingHelper.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsViewModel settingsViewModel)
    {
        InitializeComponent();
        BindingContext = settingsViewModel;
    }

    private void DefaultBetStakeEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        App.BankManager.Update();
    }
}
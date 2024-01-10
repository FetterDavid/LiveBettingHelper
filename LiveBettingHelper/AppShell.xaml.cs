using LiveBettingHelper.Utilities;
using LiveBettingHelper.ViewModels;
using LiveBettingHelper.Views;

namespace LiveBettingHelper;

public partial class AppShell : Shell
{
    public AppShell(AppShellViewModel appShellViewModel)
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(CountrySelectorPage), typeof(CountrySelectorPage));
        Routing.RegisterRoute(nameof(LeagueSelectorPage), typeof(LeagueSelectorPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(StatisticsPage), typeof(StatisticsPage));
        BindingContext = appShellViewModel;
    }
}

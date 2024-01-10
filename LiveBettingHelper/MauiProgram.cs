using LiveBettingHelper.Utilities;
using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using CommunityToolkit.Maui;
using LiveBettingHelper.Views;
using LiveBettingHelper.ViewModels;
using Microcharts.Maui;

namespace LiveBettingHelper;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
        .UseMicrocharts()
        .UseLocalNotification().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        }).UseMauiCommunityToolkit();
        // Manager injections
        builder.Services.AddSingleton<ModelManager>();
        builder.Services.AddSingleton<Logger>();
        builder.Services.AddSingleton<PopupManager>();
        builder.Services.AddSingleton<BankManager>();
        // Page injections
        builder.Services.AddSingleton<NextMachesPage>();
        builder.Services.AddSingleton<BetsPage>();
        builder.Services.AddSingleton<MenuPage>();
        builder.Services.AddTransient<CountrySelectorPage>();
        builder.Services.AddTransient<LeagueSelectorPage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<StatisticsPage>();
        // ViewModel injections
        builder.Services.AddSingleton<NextMachesViewModel>();
        builder.Services.AddSingleton<BetViewModel>();
        builder.Services.AddSingleton<MenuViewModel>();
        builder.Services.AddTransient<CountrySelectorViewModel>();
        builder.Services.AddTransient<LeagueSelectorViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();
        builder.Services.AddTransient<StatisticsViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
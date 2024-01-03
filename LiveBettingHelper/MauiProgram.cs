using LiveBettingHelper.Utilities;
using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using CommunityToolkit.Maui;
using LiveBettingHelper.Views;
using LiveBettingHelper.ViewModels;

namespace LiveBettingHelper;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
        .UseLocalNotification().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        }).UseMauiCommunityToolkit();
        builder.Services.AddSingleton<ModelManager>();
        builder.Services.AddSingleton<Logger>();
        builder.Services.AddSingleton<PopupManager>();
        builder.Services.AddTransient<LeagueSelectorPage>();
        builder.Services.AddTransient<CountrySelectorPage>();
        builder.Services.AddSingleton<NextMachesPage>();
        builder.Services.AddSingleton<NextMachesViewModel>();
        builder.Services.AddTransient<CountrySelectorViewModel>();
        builder.Services.AddTransient<LeagueSelectorViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
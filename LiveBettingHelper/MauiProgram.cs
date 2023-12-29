using LiveBettingHelper.Model;
using LiveBettingHelper.Repositories;
using LiveBettingHelper.Utilities;
using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using CommunityToolkit.Maui;

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
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
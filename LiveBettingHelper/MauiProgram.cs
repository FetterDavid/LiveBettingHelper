using LiveBettingHelper.Model;
using LiveBettingHelper.Repositories;
using LiveBettingHelper.Utilities;
using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;

namespace LiveBettingHelper;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseLocalNotification()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        // PreBet Repository
        builder.Services.AddSingleton<BaseRepository<PreBet>>();
        // BetHistory Repository
        builder.Services.AddSingleton<BaseRepository<BetHistory>>();
        // Logger
        builder.Services.AddSingleton<Logger>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

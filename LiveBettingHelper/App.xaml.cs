using LiveBettingHelper.Abstractions;
using LiveBettingHelper.Model;
using LiveBettingHelper.Repositories;
using LiveBettingHelper.Utilities;

namespace LiveBettingHelper;

public partial class App : Application
{
    public static BaseRepository<PreBet> PreBetRepo { get; set; }
    public static BaseRepository<BetHistory> BetHistoryRepo { get; set; }
    public static LastCheckRepository LastCheckRepo { get; set; }
    public static Logger Logger { get; set; }

    public App(BaseRepository<PreBet> preBetRepo, BaseRepository<BetHistory> betHistoryRepo, LastCheckRepository lastCheckRepo, Logger logger)
    {
        InitializeComponent();
        ApiManager.SetupRequestLimitTimer();
        PreBetRepo = preBetRepo;
        BetHistoryRepo = betHistoryRepo;
        LastCheckRepo = lastCheckRepo;
        Logger = logger;
        MainPage = new AppShell();
    }
}

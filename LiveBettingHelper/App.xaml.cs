using LiveBettingHelper.Abstractions;
using LiveBettingHelper.Model;
using LiveBettingHelper.Repositories;
using LiveBettingHelper.Utilities;

namespace LiveBettingHelper;

public partial class App : Application
{
    public static BaseRepository<PreBet> PreBetRepo { get; set; }
    public static BaseRepository<BetHistory> BetHistoryRepo { get; set; }
    public App(BaseRepository<PreBet> preBetRepo, BaseRepository<BetHistory> betHistoryRepo)
    {
        InitializeComponent();
        ApiManager.SetupRequestLimitTimer();
        PreBetRepo = preBetRepo;
        BetHistoryRepo = betHistoryRepo;
        MainPage = new AppShell();
    }
}

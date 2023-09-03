using LiveBettingHelper.Model;
using LiveBettingHelper.Repositories;

namespace LiveBettingHelper;

public partial class App : Application
{
    public static BaseRepository<PreBet> PreBetRepo { get; set; }
    public static BaseRepository<BetHistory> BetHistoryRepo { get; set; }
    public App(BaseRepository<PreBet> preBetRepo, BaseRepository<BetHistory> betHistoryRepo)
    {
        InitializeComponent();
        PreBetRepo = preBetRepo;
        BetHistoryRepo = betHistoryRepo;
        MainPage = new AppShell();
    }
}

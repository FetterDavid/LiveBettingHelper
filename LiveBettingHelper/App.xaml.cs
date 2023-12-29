using LiveBettingHelper.Abstractions;
using LiveBettingHelper.Model;
using LiveBettingHelper.Repositories;
using LiveBettingHelper.Utilities;

namespace LiveBettingHelper;

public partial class App : Application
{
    public static ModelManager MM { get; set; } = new();
    public static Logger Logger { get; set; }
    public static PopupManager PopupManager { get; set; }

    public App(ModelManager mm, Logger logger, PopupManager popupManager)
    {
        InitializeComponent();
        ApiManager.SetupRequestLimitTimer();
        MM = mm;
        Logger = logger;
        PopupManager = popupManager;
        MainPage = new AppShell();
        StartSetup();
    }

    private void StartSetup()
    {
        DateTime limitDate = DateTime.Now.AddDays(-2);
        MM.CheckedMatchRepo.DeleteItems(MM.CheckedMatchRepo.GetItems(x => x.CheckDate < limitDate));
    }
}

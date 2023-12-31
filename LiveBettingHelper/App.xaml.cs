using LiveBettingHelper.Model;
using LiveBettingHelper.Services;
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

    private async Task StartSetup()
    {
        // Vizsgált mecsek időszakos törlése
        DateTime limitDate = DateTime.Now.AddDays(-2);
        MM.CheckedMatchRepo.DeleteItems(MM.CheckedMatchRepo.GetItems(x => x.CheckDate < limitDate));
        // Ország check
        LastCheck lastCountryCheck = MM.LastCheckRepo.GetLastCheck(CheckType.CountryCheck);
        if (lastCountryCheck == null || lastCountryCheck.CheckDate < DateTime.Now.AddDays(-7))
        {
            MM.CountryRepo.DeleteItems(MM.CountryRepo.GetItems());
            MM.CountryRepo.AddItems(await CountryService.GetAllCountriesAsync());
            MM.LastCheckRepo.SetLastCheck(CheckType.CountryCheck);
            MM.LeagueRepo.DeleteItems(MM.LeagueRepo.GetItems());
            MM.LeagueRepo.AddItems(await LeaguesService.GetAllLeaguesAsync());
            MM.LastCheckRepo.SetLastCheck(CheckType.LeagueCheck);
        }
    }
}

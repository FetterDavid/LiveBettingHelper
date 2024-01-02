using LiveBettingHelper.Model;
using LiveBettingHelper.Services;
using LiveBettingHelper.Utilities;
using LiveBettingHelper.Views;
using LiveBettingHelper.Views.Popups;

namespace LiveBettingHelper;

public partial class App : Application
{
    /// <summary>
    /// A SQLite repository-kat összegyüjtő osztály
    /// </summary>
    public static ModelManager MM { get; set; } = new();
    /// <summary>
    /// A saját Logger-ünk
    /// </summary>
    public static Logger Logger { get; set; }
    /// <summary>
    /// A popup-okat kezelő osztály
    /// </summary>
    public static PopupManager PopupManager { get; set; }

    public App(ModelManager mm, Logger logger, PopupManager popupManager)
    {
        InitializeComponent();
        ApiManager.SetupRequestLimitTimer();
        MM = mm;
        Logger = logger;
        PopupManager = popupManager;
        MainPage = new SplashScreenPage();
        _ = LoadDataAsync();
    }
    /// <summary>
    /// Betölti mindent a SplashScreen-en majd tovább lép az AppShell-re
    /// </summary>
    private async Task LoadDataAsync()
    {
        try
        {
            StartSetup();
            await UpdateCountriesAndLeagues();
        }
        catch (Exception ex)
        {
            App.Logger.Exception(ex);
        }
        finally
        {
            App.Logger.SetCaption("");
            MainPage = new AppShell();
        }

    }
    /// <summary>
    /// Az alkalmazás inditásakor elvégezendő feladatok
    /// </summary>
    private void StartSetup()
    {
        // Vizsgált mecsek időszakos törlése
        App.Logger.SetCaption("Start Setup...");
        DateTime limitDate = DateTime.Now.AddDays(-2);
        MM.CheckedMatchRepo.DeleteItems(MM.CheckedMatchRepo.GetItems(x => x.CheckDate < limitDate));
    }
    /// <summary>
    /// Frissíti az országojat és a ligákat 
    /// </summary>
    /// <returns></returns>
    private async Task UpdateCountriesAndLeagues()
    {
        LastCheck lastCountryCheck = App.MM.LastCheckRepo.GetLastCheck(CheckType.CountryCheck);
        if (lastCountryCheck == null || lastCountryCheck.CheckDate < DateTime.Now.AddMonths(-3))
        {
            App.Logger.SetCaption("Update Countries...");
            App.MM.CountryRepo.DeleteItems(App.MM.CountryRepo.GetItems());
            App.MM.CountryRepo.AddItems(await CountryService.GetAllCountriesAsync());
            App.MM.LastCheckRepo.SetLastCheck(CheckType.CountryCheck);
            App.Logger.SetCaption("Update Leagues...");
            App.MM.LeagueRepo.DeleteItems(App.MM.LeagueRepo.GetItems());
            App.MM.LeagueRepo.AddItems(await LeaguesService.GetAllLeaguesAsync());
            App.MM.LastCheckRepo.SetLastCheck(CheckType.LeagueCheck);
        }
    }
}

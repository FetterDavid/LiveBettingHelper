using LiveBettingHelper.Model;
using LiveBettingHelper.Services;
using LiveBettingHelper.Utilities;
using LiveBettingHelper.ViewModels;
using LiveBettingHelper.Views.Popups;

namespace LiveBettingHelper.Views;

public partial class LiveMatchesPage : ContentPage
{
    private LiveMatchesPageModel _model;
    private Timer _timer;
    public LiveMatchesPage()
    {
        InitializeComponent();
        this._model = new LiveMatchesPageModel();
        BindingContext = _model;
        StartTimer();
    }

    private void StartTimer()
    {
        _timer = new Timer(ReloadLiveMatches, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
    }

    private void ReloadLiveMatches(object state)
    {
        _ = _model.ReloadDesiredLiveMatches();
    }

    private async void LiveMatch_Tapped(object sender, TappedEventArgs e)
    {
        LiveMatch match = ((VisualElement)sender).BindingContext as LiveMatch;
        if (match == null) return;
        // fogad�si tipus ellem�rz�se
        List<BetType> betTypes = match.GetPossibleBetTypes();
        if (betTypes.Count == 0)
        {
            App.PopupManager.ShowPopup(new InfoPopup("There are no suitable betting options."));
            return;
        }
        // odds ellen�rz�se
        double odds = 0;
        foreach (BetType betType in betTypes)
        {
            odds = await OddsService.GetOdsByBetType(match, betTypes[0]);
            if (odds > 0) break;
        }
        if (odds == 0)
        {
            App.PopupManager.ShowPopup(new InfoPopup("There is no available odds for tha match."));
            return;
        }
        BetType bt = betTypes[0];
        PreBet preBet = App.MM.PreBetRepo.GetItem(x => x.FixtureId == match.Id && x.BettingType == bt);
        if (preBet == null)
        {
            App.Logger.Error("There is no available prebet for tha match.");
            return;
        }
        // fogad�s l�trehozz�sa
        Bet bet = new Bet
        {
            FixtureId = match.Id,
            LeagueId = match.LeagueId,
            LeagueName = match.LeagueName,
            LeagueCountry = match.LeagueCountry,
            LeagueSeason = match.LeagueSeason,
            HomeTeamId = match.HomeTeamId,
            HomeTeamName = match.HomeTeamName,
            AwayTeamId = match.AwayTeamId,
            AwayTeamName = match.AwayTeamName,
            Date = match.Date,
            Odds = odds,
            BetMinute = match.ElapsedTime,
            Probability = preBet.Probability,
            BetValue = App.SettingsManager.MySettings.DefaultBetStake,
            BettingType = preBet.BettingType
        };
        App.PopupManager.ShowPopup(new BetPopup(bet));

    }
}
using LiveBettingHelper.Model;
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
        this._model = new LiveMatchesPageModel(App.MM.PreBetRepo);
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

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        LiveMatch match = ((VisualElement)sender).BindingContext as LiveMatch;
        List<BetType> betTypes = match.GetPossibleBetTypes();
        if (betTypes.Count == 0) App.PopupManager.ShowPopup(new InfoPopup("Nincs megfelelõ fogadási lehetõség"));
        else
        {
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
                Odds = 1.36,
                BetValue = App.BankManager.MyBank.DefaultBetStake,
                BettingType = betTypes[0]
            };
            App.PopupManager.ShowPopup(new BetPopup(bet));
        }
    }
}
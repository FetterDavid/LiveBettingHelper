using LiveBettingHelper.ViewModels;

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
        _model.ReloadDesiredLiveMatches();
    }
}
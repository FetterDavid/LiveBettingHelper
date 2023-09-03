using LiveBettingHelper.Utilities;
using LiveBettingHelper.ViewModels;

namespace LiveBettingHelper.Views;

public partial class LiveMatchesPage : ContentPage
{
    private LiveMatchesPageModel _model;
    private Timer _timer;
    public LiveMatchesPage()
    {
        InitializeComponent();
        this._model = new LiveMatchesPageModel(App.PreBetRepo);
        BindingContext = _model;
        StartTimer();
    }

    private void StartTimer()
    {
        // Create a timer that calls the ReloadDesiredLiveMatches method every minute
        _timer = new Timer(ReloadLiveMatches, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
    }

    private void ReloadLiveMatches(object state)
    {
        // Call the ReloadDesiredLiveMatches method of the _model
        _model.ReloadDesiredLiveMatches();
    }
}
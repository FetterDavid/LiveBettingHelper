using LiveBettingHelper.Model;
using LiveBettingHelper.ViewModels;

namespace LiveBettingHelper.Views;

public partial class LiveFixtureDetailsPage : ContentPage
{
    private LiveFixtureDetailsPageModel _model;
    public LiveFixtureDetailsPage(LiveMatch match)
    {
        InitializeComponent();
        this._model = new LiveFixtureDetailsPageModel(match);
    }
}
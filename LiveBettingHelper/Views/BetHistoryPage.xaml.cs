using LiveBettingHelper.Abstractions;
using LiveBettingHelper.ViewModels;

namespace LiveBettingHelper.Views;

public partial class BetHistoryPage : ContentPage
{
    private BetHistoryPageModel _model;
    public BetHistoryPage()
    {
        InitializeComponent();
        this._model = new BetHistoryPageModel(App.BetHistoryRepo, App.PreBetRepo);
        BindingContext = _model;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _model.CheckPreBetsAndLoad();
    }
}
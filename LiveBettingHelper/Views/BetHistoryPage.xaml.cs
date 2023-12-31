using LiveBettingHelper.ViewModels;

namespace LiveBettingHelper.Views;

public partial class BetHistoryPage : ContentPage
{
    private BetHistoryViewModel _model;
    public BetHistoryPage()
    {
        InitializeComponent();
        this._model = new BetHistoryViewModel(App.MM.BetHistoryRepo, App.MM.PreBetRepo);
        BindingContext = _model;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _model.CheckPreBetsAndLoad();
    }
}
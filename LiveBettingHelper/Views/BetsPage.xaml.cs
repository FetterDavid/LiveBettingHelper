using LiveBettingHelper.Model;
using LiveBettingHelper.Services;
using LiveBettingHelper.ViewModels;

namespace LiveBettingHelper.Views;

public partial class BetsPage : ContentPage
{
    private BetViewModel _viewModel;

    public BetsPage(BetViewModel betViewModel)
    {
        InitializeComponent();
        _viewModel = betViewModel;
        BindingContext = _viewModel;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _viewModel.ReloadBets(); // fire and forget
    }
}
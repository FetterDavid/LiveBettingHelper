using LiveBettingHelper.Model;
using LiveBettingHelper.Utilities;
using LiveBettingHelper.ViewModels;

namespace LiveBettingHelper.Views;

public partial class NextMachesPage : ContentPage
{
    private NextMachesViewModel _viewModel;
    public NextMachesPage(NextMachesViewModel nextMachesViewModel)
    {
        InitializeComponent();
        _viewModel = nextMachesViewModel;
        BindingContext = _viewModel;
        _ = _viewModel.RecheckAsync(); // fire and forget
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}
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
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _viewModel.Recheck(); // fire and forget
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Frame frame = (Frame)sender;
        PreBet preBet = (PreBet)frame.BindingContext;
        Static.CreateNotificationRequest(preBet.FixtureId, "Fogadási lehetõség", $"{preBet.HomeTeamName} - {preBet.AwayTeamName}");
    }
}
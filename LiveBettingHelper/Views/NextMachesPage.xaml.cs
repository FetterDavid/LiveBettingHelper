using LiveBettingHelper.Model;
using LiveBettingHelper.Utilities;
using LiveBettingHelper.ViewModels;

namespace LiveBettingHelper.Views;

public partial class NextMachesPage : ContentPage
{
    private NextMachesPageModel _model;
    public NextMachesPage()
    {
        InitializeComponent();
        _model = new NextMachesPageModel(App.PreBetRepo);
        BindingContext = _model;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _model.Reload();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Frame frame = (Frame)sender;
        PreBet preBet = (PreBet)frame.BindingContext;
        Static.CreateNotificationRequest(preBet.FixtureId, "Fogadási lehetõség", $"{preBet.HomeTeamName} - {preBet.AwayTeamName}");
    }
}
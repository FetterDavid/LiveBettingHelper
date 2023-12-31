using LiveBettingHelper.Model;
using LiveBettingHelper.Utilities;
using LiveBettingHelper.ViewModels;

namespace LiveBettingHelper.Views;

public partial class NextMachesPage : ContentPage
{
    private NextMachesViewModel _model;
    public NextMachesPage()
    {
        InitializeComponent();
        _model = new NextMachesViewModel();
        BindingContext = _model;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Frame frame = (Frame)sender;
        PreBet preBet = (PreBet)frame.BindingContext;
        Static.CreateNotificationRequest(preBet.FixtureId, "Fogad�si lehet�s�g", $"{preBet.HomeTeamName} - {preBet.AwayTeamName}");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _model.Reload();// Fire and forget approach 
    }
}
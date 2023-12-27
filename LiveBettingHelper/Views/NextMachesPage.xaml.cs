using CommunityToolkit.Maui.Views;
using LiveBettingHelper.Model;
using LiveBettingHelper.Utilities;
using LiveBettingHelper.ViewModels;
using LiveBettingHelper.Views.Popups;

namespace LiveBettingHelper.Views;

public partial class NextMachesPage : ContentPage
{
    private NextMachesPageModel _model;
    public NextMachesPage()
    {
        InitializeComponent();
        _model = new NextMachesPageModel();
        BindingContext = _model;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Frame frame = (Frame)sender;
        PreBet preBet = (PreBet)frame.BindingContext;
        Static.CreateNotificationRequest(preBet.FixtureId, "Fogadási lehetõség", $"{preBet.HomeTeamName} - {preBet.AwayTeamName}");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _model.Reload();// Fire and forget approach 
    }
}
using CommunityToolkit.Maui.Views;
using LiveBettingHelper.Model;

namespace LiveBettingHelper.Views.Popups;

public partial class BetPopup : Popup
{
    private Bet Bet { get; set; }
    public BetPopup(Bet bet)
    {
        InitializeComponent();
        this.Bet = bet;
        Bet.SetPossibleWinning();
        BindingContext = Bet;
    }

    private void CloseBtn_Clicked(object sender, EventArgs e)
    {
        Close();
    }

    private void CorrectPossibleWinning(object sender, TextChangedEventArgs e)
    {
        Bet.SetPossibleWinning();
    }

    private void BetBtn_Clicked(object sender, EventArgs e)
    {
        App.MM.BetRepo.AddItem(Bet);
        Close();
    }
}
using LiveBettingHelper.ViewModels;

namespace LiveBettingHelper.Views;

public partial class LeagueSelectorPage : ContentPage
{
    public LeagueSelectorPage(LeagueSelectorViewModel leagueSelectorViewModel)
    {
        InitializeComponent();
        BindingContext = leagueSelectorViewModel;
    }
}
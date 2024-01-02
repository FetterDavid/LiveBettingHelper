using LiveBettingHelper.Model;
using LiveBettingHelper.ViewModels;

namespace LiveBettingHelper.Views;

public partial class LeagueSelectorPage : ContentPage
{
    public LeagueSelectorPage(LeagueSelectorViewModel leagueSelectorViewModel)
    {
        InitializeComponent();
        BindingContext = leagueSelectorViewModel;
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        League league = ((VisualElement)sender).BindingContext as League;
        App.MM.LeagueRepo.UpdateItem(league);
    }
}
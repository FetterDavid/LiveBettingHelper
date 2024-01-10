using LiveBettingHelper.ViewModels;
using LiveBettingHelper.Views.Popups;

namespace LiveBettingHelper.Views;

public partial class StatisticsPage : ContentPage
{
    public StatisticsPage(StatisticsViewModel statisticsViewModel)
    {
        InitializeComponent();
        BindingContext = statisticsViewModel;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        App.PopupManager.ShowPopup(new InfoPopup("Ez egy teszt"));
    }
    private void TapGestureRecognizer_Tapped2(object sender, TappedEventArgs e)
    {
        App.PopupManager.ShowPopup(new InfoPopup("Ez egy másik teszt"));
    }
}
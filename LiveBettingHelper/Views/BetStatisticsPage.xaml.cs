using LiveBettingHelper.ViewModels;

namespace LiveBettingHelper.Views;

public partial class BetStatisticsPage : ContentPage
{
    public BetStatisticsPage(BetStatisticsViewModel betStatisticsViewModel)
    {
        InitializeComponent();
        BindingContext = betStatisticsViewModel;
    }
}
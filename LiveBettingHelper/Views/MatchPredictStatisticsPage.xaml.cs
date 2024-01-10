using LiveBettingHelper.ViewModels;

namespace LiveBettingHelper.Views;

public partial class MatchPredictStatisticsPage : ContentPage
{
    public MatchPredictStatisticsPage(MatchPredictStatisticsViewModel matchPredictStatisticsViewModel)
    {
        InitializeComponent();
        BindingContext = matchPredictStatisticsViewModel;
    }
}
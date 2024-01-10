using LiveBettingHelper.ViewModels;

namespace LiveBettingHelper.Views;

public partial class StatisticsPage : ContentPage
{
    public StatisticsPage(StatisticsViewModel statisticsViewModel)
    {
        InitializeComponent();
        BindingContext = statisticsViewModel;
    }

    private async void BetChart_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(BetStatisticsPage));
    }

    private async void MatchPredictChart_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MatchPredictStatisticsPage));
    }

    private async void BankChart_TappedAsync(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(BankStatisticsPage));
    }
}
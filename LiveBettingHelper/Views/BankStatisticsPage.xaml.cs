using LiveBettingHelper.ViewModels;

namespace LiveBettingHelper.Views;

public partial class BankStatisticsPage : ContentPage
{
    public BankStatisticsPage(BankStatisticsViewModel bankStatisticsViewModel)
    {
        InitializeComponent();
        BindingContext = bankStatisticsViewModel;
    }
}
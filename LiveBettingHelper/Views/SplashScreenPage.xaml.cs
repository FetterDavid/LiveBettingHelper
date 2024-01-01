namespace LiveBettingHelper.Views;

public partial class SplashScreenPage : ContentPage
{
    public SplashScreenPage()
    {
        InitializeComponent();
        BindingContext = App.Logger;
    }
}
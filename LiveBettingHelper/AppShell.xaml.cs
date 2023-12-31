using LiveBettingHelper.Views;

namespace LiveBettingHelper;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(LeagueSelectorPage), typeof(LeagueSelectorPage));
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {

    }
}

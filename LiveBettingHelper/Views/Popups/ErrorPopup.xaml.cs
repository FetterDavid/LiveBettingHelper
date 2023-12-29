using CommunityToolkit.Maui.Views;
using LiveBettingHelper.Utilities;

namespace LiveBettingHelper.Views.Popups;

public partial class ErrorPopup : Popup
{
    public string Message { get; set; }
    public ErrorPopup(string message)
    {
        InitializeComponent();
        Message = message;
        BindingContext = this;
    }

    private void CloseBtn_Clicked(object sender, EventArgs e)
    {
        Close();
    }

    private async void SendBtn_Clicked(object sender, EventArgs e)
    {
        await Static.SendEmailManual("Live Betting Helper Error", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + "\n" + Message);
    }
}
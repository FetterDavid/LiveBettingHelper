using CommunityToolkit.Maui.Views;

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
}
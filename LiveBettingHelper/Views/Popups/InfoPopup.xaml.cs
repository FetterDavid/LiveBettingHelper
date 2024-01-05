using CommunityToolkit.Maui.Views;

namespace LiveBettingHelper.Views.Popups;

public partial class InfoPopup : Popup
{
    public string Message { get; set; }
    public InfoPopup(string message)
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
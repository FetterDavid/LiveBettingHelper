using CommunityToolkit.Maui.Views;
using LiveBettingHelper.Utilities;

namespace LiveBettingHelper.Views.Popups;

public partial class LoadingPopup : Popup
{
    public LoadingPopup()
    {
        InitializeComponent();
        BindingContext = App.Logger;
    }

    private void LoadingBar_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (App.Logger.Progress == 1)
        {
            LoadingBar.PropertyChanged -= LoadingBar_PropertyChanged;
            Close();
        }
    }
}
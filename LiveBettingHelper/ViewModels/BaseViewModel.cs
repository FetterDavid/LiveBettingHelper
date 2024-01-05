using CommunityToolkit.Mvvm.ComponentModel;

namespace LiveBettingHelper.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy = true;
    }
}

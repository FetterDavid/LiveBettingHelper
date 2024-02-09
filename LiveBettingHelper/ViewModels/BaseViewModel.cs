using CommunityToolkit.Mvvm.ComponentModel;

namespace LiveBettingHelper.ViewModels
{
    /// <summary>
    /// Minden ViewModel osztálynak ebből az osályból kell származnia
    /// </summary>
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy = true;
    }
}

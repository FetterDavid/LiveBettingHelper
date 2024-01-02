using LiveBettingHelper.Model;
using LiveBettingHelper.ViewModels;

namespace LiveBettingHelper.Views;

public partial class CountrySelectorPage : ContentPage
{
    private CountrySelectorViewModel _model;

    public CountrySelectorPage(CountrySelectorViewModel countrySelectorViewModel)
    {
        InitializeComponent();
        _model = countrySelectorViewModel;
        BindingContext = _model;
    }

    private async void Country_Tapped(object sender, TappedEventArgs e)
    {
        Country country = ((VisualElement)sender).BindingContext as Country;
        if (country == null) return;
        await Shell.Current.GoToAsync(nameof(LeagueSelectorPage), true, new Dictionary<string, object>
        {
            { "Country", country}
        });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _model.SetCountrySelections();
    }
}
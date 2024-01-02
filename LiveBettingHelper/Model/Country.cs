using CommunityToolkit.Mvvm.ComponentModel;
using LiveBettingHelper.Utilities;
using SQLite;

namespace LiveBettingHelper.Model
{
    public partial class Country
    {
        [ObservableProperty]
        private bool _isSelected;
        [ObservableProperty]
        private bool _isPartiallySelected;
        public void SetSelections()
        {
            string name = Name;
            SelectType leaguesSelectType = GetLeaguesSelectType();
            IsSelected = leaguesSelectType == SelectType.Selected;
            IsPartiallySelected = leaguesSelectType == SelectType.PartiallySelected;
        }

        private SelectType GetLeaguesSelectType()
        {
            List<League> leagues = App.MM.LeagueRepo.GetItems(x => x.CountryCode == Code);
            if (leagues.All(x => x.Selected)) return SelectType.Selected;
            if (leagues.Any(x => x.Selected)) return SelectType.PartiallySelected;
            return SelectType.NotSelected;
        }
    }
}

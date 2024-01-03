using CommunityToolkit.Mvvm.ComponentModel;
using LiveBettingHelper.Utilities;
using SQLite;

namespace LiveBettingHelper.Model
{
    public partial class Country
    {
        [ObservableProperty]
        private SelectType _checkType;
        /// <summary>
        /// Újratölti az országok kijelölésének tipusát (a kiválasztott ligák alapján)
        /// </summary>
        public void SetLeageSelection()
        {
            CheckType = GetLeaguesSelectType();
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

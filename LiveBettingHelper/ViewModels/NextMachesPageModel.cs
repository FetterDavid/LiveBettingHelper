using LiveBettingHelper.Model;
using LiveBettingHelper.Repositories;
using LiveBettingHelper.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.ViewModels
{
    public class NextMachesPageModel
    {
        public ObservableCollection<PreBet> PreBets { get; set; } = new();

        public NextMachesPageModel()
        {

        }

        public void Reload()
        {
            // TODO ez a feltétel így nem jó
            if (!App.PreBetRepo.GetItems().Any(x => x.Date.Date == DateTime.Now.Date)) SaveAndLoadTodayMatches();
            else LoadTodayMatches();
        }

        private async void SaveAndLoadTodayMatches()
        {
            await SaveDesiredPreMatches(await ApiManager.GetTodayMatchesAsync());
            LoadTodayMatches();
        }

        private void LoadTodayMatches()
        {
            PreBets.Clear();
            List<PreBet> matches = App.PreBetRepo.GetItems().Where(x => x.Date.Date == DateTime.Now.Date).OrderBy(x => x.Date).ToList();
            foreach (PreBet match in matches)
            {
                PreBets.Add(match);
            }
        }

        private async Task SaveDesiredPreMatches(IEnumerable<PreMatch> matches)
        {
            List<Task> tasks = new List<Task>();
            foreach (PreMatch match in matches)
            {
                tasks.Add(Task.Run(async () =>
                {
                    if (match.Date.AddHours(1) < DateTime.Now) return;
                    if (await ApiManager.CanBetOnMatchAsync(match.Id) == false) return;// ha nem lehet a mecsre fogani akkor nem is nézzük tovább           
                    (double, double) homeHalfOvers = await ApiManager.GetFirstAndSecondHalfPercentAsync(match.LeagueId, match.LeagueSeason, match.HomeTeamId, "home");
                    if (homeHalfOvers.Item1 < 79 && homeHalfOvers.Item2 < 79) return;// ha rossz a hazai csapat akkor nem is nézük tovább (sporulunk egy lekérdezést)
                    match.HomeTeamFHOverPercent = homeHalfOvers.Item1;
                    match.HomeTeamSHOverPercent = homeHalfOvers.Item2;
                    (double, double) awayHalfOvers = await ApiManager.GetFirstAndSecondHalfPercentAsync(match.LeagueId, match.LeagueSeason, match.AwayTeamId, "away");
                    match.AwayTeamSHOverPercent = awayHalfOvers.Item2;
                    if (awayHalfOvers.Item1 > 79 && homeHalfOvers.Item1 > 79)// első félídő over
                    {
                        double probability = (awayHalfOvers.Item1 + homeHalfOvers.Item1) / 2;
                        App.PreBetRepo.AddItem(Static.ConvertPreMatchToPreBet(match, BetType.FirstHalfOver, probability));
                    }
                    if (awayHalfOvers.Item2 > 79 && homeHalfOvers.Item2 > 79)// második félidő over
                    {
                        double probability = (awayHalfOvers.Item2 + homeHalfOvers.Item2) / 2;
                        App.PreBetRepo.AddItem(Static.ConvertPreMatchToPreBet(match, BetType.SecondHalfOver, probability));
                    }
                }));
            }
            await Task.WhenAll(tasks);
        }
    }
}

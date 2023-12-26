﻿using LiveBettingHelper.Model;
using LiveBettingHelper.Repositories;
using LiveBettingHelper.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LiveBettingHelper.ViewModels
{
    public class NextMachesPageModel
    {
        public ObservableCollection<PreBet> PreBets { get; set; } = new();
        private int _checkedMatches;

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
            try
            {
                List<Task> tasks = new List<Task>();
                foreach (PreMatch match in matches)
                {
                    tasks.Add(GetMatchCheckingTask(match, matches.Count()));
                }
                await Task.WhenAll(tasks);
                App.Logger.SetProgress(1, 1);
                App.Logger.Info($"{App.Logger.Progress}");
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, "Exception in SaveDesiredPreMatches");
            }
        }

        private Task GetMatchCheckingTask(PreMatch match, int matchesCount)
        {
            return Task.Run(async () =>
            {
                if (match.Date.AddHours(1) > DateTime.Now) // Ha már vége a mecsnek akkor nem nézzük (TODO miért van itt egyáltalán ilyen)
                {
                    if (await ApiManager.CanBetOnMatchAsync(match.Id))// ha nem lehet a mecsre fogani akkor nem is nézzük tovább           
                    {
                        (double, double) homeHalfOvers = await ApiManager.GetFirstAndSecondHalfPercentAsync(match.LeagueId, match.LeagueSeason, match.HomeTeamId, "home");
                        if (homeHalfOvers.Item1 >= 80 && homeHalfOvers.Item2 >= 80)// ha rossz a hazai csapat akkor nem is nézük tovább
                        {
                            match.HomeTeamFHOverPercent = homeHalfOvers.Item1;
                            match.HomeTeamSHOverPercent = homeHalfOvers.Item2;
                            (double, double) awayHalfOvers = await ApiManager.GetFirstAndSecondHalfPercentAsync(match.LeagueId, match.LeagueSeason, match.AwayTeamId, "away");
                            match.AwayTeamSHOverPercent = awayHalfOvers.Item2;
                            if (awayHalfOvers.Item1 >= 80 && homeHalfOvers.Item1 >= 80)// első félídő over
                            {
                                double probability = (awayHalfOvers.Item1 + homeHalfOvers.Item1) / 2;
                                App.PreBetRepo.AddItem(Static.ConvertPreMatchToPreBet(match, BetType.FirstHalfOver, probability));
                            }
                            if (awayHalfOvers.Item2 >= 80 && homeHalfOvers.Item2 >= 80)// második félidő over
                            {
                                double probability = (awayHalfOvers.Item2 + homeHalfOvers.Item2) / 2;
                                App.PreBetRepo.AddItem(Static.ConvertPreMatchToPreBet(match, BetType.SecondHalfOver, probability));
                            }
                        }
                    }
                }
                _checkedMatches++;
                App.Logger.SetProgress(_checkedMatches, matchesCount);
                App.Logger.Info($"{App.Logger.Progress}");
            });
        }
    }
}

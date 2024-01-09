﻿using CommunityToolkit.Mvvm.ComponentModel;
using LiveBettingHelper.Model;
using LiveBettingHelper.Services;
using LiveBettingHelper.Utilities;
using LiveBettingHelper.Views.Popups;

namespace LiveBettingHelper.ViewModels
{
    public partial class NextMachesViewModel : BaseViewModel
    {
        private const double MIN_STAT = 65;
        [ObservableProperty]
        private List<PreBet> _preBets;
        private int _checkedMatches;

        public NextMachesViewModel()
        {
            PreBets = new();
        }

        public async Task Recheck()
        {
            IsBusy = true;
            LastCheck lastCheck = App.MM.LastCheckRepo.GetLastCheck(CheckType.NextMatchesCheck);
            await ArchiveFinishedPreBets();
            if (lastCheck == null || lastCheck.CheckDate.AddHours(1) < DateTime.Now)
            {
                await CheckTodayMatches();
                App.MM.LastCheckRepo.SetLastCheck(CheckType.NextMatchesCheck);
            }
            LoadNextMatches();
            IsBusy = false;
        }

        private async Task CheckTodayMatches()
        {
            try
            {
                App.Logger.SetProgress(0);
                App.Logger.SetCaption("Check Next Matches...");
                App.PopupManager.ShowPopup(new LoadingPopup());
                HashSet<int> checkedMatchIds = App.MM.CheckedMatchRepo.GetItems().Select(x => x.FixtureId).ToHashSet();
                IEnumerable<PreMatch> matches = await PreMatchService.GetNext24HourMatchesAsync();
                await SaveDesiredPreMatches(matches.Where(x => !checkedMatchIds.Contains(x.Id)));
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, "Exception in SaveDesiredPreMatches");
            }
            finally
            {
                App.Logger.SetProgress(1, 1);
                App.Logger.SetCaption("");
                App.Logger.SetSubCaption("");
            }
        }

        private async Task SaveDesiredPreMatches(IEnumerable<PreMatch> matches)
        {
            _checkedMatches = 0;
            App.Logger.SetSubCaption($"({_checkedMatches}/{matches.Count()})");
            List<Task> tasks = new List<Task>();
            foreach (PreMatch match in matches)
            {
                tasks.Add(GetMatchCheckingTask(match, matches.Count()));
            }
            await Task.WhenAll(tasks);
        }

        private Task GetMatchCheckingTask(PreMatch match, int matchesCount)
        {
            return Task.Run(async () =>
            {
                (double, double) homeHalfOvers = await StatisticsService.GetFirstAndSecondHalfPercentAsync(match.LeagueId, match.LeagueSeason, match.HomeTeamId, "home");
                if (homeHalfOvers.Item1 >= MIN_STAT || homeHalfOvers.Item2 >= MIN_STAT)// ha legalább az egyik félidpre jó a hazai csapat csak akkor nézük tovább
                {
                    match.HomeTeamFHOverPercent = homeHalfOvers.Item1;
                    match.HomeTeamSHOverPercent = homeHalfOvers.Item2;
                    (double, double) awayHalfOvers = await StatisticsService.GetFirstAndSecondHalfPercentAsync(match.LeagueId, match.LeagueSeason, match.AwayTeamId, "away");
                    match.AwayTeamSHOverPercent = awayHalfOvers.Item2;
                    if (awayHalfOvers.Item1 >= MIN_STAT && homeHalfOvers.Item1 >= MIN_STAT)// első félídő over
                    {
                        double probability = (awayHalfOvers.Item1 + homeHalfOvers.Item1) / 2;
                        App.MM.PreBetRepo.AddItem(Static.ConvertPreMatchToPreBet(match, BetType.FirstHalfOver, probability));
                    }
                    if (awayHalfOvers.Item2 >= MIN_STAT && homeHalfOvers.Item2 >= MIN_STAT)// második félidő over
                    {
                        double probability = (awayHalfOvers.Item2 + homeHalfOvers.Item2) / 2;
                        App.MM.PreBetRepo.AddItem(Static.ConvertPreMatchToPreBet(match, BetType.SecondHalfOver, probability));
                    }
                }
                App.MM.CheckedMatchRepo.AddItem(new CheckedMatch { FixtureId = match.Id, CheckDate = DateTime.Now });
                _checkedMatches++;
                App.Logger.SetProgress(_checkedMatches, matchesCount);
                App.Logger.SetSubCaption($"({_checkedMatches}/{matchesCount})");
            });
        }

        private async Task ArchiveFinishedPreBets()
        {
            try
            {
                List<PreBet> matches = App.MM.PreBetRepo.GetItems(x => x.Date < DateTime.Now).OrderBy(x => x.Date).ToList();
                foreach (PreBet match in matches)
                {
                    if (await MatchResultService.IsMatchFinished(match.FixtureId))
                        await match.Archive();
                    else if (match.Date.AddHours(3) < DateTime.Now) // ha azt kapjuk hogy még nem fejezöfött de már bekellett volna (kezdés után 3 óra) akkor töröljük
                    {
                        App.MM.CheckedMatchRepo.DeleteItem(App.MM.CheckedMatchRepo.GetItem(x => x.FixtureId == match.FixtureId));//A CheckedMatch-et is töröljük ha estleg ellet halasztva akkor úja feltudjuk venni
                        App.MM.PreBetRepo.DeleteItem(match);
                    }
                }
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, "Exception in ArchiveFinishedPreBets");
            }
        }

        private void LoadNextMatches()
        {
            try
            {
                PreBets.Clear();
                List<PreBet> matches = App.MM.PreBetRepo.GetItems().OrderBy(x => x.Date).ToList();
                foreach (PreBet match in matches)
                {
                    PreBets.Add(match);
                }
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, "Exception in LoadTodayMatches");
            }
        }
    }
}

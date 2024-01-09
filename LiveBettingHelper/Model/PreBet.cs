using LiveBettingHelper.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Model
{
    public partial class PreBet
    {
        [Ignore]
        public string StartStr => Date.ToString("HH:mm");

        public async Task Archive()
        {
            ArchivedPreBet archivedPreBet = new ArchivedPreBet
            {
                BettingType = this.BettingType,
                Probability = this.Probability,
                FixtureId = this.FixtureId,
                LeagueId = this.LeagueId,
                LeagueName = this.LeagueName,
                LeagueCountry = this.LeagueCountry,
                LeagueSeason = this.LeagueSeason,
                HomeTeamId = this.HomeTeamId,
                HomeTeamName = this.HomeTeamName,
                AwayTeamId = this.AwayTeamId,
                AwayTeamName = this.AwayTeamName,
                Date = this.Date
            };
            archivedPreBet.IsWon = await MatchResultService.GetOutcome(FixtureId, BettingType);
            App.MM.ArchivedPreBetRepo.AddItem(archivedPreBet);
            App.MM.PreBetRepo.DeleteItem(this);
        }
    }
}

using LiveBettingHelper.Model.ApiSchemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Model
{
    public partial class PreMatch
    {
        public PreMatch()
        {
        }

        public PreMatch(FixtureObject fixture)
        {
            Id = fixture.fixture.id;
            LeagueId = fixture.league.id;
            LeagueName = fixture.league.name;
            LeagueCountry = fixture.league.country;
            LeagueSeason = fixture.league.season;
            HomeTeamId = fixture.teams.home.id;
            HomeTeamName = fixture.teams.home.name;
            AwayTeamId = fixture.teams.away.id;
            AwayTeamName = fixture.teams.away.name;
            Date = fixture.fixture.date;
        }
    }
}

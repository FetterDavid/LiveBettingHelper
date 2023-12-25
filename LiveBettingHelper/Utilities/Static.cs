using LiveBettingHelper.Model;
using Plugin.LocalNotification;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Utilities
{
    public class Static
    {
        /// <summary>
        /// Az adatbázis file neve
        /// </summary>
        private const string DB_FILE_NAME = "bet.db3";
        /// <summary>
        /// Az adatbázis Flag-ek
        /// </summary>
        public const SQLiteOpenFlags DBFlags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
        /// <summary>
        /// Az adatbázis elérési útvonala
        /// </summary>
        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DB_FILE_NAME);
        /// <summary>
        /// Egy PreBet objektumot BetHistory objektumá konvertál
        /// </summary>
        public static BetHistory ConvertPreBetToBetHistory(PreBet preBet, bool isWon)
        {
            return new BetHistory
            {
                IsWon = isWon,
                BetType = preBet.BetType,
                Probability = preBet.Probability,
                FixtureId = preBet.FixtureId,
                LeagueId = preBet.LeagueId,
                LeagueName = preBet.LeagueName,
                LeagueCountry = preBet.LeagueCountry,
                LeagueSeason = preBet.LeagueSeason,
                HomeTeamId = preBet.HomeTeamId,
                HomeTeamName = preBet.HomeTeamName,
                AwayTeamId = preBet.AwayTeamId,
                AwayTeamName = preBet.AwayTeamName
            };
        }
        /// <summary>
        /// Egy PreMatch objektumot PreBet objektumá konvertál
        /// </summary>
        public static PreBet ConvertPreMatchToPreBet(PreMatch preMatch, BetType betType, double probability)
        {
            return new PreBet
            {
                BetType = betType,
                Probability = probability,
                Date = preMatch.Date,
                FixtureId = preMatch.Id,
                LeagueId = preMatch.LeagueId,
                LeagueName = preMatch.LeagueName,
                LeagueCountry = preMatch.LeagueCountry,
                LeagueSeason = preMatch.LeagueSeason,
                HomeTeamId = preMatch.HomeTeamId,
                HomeTeamName = preMatch.HomeTeamName,
                AwayTeamId = preMatch.AwayTeamId,
                AwayTeamName = preMatch.AwayTeamName
            };
        }
        public static void CreateNotificationRequest(int id, string title, string description)
        {
            var request = new NotificationRequest
            {
                NotificationId = id,
                Title = title,
                Description = description,
            };

            LocalNotificationCenter.Current.Show(request);
        }
    }
}

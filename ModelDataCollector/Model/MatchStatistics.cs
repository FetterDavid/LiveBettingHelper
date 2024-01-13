using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataCollector.Model
{
    public class MatchStatistics
    {
        public int IsOver { get; set; }
        // Home Team Statistics
        public double HomePossession { get; set; }
        public double HomeAttacksNormal { get; set; }
        public double HomeAttacksDangerous { get; set; }
        public double HomeShootsTotal { get; set; }
        public double HomeShootsOnTarget { get; set; }
        public double HomeShootsOffTarget { get; set; }
        public double HomeGoalAssists { get; set; }
        public double HomePenalties { get; set; }
        public double HomeCorners { get; set; }
        public double HomeFoulsTotal { get; set; }
        public double HomeFoulsYellowCards { get; set; }
        public double HomeFoulsYellowToRedCards { get; set; }
        public double HomeFoulsRedCards { get; set; }
        public double HomeSubstitutions { get; set; }
        public double HomeOffSides { get; set; }
        public double HomeThrowIns { get; set; }
        public double HomeInjuries { get; set; }
        public double HomeDominanceIndex { get; set; }
        public double HomeDominanceAverageOver25 { get; set; }

        // Away Team Statistics
        public double AwayPossession { get; set; }
        public double AwayAttacksNormal { get; set; }
        public double AwayAttacksDangerous { get; set; }
        public double AwayShootsTotal { get; set; }
        public double AwayShootsOnTarget { get; set; }
        public double AwayShootsOffTarget { get; set; }
        public double AwayGoalAssists { get; set; }
        public double AwayPenalties { get; set; }
        public double AwayCorners { get; set; }
        public double AwayFoulsTotal { get; set; }
        public double AwayFoulsYellowCards { get; set; }
        public double AwayFoulsYellowToRedCards { get; set; }
        public double AwayFoulsRedCards { get; set; }
        public double AwaySubstitutions { get; set; }
        public double AwayOffSides { get; set; }
        public double AwayThrowIns { get; set; }
        public double AwayInjuries { get; set; }
        public double AwayDominanceIndex { get; set; }
        public double AwayDominanceAverageOver25 { get; set; }
    }
}

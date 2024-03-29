﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace LiveBettingHelper.Model
{
    public partial class Bet : BetBase
    {
        public double Odds { get; set; }
        public int BetMinute { get; set; }
        public double BetValue { get; set; }
        public bool Finished { get; set; }
        public bool Winned { get; set; }
        [ObservableProperty]
        private double _possibleWinning;
    }
}

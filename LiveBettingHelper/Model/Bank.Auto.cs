using CommunityToolkit.Mvvm.ComponentModel;
using LiveBettingHelper.Abstractions;

namespace LiveBettingHelper.Model
{
    public partial class Bank : LocalBaseModel
    {
        [ObservableProperty]
        private double _balance;
        public double DefaultBetStake { get; set; }
    }
}

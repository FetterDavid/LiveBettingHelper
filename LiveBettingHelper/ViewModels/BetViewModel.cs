using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveBettingHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.ViewModels
{
    public partial class BetViewModel : BaseViewModel
    {
        [ObservableProperty]
        private List<Bet> _bets;

        public BetViewModel() { ReloadBets(); }

        public void ReloadBets()
        {
            Bets = App.MM.BetRepo.GetItems();
        }
    }
}

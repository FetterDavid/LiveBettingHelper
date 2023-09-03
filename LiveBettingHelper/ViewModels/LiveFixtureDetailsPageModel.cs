using LiveBettingHelper.Model;
using LiveBettingHelper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.ViewModels
{
    public class LiveFixtureDetailsPageModel
    {
        private LiveMatch _match;
        private double _over05Odd;

        public LiveFixtureDetailsPageModel(LiveMatch liveMatch)
        {
            this._match = liveMatch;
            LoadOdd();
        }

        private async void LoadOdd()
        {
            //_over05Odd = await ApiManager.GetRankDifferenceAsync(_match);
            var str = await ApiManager.GetTodayMatchesAsync();
            Console.WriteLine("-----------");
        }
    }
}

using LiveBettingHelper.Model;

namespace LiveBettingHelper.ViewModels
{
    public class LiveFixtureDetailsViewModel
    {
        //private LiveMatch _match;
        //private double _over05Odd;

        public LiveFixtureDetailsViewModel(LiveMatch liveMatch)
        {
            Console.WriteLine(liveMatch.AwayTeamName);
            //this._match = liveMatch;
            //LoadOdd();
        }

        //private async void LoadOdd()
        //{
        //    //_over05Odd = await ApiManager.GetRankDifferenceAsync(_match);
        //    //var str = await ApiManager.GetTodayMatchesAsync();
        //    //Console.WriteLine("-----------");
        //}
    }
}

namespace LiveBettingHelper.Model.ApiSchemas
{

    public class LiveFixturesRootObject
    {
        public object[] errors { get; set; }
        public int results { get; set; }
        public LiveFixtureObject[] response { get; set; }
    }

}

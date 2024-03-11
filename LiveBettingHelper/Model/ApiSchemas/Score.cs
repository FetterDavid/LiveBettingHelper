namespace LiveBettingHelper.Model.ApiSchemas
{
    public class Score
    {
        public Halftime halftime { get; set; }
        public Fulltime fulltime { get; set; }
        public Extratime extratime { get; set; }
        public Penalty penalty { get; set; }
    }

}

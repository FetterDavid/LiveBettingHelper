namespace LiveBettingHelper.Model.ApiSchemas
{
    public class Event
    {
        public Time time { get; set; }
        public Team team { get; set; }
        public Player player { get; set; }
        public Assist assist { get; set; }
        public string type { get; set; }
        public string detail { get; set; }
        public string comments { get; set; }
    }

}

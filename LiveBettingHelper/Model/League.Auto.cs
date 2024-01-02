using LiveBettingHelper.Abstractions;

namespace LiveBettingHelper.Model
{
    public partial class League : LocalBaseModel
    {
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string Type { get; set; }
        public bool Selected { get; set; }
    }
}

using LiveBettingHelper.Abstractions;
using SQLite;

namespace LiveBettingHelper.Model
{
    public partial class Country : LocalBaseModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}

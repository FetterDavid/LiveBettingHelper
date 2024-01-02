using LiveBettingHelper.Abstractions;
using LiveBettingHelper.Utilities;

namespace LiveBettingHelper.Model
{
    public partial class LastCheck : LocalBaseModel
    {
        public CheckType CheckType { get; set; }
        public DateTime CheckDate { get; set; }
    }
}

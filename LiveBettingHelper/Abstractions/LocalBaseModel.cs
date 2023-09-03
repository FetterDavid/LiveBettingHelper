using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Abstractions
{
    public class LocalBaseModel : BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public new int Id { get; set; }
    }
}

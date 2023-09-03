using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Abstractions
{
    public class ApiBaseModel : BaseModel
    {
        [PrimaryKey]
        public new int Id { get; set; }
    }
}

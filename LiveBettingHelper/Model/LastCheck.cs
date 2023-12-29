﻿using LiveBettingHelper.Abstractions;
using LiveBettingHelper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Model
{
    public class LastCheck : LocalBaseModel
    {
        public CheckType CheckType { get; set; }
        public DateTime CheckDate { get; set; }
    }
}

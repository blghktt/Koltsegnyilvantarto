﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TartalomGyakorlas.Models
{
    public class Bevetel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int IncomeItemID { get; set; }
        public int Value { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
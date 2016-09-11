using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Koltseg.Models
{
    public class Income
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int IncomeItemID { get; set; }
        public int Value { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual User User { get; set; }
        public virtual IncomeItem IncomeItem { get; set; }
    }
}
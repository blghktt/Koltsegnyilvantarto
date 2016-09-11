using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Koltseg.Models
{
    public class Spending
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int SpendingItemID { get; set; }
        public int Value { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual SpendingItem SpendingItem { get; set; }
        public virtual User User { get; set; }
    }
}
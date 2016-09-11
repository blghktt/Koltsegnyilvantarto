using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Koltseg.Models
{
    public class SpendingItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public int LastValue { get; set; }

        public virtual ICollection<Spending> Spendings { get; set; }
        public virtual Category Category { get; set; }
    }
}
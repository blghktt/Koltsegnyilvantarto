using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Koltseg.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Display(Name="Név")]
        public string Name { get; set; }

        public virtual ICollection<IncomeItem> IncomeItems { get; set; }
        public virtual ICollection<SpendingItem> SpendingItems { get; set; }

    }
}
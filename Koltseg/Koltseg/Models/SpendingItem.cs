﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Koltseg.Models
{
    public class SpendingItem
    {
        public int ID { get; set; }

        [Display(Name="Név")]
        public string Name { get; set; }
        public int CategoryID { get; set; }

        [Display(Name = "Utolsó érték")]
        public int LastValue { get; set; }

        public virtual ICollection<Spending> Spendings { get; set; }
        public virtual Category Category { get; set; }
    }
}
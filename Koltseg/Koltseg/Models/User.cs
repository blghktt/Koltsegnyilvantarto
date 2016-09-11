using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Koltseg.Models
{
    public class User
    {
        public int ID { get; set; }

        [Display(Name="Felhasználónév")]
        public string UserName { get; set; }

        [Display(Name="Jelszó")]
        public string Password { get; set; }

        public virtual ICollection<Spending> Spendings { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }
    }
}
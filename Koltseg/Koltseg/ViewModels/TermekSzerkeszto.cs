using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Koltseg.ViewModels
{
    public class TermekSzerkeszto
    {
        public int ID { get; set; }

        [Display(Name = "Név")]
        public string Name { get; set; }
        public int CategoryID { get; set; }
        
        [Display(Name="Kategória")]
        public string CategoryName { get; set; }

        [Display(Name = "Utolsó érték")]
        public int LastValue { get; set; }

        public static List<string> CategoryNames = new List<string>();
    }
}
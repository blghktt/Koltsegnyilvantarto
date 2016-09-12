using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Koltseg.ViewModels
{
    public class Koltsegvetes
    {
        public int ID { get; set; }
        
        [Display(Name ="Típus")]
        public string Tipus { get; set; }

        [StringLength(3)]
        [Display(Name = "Tétel név")]
        public string TetelNev { get; set; }

        [StringLength(3)]
        [Display(Name = "Kategória név")]
        public string KategoriaNev { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Dátum")]
        public DateTime Datum { get; set; }

        [Range(1, Int32.MaxValue)]
        [Display(Name = "Érték")]
        public int Value { get; set; }
       
    }
}
using Koltseg.Misc;
using Koltseg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Koltseg.ViewModels
{
    public class Statisztika
    {
        public string Id { get; set; }
        
        //public EInterval Intervallum { get; set; }

        public DateTime Datum { get; set; }

        public int OsszBevetel { get; set; }
        public int OsszKiadas { get; set; }


        public List<Spending> koltsegek = new List<Spending>();

    }
}
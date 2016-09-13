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
        public string Idopont { get; set; }

        public int OsszBevetel { get; set; }
        public int OsszKiadas { get; set; }

        public Dictionary<string, int> koltsegek { get; set; }

    }
}
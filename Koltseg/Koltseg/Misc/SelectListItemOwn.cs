using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Koltseg.Misc
{
    public class SelectListItemOwn : SelectListItem
    {
        public override string ToString()
        {
            return Text;
        }
    }
}
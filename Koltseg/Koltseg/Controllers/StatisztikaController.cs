using Koltseg.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Koltseg.Controllers
{
    public class StatisztikaController : Controller
    {
        private KoltsegContext db = new KoltsegContext();
        // GET: Statisztika
        [HttpGet]
        public ActionResult Index()
        {
            var model = db.Categories.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int? id)
        {
            var model = db.Categories.ToList();
            return View(id==null? "0":id.ToString());
        }
    }
}
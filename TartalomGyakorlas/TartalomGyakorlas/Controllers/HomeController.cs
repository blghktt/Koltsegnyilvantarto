using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TartalomGyakorlas.Models;

namespace TartalomGyakorlas.Controllers
{
    public class HomeController : Controller
    {
        
        private static List<BevetelEsKiadas> tetelek = new List<BevetelEsKiadas>();
        bool isBevetel;
        static HomeController()
        {
            tetelek.Add(new BevetelEsKiadas() {ID = 1, CreatedTime = DateTime.Now, IncomeItemID =3, IsBevetel=true, Value = 2100, UserID= 2 });
            tetelek.Add(new BevetelEsKiadas() { ID = 2, CreatedTime = DateTime.Now, IncomeItemID = 30, IsBevetel = true, Value = 23000, UserID = 2 });
            tetelek.Add(new BevetelEsKiadas() { ID = 3, CreatedTime = DateTime.Now, IncomeItemID = 3, IsBevetel = false, Value = 200, UserID = 2 });
            tetelek.Add(new BevetelEsKiadas() { ID = 4, CreatedTime = DateTime.Now, IncomeItemID = 31, IsBevetel = true, Value = 500, UserID = 2 });
            tetelek.Add(new BevetelEsKiadas() { ID = 5, CreatedTime = DateTime.Now, IncomeItemID = 31, IsBevetel = false, Value = 600, UserID = 2 });
            tetelek.Add(new BevetelEsKiadas() { ID = 6, CreatedTime = DateTime.Now, IncomeItemID = 33, IsBevetel = true, Value = 700, UserID = 2 });
            tetelek.Add(new BevetelEsKiadas() { ID = 7, CreatedTime = DateTime.Now, IncomeItemID = 3, IsBevetel = true, Value = 500, UserID = 2 });
        }

        public ActionResult Index()
        {
            return View(tetelek);
        }

        [HttpGet]
        public ActionResult UjFelvitel(int id)
        {
            BevetelEsKiadas model = new BevetelEsKiadas() { IsBevetel = id==0?true:false};
            return View(model);
        }

        [HttpPost]
        public ActionResult UjFelvitel(BevetelEsKiadas model)
        {
            if (model.IsBevetel)
            {
                //bevetel
                tetelek.Add(new BevetelEsKiadas() { ID = 10, CreatedTime = DateTime.Now, IncomeItemID = model.IncomeItemID, IsBevetel = true, Value = model.Value, UserID = 2 });
            }
            else
            {
                //kiadas
                tetelek.Add(new BevetelEsKiadas() { ID = 11, CreatedTime = DateTime.Now, IncomeItemID = model.IncomeItemID, IsBevetel = false, Value = model.Value, UserID = 2 });
            }
            ModelState.Clear();
            BevetelEsKiadas uj = new BevetelEsKiadas() { IsBevetel = model.IsBevetel };
            return View(uj);
        }

        public ActionResult Statisztika(int? id)
        {
            return View();
        }
    }
}
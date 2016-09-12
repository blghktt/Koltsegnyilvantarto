using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Koltseg.Models;
using Koltseg.ViewModels;
using Koltseg.DAL;

namespace Koltseg.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private KoltsegContext db = new KoltsegContext();
       
        public ActionResult Index()
        {
            List<Koltsegvetes> tetelek = new List<Koltsegvetes>();
            var bevetelek = db.Incomes.ToList();
            var kiadasok = db.Spendings.ToList();

            string nev = System.Web.HttpContext.Current.User.Identity.Name;

            foreach (var item in bevetelek)
            {
                tetelek.Add(new Koltsegvetes()
                {
                    ID = item.ID,
                    TetelNev = item.IncomeItem.Name,
                    KategoriaNev = item.IncomeItem.Category.Name,
                    IsBevetel = true,
                    Value = item.Value,
                    Datum = item.CreatedTime,

                });
            }

            foreach (var item in kiadasok)
            {
                tetelek.Add(new Koltsegvetes()
                {
                    ID = item.ID,
                    TetelNev = item.SpendingItem.Name,
                   
                    KategoriaNev = item.SpendingItem.Category.Name,
                    IsBevetel = false,
                    Value = item.Value,
                    Datum = item.CreatedTime,

                });
            }

            tetelek.OrderBy(o => o.Datum).ToList();

            return View(tetelek);
        }

        [HttpGet]
        public ActionResult UjFelvitel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UjFelvitel(Koltsegvetes model)
        {
            
            if (Request.Form["select"] == "Bevétel")
            {
                db.Categories.Add(new Category() {Name = model.KategoriaNev });
                db.IncomeItems.Add(new IncomeItem() { Name = model.TetelNev, CategoryID = db.Categories.Single(p=>p.Name == model.KategoriaNev).ID  ,LastValue = 2010});
                //bevetel
                db.Incomes.Add(new Income() {CreatedTime = DateTime.Now, IncomeItemID = db.IncomeItems.Single(p => p.Name == model.TetelNev).ID, Value = model.Value, UserID = 2 });
            }
            else
            {
                //kiadas
               // tetelek.Add(new BevetelEsKiadas() { ID = 11, CreatedTime = DateTime.Now, ItemID = model.ItemID, IsBevetel = false, Value = model.Value, UserID = 2 });
            }
            db.SaveChanges();
            ModelState.Clear();
            Koltsegvetes uj = new Koltsegvetes() { IsBevetel = model.IsBevetel };
            return View(uj);
        }

        public ActionResult Statisztika(int? id)
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
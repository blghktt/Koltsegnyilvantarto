using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Koltseg.Models;
using Koltseg.ViewModels;
using Koltseg.DAL;
using System.Globalization;

namespace Koltseg.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private KoltsegContext db = new KoltsegContext();

        public ActionResult Index( int? ev, int?honap)
        {
            
            List<Koltsegvetes> tetelek = new List<Koltsegvetes>();
            List<Income> bevetelek = new List<Income>();
            List<Spending> kiadasok = new List<Spending>();
            
            if (ev == null)
            {
                bevetelek = db.Incomes.Where(m => m.CreatedTime.Year == DateTime.Now.Year).Where(m => m.CreatedTime.Month == DateTime.Now.Month).ToList();
                kiadasok = db.Spendings.Where(m => m.CreatedTime.Year == DateTime.Now.Year).Where(m => m.CreatedTime.Month == DateTime.Now.Month).ToList();

            }
            else if (honap == null)
            {
                bevetelek = db.Incomes.Where(m => m.CreatedTime.Year == ev).ToList();
                kiadasok = db.Spendings.Where(m => m.CreatedTime.Year == ev).ToList();

            }
            else
            {
                bevetelek = db.Incomes.Where(m => m.CreatedTime.Year == ev).Where(m => m.CreatedTime.Month == honap).ToList();
                kiadasok = db.Spendings.Where(m => m.CreatedTime.Year == ev).Where(m=>m.CreatedTime.Month == honap).ToList();

            }

            foreach (var item in bevetelek)
            {
                tetelek.Add(new Koltsegvetes()
                {
                    ID = item.ID,
                    TetelNev = item.IncomeItem.Name,
                    KategoriaNev = item.IncomeItem.Category.Name,
                    Tipus = "Bevétel",
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
                    Tipus = "Kiadás",
                    Value = item.Value,
                    Datum = item.CreatedTime,

                });
            }

            //tetelek.OrderBy(o => o.Datum).ToList();

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
            var currentUser = System.Web.HttpContext.Current.User.Identity.Name;

            if (model.Tipus == "Bevétel")
            {
                if (!db.IncomeItems.Any(m => m.Name == model.TetelNev))
                {
                    db.IncomeItems.Add(new IncomeItem() { Name = model.TetelNev, LastValue = model.Value, CategoryID = 1 });
                    db.SaveChanges();
                }
                else
                {
                    var tetel = db.IncomeItems.Single(m => m.Name == model.TetelNev);
                    tetel.LastValue = model.Value;
                    db.SaveChanges();
                }

                db.Incomes.Add(new Income() { CreatedTime = model.Datum, IncomeItemID = db.IncomeItems.Single(p => p.Name == model.TetelNev).ID, Value = model.Value, UserID = db.Users.Single(m => m.UserName == currentUser).ID });
                db.SaveChanges();
            }
            else
            {
                if (!db.SpendingItems.Any(m => m.Name == model.TetelNev))
                {
                    db.SpendingItems.Add(new SpendingItem() { Name = model.TetelNev, LastValue = model.Value, CategoryID = 1 });
                    db.SaveChanges();
                }
                else
                {
                    var tetel = db.SpendingItems.Single(m => m.Name == model.TetelNev);
                    tetel.LastValue = model.Value;
                    db.SaveChanges();
                }
                db.Spendings.Add(new Spending() { CreatedTime = model.Datum, SpendingItemID = db.SpendingItems.Single(p => p.Name == model.TetelNev).ID, Value = model.Value, UserID = db.Users.Single(m => m.UserName == currentUser).ID });
                db.SaveChanges();
            }

            ModelState.Clear();

            return View();
        }

        public ActionResult Statisztika()
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
using Koltseg.DAL;
using Koltseg.Misc;
using Koltseg.Models;
using Koltseg.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Koltseg.Controllers
{
    public class StatisztikaController : Controller
    {
        private KoltsegContext db = new KoltsegContext();
        private EInterval currentInterval = EInterval.Month;
        
                            //bevétel, kiadás                                                 
        Dictionary<string, Tuple<int, int>> model = new Dictionary<string, Tuple<int, int>>();

        // GET: Statisztika
        [HttpGet]
        public ActionResult Index()
        {
            GetModel();

            return View(model);
        }

        private void GetModel()
        {
            foreach (var item in db.Incomes)
            {
                var key = item.CreatedTime.Year.ToString() + "-" + item.CreatedTime.Month.ToString();
                if (!model.ContainsKey(key))
                {
                    model.Add(key, new Tuple<int, int>(0, 0));
                }

                model[key] = new Tuple<int, int>(model[key].Item1 + item.Value, model[key].Item2);

            }

            foreach (var item in db.Spendings)
            {
                var key = item.CreatedTime.Year.ToString() + "-" + item.CreatedTime.Month.ToString();
                if (!model.ContainsKey(key))
                {
                    model.Add(key, new Tuple<int, int>(0, 0));
                }

                model[key] = new Tuple<int, int>(model[key].Item1, model[key].Item2 + item.Value);

            }
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
/*

    @{
    Dictionary<Tuple<int, int>, Tuple<int, int>> statisztika = new Dictionary<Tuple<int, int>, Tuple<int, int>>();

    //bevétel
    foreach (var category in Model)
    {
        foreach (var incomeItem in category.IncomeItems)
        {
            foreach (var income in incomeItem.Incomes)
            {
                var year = income.CreatedTime.Year;
    var month = income.CreatedTime.Month;

    var key = new Tuple<int, int>(year, month);

                if (!statisztika.ContainsKey(key))
                {
                    statisztika.Add(key, new Tuple<int, int>(0, 0));
                }
statisztika[key] = new Tuple<int, int>(statisztika[key].Item1 + income.Value, statisztika[key].Item2);
            }

        }

        //kiadás
        foreach (var spendingItem in category.SpendingItems)
        {
            foreach (var spending in spendingItem.Spendings)
            {
                var year = spending.CreatedTime.Year;
var month = spending.CreatedTime.Month;

var key = new Tuple<int, int>(year, month);

                if (!statisztika.ContainsKey(key))
                {
                    statisztika.Add(key, new Tuple<int, int>(0, 0));
                }
                statisztika[key] = new Tuple<int, int>(statisztika[key].Item1, statisztika[key].Item2 + spending.Value);
            }

        }
    }

}*/
/*
          List<Statisztika> model = new List<Statisztika>();

          if (interval == "Year")
              currentInterval = EInterval.Year;

          foreach (var item in db.Incomes)
          {
              Statisztika currentStatisztika;

              try
              {
                  currentStatisztika = model.Single(m => (m.Datum.Year == item.CreatedTime.Year && (currentInterval == EInterval.Year || m.Datum.Month == item.CreatedTime.Month)));
              }
              catch
              {
                  currentStatisztika = new Statisztika()
                  {
                      Datum = new DateTime(item.CreatedTime.Year, currentInterval == EInterval.Month ? item.CreatedTime.Month : 1, 1),
                      Intervallum = currentInterval,
                      OsszBevetel = 0,
                      OsszKiadas = 0,
                      Id = currentInterval == EInterval.Month ? item.CreatedTime.ToString("yyyy-MM") : item.CreatedTime.ToString("yyyy")
                  };
                  model.Add(currentStatisztika);
              }

              currentStatisztika.OsszBevetel += item.Value;
          }

          foreach (var item in db.Spendings)
          {
              Statisztika currentStatisztika;

              try
              {
                  currentStatisztika = model.Single(m => (m.Datum.Year == item.CreatedTime.Year && (currentInterval == EInterval.Year || m.Datum.Month == item.CreatedTime.Month)));
              }
              catch
              {
                  currentStatisztika = new Statisztika()
                  {
                      Datum = new DateTime(item.CreatedTime.Year, currentInterval == EInterval.Month ? item.CreatedTime.Month : 1, 1),
                      Intervallum = currentInterval,
                      OsszBevetel = 0,
                      OsszKiadas = 0,
                      Id = currentInterval == EInterval.Month ? item.CreatedTime.ToString("yyyy-MM") : item.CreatedTime.ToString("yyyy")
                  };
                  model.Add(currentStatisztika);
              }

              currentStatisztika.OsszKiadas += item.Value;
              currentStatisztika.koltsegek.Add(item);
          }
           */



using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Koltseg.Models;
using Koltseg.ViewModels;
using Koltseg.DAL;

namespace TartalomGyakorlas.Controllers
{
    [Authorize]
    public class SpendingItemController : Controller
    {
        private KoltsegContext db = new KoltsegContext();

        // GET: IncomeItems
        public ActionResult Index()
        {
            List<TermekSzerkeszto> tetelek = new List<TermekSzerkeszto>();
            foreach (var item in db.SpendingItems)
            {
                tetelek.Add(new TermekSzerkeszto()
                {
                    ID= item.ID,
                    Name = item.Name,
                    CategoryID = item.CategoryID,
                    CategoryName = item.Category.Name,
                    LastValue = item.LastValue

                });
            }
            
            return View(tetelek);
        }

        [HttpGet]
        public ActionResult UjElem()
        {
            TermekSzerkeszto.CategoryNames.Clear();
            foreach (var item in db.Categories)
            {
                TermekSzerkeszto.CategoryNames.Add(item.Name);
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult UjElem(TermekSzerkeszto model)
        {
            if (!db.Categories.Any(m => m.Name == model.CategoryName))
                db.Categories.Add(new Category()
                {
                    Name = model.CategoryName
                });
            db.SaveChanges();
            db.SpendingItems.Add(new SpendingItem()
            {
                Name = model.Name,
                CategoryID = db.Categories.Single(m=>m.Name == model.CategoryName).ID,
                LastValue = model.LastValue
            });
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Szerkesztes(int id)
        {
            var act = db.SpendingItems.Single(p => p.ID == id);
            return View("UjElem", new TermekSzerkeszto() { ID = act.ID, CategoryID = act.CategoryID, Name = act.Name, LastValue = act.LastValue, CategoryName = act.Category.Name});
        }

        [HttpPost]
        public ActionResult Szerkesztes(TermekSzerkeszto model)
        {
            if (!db.Categories.Any(m => m.Name == model.CategoryName))
            {
                db.Categories.Add(new Category()
                {
                    Name = model.CategoryName
                });
                db.SaveChanges();
            }
            
            var act = db.SpendingItems.Single(p => p.ID == model.ID);
            act.Name = model.Name;
            act.CategoryID = db.Categories.Single(m=>m.Name == model.CategoryName).ID;
            act.LastValue = model.LastValue;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult UploadFile()
        {
            var httpPostedFileBase = Request.Files["FileName"];
            if (httpPostedFileBase != null && httpPostedFileBase.ContentLength > 0)
            {
                using (StreamReader reader = new StreamReader(httpPostedFileBase.InputStream))
                {
                    while (!reader.EndOfStream)
                    {
                        try
                        {
                            string[] elements = reader.ReadLine().Split(';');

                            int id = Convert.ToInt32(elements[0].Trim('"'));
                            string name = elements[1].Trim('"');
                            string categoryName = elements[2].Trim('"');
                            int value = Convert.ToInt32(elements[3].Trim('"'));

                            if (!db.Categories.Any(m => m.Name == categoryName))
                            {
                                db.Categories.Add(new Category() { Name = categoryName});
                                db.SaveChanges();
                            }

                            if (String.IsNullOrEmpty(elements[0].Trim('"')) || !db.SpendingItems.Any(m => m.ID == id))
                            {
                                
                                db.SpendingItems.Add(new SpendingItem() {Name = name, CategoryID = db.Categories.Single(m=>m.Name == categoryName).ID , LastValue = value });
                                db.SaveChanges();
                            }
                            else
                            {
                                var act = db.SpendingItems.Single(p => p.ID == id);
                                act.Name = name;
                                act.CategoryID = db.Categories.Single(m => m.Name == categoryName).ID;
                                act.LastValue =value;
                                db.SaveChanges();

                            }
                        }
                        catch (Exception e)
                        {
                            var s = e.Message;
                            continue;
                        }
                    }
                }

            }

            return RedirectToAction("Index");
        }

        public ActionResult Letoltes()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\"Id\";\"Name\";\"CategoryName\";\"Value\"");
            foreach (var t in db.SpendingItems)
            {
                sb.AppendFormat("{0};\"{1}\";\"{2}\";{3}", t.ID, t.Name, t.Category.Name, t.LastValue);
                sb.AppendLine();
            }
            string cvsFileContent = sb.ToString();
            byte[] content = Encoding.UTF8.GetBytes(cvsFileContent);
            return new FileContentResult(content, "text/csv") { FileDownloadName = "termekek.csv" };
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
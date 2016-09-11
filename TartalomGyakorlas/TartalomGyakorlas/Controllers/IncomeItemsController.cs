using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TartalomGyakorlas.Models;

namespace TartalomGyakorlas.Controllers
{
    public class IncomeItemsController : Controller
    {
        private static List<IncomeItems> tetelek = new List<IncomeItems>();

        static IncomeItemsController()
        {
            tetelek.Add(new IncomeItems() { ID = 2, Name = "Fizetés", CategoryID = 3, LastValue = 2000 });
            tetelek.Add(new IncomeItems() { ID = 4, Name = "Könyvutalvány", CategoryID = 2, LastValue = 2000 });

        }
        // GET: IncomeItems
        public ActionResult Index()
        {
            return View(tetelek);
        }

        [HttpGet]
        public ActionResult UjElem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UjElem(IncomeItems model)
        {
            tetelek.Add(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Szerkesztes(int id)
        {
            return View("UjElem", tetelek.Single(p=>p.ID == id));
        }

        [HttpPost]
        public ActionResult Szerkesztes(IncomeItems model)
        {
            var act = tetelek.Single(p => p.ID == model.ID);
            act.Name = model.Name;
            act.CategoryID = model.CategoryID;
            act.LastValue = model.LastValue;

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
                        try {
                            string[] elements = reader.ReadLine().Split(';');
                            
                            tetelek.Add(new IncomeItems()
                            {
                                ID = Convert.ToInt32(elements[0].Trim('"')),
                                Name = elements[1].Trim('"'),
                                CategoryID =3,
                                LastValue = Convert.ToInt32(elements[3].Trim('"'))

                            });
                        }
                        catch
                        {
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
            foreach (var t in tetelek)
            {
                sb.AppendFormat("{0};\"{1}\";\"{2}\";{3}", t.ID, t.Name, t.CategoryID, t.LastValue);
                sb.AppendLine();
            }
            string cvsFileContent = sb.ToString();
            byte[] content = Encoding.UTF8.GetBytes(cvsFileContent);
            return new FileContentResult(content, "text/csv") { FileDownloadName = "termekek.csv" };
        }
    }
}
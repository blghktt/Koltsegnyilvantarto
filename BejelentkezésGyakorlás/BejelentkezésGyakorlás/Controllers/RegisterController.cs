using BejelentkezésGyakorlás.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BejelentkezésGyakorlás.Controllers
{
    public class RegisterController : Controller
    {
        public static List<User> felhasznalok = new List<User>();

        static RegisterController()
        {
            for (int i = 0; i < 3; i++)
                felhasznalok.Add(new Models.User());

            felhasznalok[0].UserName = "elso";
            felhasznalok[0].Password = "elso";

            felhasznalok[1].UserName = "masodik";
            felhasznalok[1].Password = "masodik";

            felhasznalok[2].UserName = "harmadik";
            felhasznalok[2].Password = "harmadik";

        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User model)
        {
            int act = -1;
            for (int i = 0; i < felhasznalok.Count; i++)
            {
                if (felhasznalok[i].UserName == model.UserName)
                {
                    act = i;
                    ModelState.AddModelError("","A megadott felhasználónév már foglalt");
                    break;
                }
            }

            if (act != -1)
            {
                return View(model);
            }

            felhasznalok.Add(new User());
            felhasznalok[felhasznalok.Count-1].UserName = model.UserName;
            felhasznalok[felhasznalok.Count - 1].Password = model.Password;

           
            return RedirectToAction("Index", "Home");
           
        }
    }
}
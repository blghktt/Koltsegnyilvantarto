using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Koltseg.DAL;
using Koltseg.Models;

namespace Koltseg.Controllers
{
    public class RegisterController : Controller
    {
        private KoltsegContext db = new KoltsegContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User model)
        {
            if (db.Users.Any(u => u.UserName == model.UserName))
            {
                ModelState.AddModelError("", "A megadott felhasználónév már foglalt");
                return View(model);
            }
            else
            {
                db.Users.Add(new User() { UserName = model.UserName, Password = PasswordHelper.PasswordHelper.EncryptPassword(model.Password) });
                db.SaveChanges();

                return RedirectToAction("Index", "Login");
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
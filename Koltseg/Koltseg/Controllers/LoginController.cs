using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Koltseg.DAL;
using Koltseg.Models;

namespace Koltseg.Controllers
{
    public class LoginController : Controller
    {
        private KoltsegContext db = new KoltsegContext();

        [HttpGet]
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Index(User model, string returnUrl)
        {
            try
            {
                User act = db.Users.Single(u => (u.UserName == model.UserName));
                var pwd = act.Password;
                if (!PasswordHelper.PasswordHelper.CheckPassword(model.Password, pwd))
                {
                    throw new Exception();
                }
                
            }
            catch
            {
                ModelState.AddModelError("", "Hibás felhasználónév vagy jelszó");
                return View(model);
            }
            
            FormsAuthentication.SetAuthCookie(model.UserName, false);
            return Redirect(returnUrl ?? "/");
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
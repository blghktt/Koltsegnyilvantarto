using BejelentkezésGyakorlás.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BejelentkezésGyakorlás.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult Index(User model, string returnUrl)
        { 
            for (int i = 0; i < RegisterController.felhasznalok.Count; i++)
            {
                if (RegisterController.felhasznalok[i].UserName == model.UserName && RegisterController.felhasznalok[i].Password == model.Password)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return Redirect(returnUrl ?? "/");
                }
            }
           
            ModelState.AddModelError("", "Hibás felhasználónév vagy jelszó");
            return View(model);
          
        }
    }
}
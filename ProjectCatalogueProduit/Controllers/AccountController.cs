using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProjectCatalogueProduit.Models;

namespace ProjectCatalogueProduit.Controllers
{
    public class AccountController : Controller
    {
        CATALOGUE_Entities db = new CATALOGUE_Entities();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
       public ActionResult Login(LoginModel loginModel)
        {
            bool exist = db.user.Any(x => x.name == loginModel.name && x.password == loginModel.password);
            user u = db.user.FirstOrDefault(x => x.name == loginModel.name && x.password == loginModel.password);
            if (exist)
            {
                FormsAuthentication.SetAuthCookie(u.name, false);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "verifier le nom ou le mot de passe");
            return View();
        }

        public ActionResult signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult signup(user USER)
        {
            db.user.Add(USER);
            db.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        
    }
}
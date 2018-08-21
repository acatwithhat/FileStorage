using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NHibernate.Linq;
using NHibernate;
using MvcApplication1.Models;
using WebMatrix.WebData;

namespace MvcApplication1.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Auth/

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Author model, string returnUrl)
        {
            using (ISession session = NHibertnateSession.OpenSession())
            {
                var authors = session.Query<Author>().ToList();
                foreach (var item in authors)
                {
                    if (item.Login == model.Login & item.Password == model.Password)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        return Redirect("/documents");
                    }
                }
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }

    }
}

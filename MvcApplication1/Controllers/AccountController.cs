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
            /*if (ModelState.IsValid && WebSecurity.Login(model.Login, model.Password,
                                        persistCookie: model.Remember))
            {
                return RedirectToAction("Index", "Home");
            }*/
            using (ISession session = NHibertnateSession.OpenSession())
            {
                var authors = session.Query<Author>().ToList();
                foreach (var item in authors)
                {
                    if (item.Login == model.Login & item.Password == model.Password)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        /*
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,                                     // ticket version
                        model.Login,                              // authenticated username
                        DateTime.Now,                          // issueDate
                        DateTime.Now.AddMinutes(30),           // expiryDate
                        false,                          // true to persist across browser sessions
                        "",                              // can be used to store additional user data
                        FormsAuthentication.FormsCookiePath); 

                     
                        string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                        
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);*/

                        // Your redirect logic
                        return Redirect("/documents");
                    }
                }
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

    }
}

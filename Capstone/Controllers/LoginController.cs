using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Capstone.Controllers
{
    public class LoginController : Controller
    {
        private Model1 Db = new Model1();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Accesso utente)
        {
            if (ModelState.IsValid)
            {
                Utenti user = Db.Utenti.Where(u => u.Username == utente.Username && u.Password == utente.Password).FirstOrDefault();

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    Session["UtenteId"] = user.IdUtente;

                    if (user.Ruolo == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Gioco", "Giochi");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nome utente o password non validi");
                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
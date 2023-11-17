using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Controllers
{
    public class HomeController : Controller
    {
        private Model1 Db = new Model1();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpGet]
        public ActionResult Registrazione()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrazione(Utenti utenti)
        {
            utenti.Ruolo = "User";
            if (ModelState.IsValid)
            {
                Db.Utenti.Add(utenti);
                Db.SaveChanges();
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
    }
}

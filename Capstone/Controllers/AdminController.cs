using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Capstone.Models;

namespace Capstone.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private Model1 Db = new Model1();

        //Lista utenti
        public ActionResult Index()
        {
            return View(Db.Utenti.ToList());
        }

        // GET Elimina utente
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utenti utenti = Db.Utenti.Find(id);
            if (utenti == null)
            {
                return HttpNotFound();
            }
            return View(utenti);
        }

        // POST Elimina utente
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utenti utenti = Db.Utenti.Find(id);
            Db.Utenti.Remove(utenti);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Elenco giochi
        public ActionResult ElencoGiochi()
        {
            var giochi = Db.Giochi.ToList();
            return View(giochi);
        }

        //Dettagli giochi
        public ActionResult DettagliGiochi(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giochi gioco = Db.Giochi.Find(id);
            if (gioco == null)
            {
                return HttpNotFound();
            }
            return View(gioco);
        }

        // GET Elimina giochi
        public ActionResult EliminaGiochi(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giochi gioco = Db.Giochi.Find(id);
            if (gioco == null)
            {
                return HttpNotFound();
            }
            return View(gioco);
        }

        // POST Elimina utente
        [HttpPost, ActionName("EliminaGiochi")]
        [ValidateAntiForgeryToken]
        public ActionResult CancellazioneConfermata(int id)
        {
            Giochi giochi = Db.Giochi.Find(id);
            Db.Giochi.Remove(giochi);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Totale utenti
        [HttpGet]
        public JsonResult GetTotalUtenti()
        {
            int totalUtenti = Db.Utenti.Count();

            return Json(new { TotalUtenti = totalUtenti }, JsonRequestBehavior.AllowGet);
        }

        //Totale giochi
        [HttpGet]
        public JsonResult GetTotalGiochi()
        {
            int totalGiochi = Db.Giochi.Count();

            return Json(new { TotalGiochi = totalGiochi }, JsonRequestBehavior.AllowGet);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Capstone.Models;

namespace Capstone.Controllers
{
    [Authorize(Roles = "User")]
    public class GiochiController : Controller
    {
        private Model1 Db = new Model1();

        // GET: Giochi
        [Authorize]
        public ActionResult Index()
        {
            var currentUserName = User.Identity.Name;
            var userAds = Db.Giochi.Where(g => g.Utenti.Username == currentUserName).ToList();
            return View(userAds);
        }

        public ActionResult Gioco()
        {
            var giochi = Db.Giochi.Include(g => g.Utenti).ToList();
            return View(giochi);
        }

        // GET Dettaglio giochi 
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giochi giochi = Db.Giochi.Find(id);
            if (giochi == null)
            {
                return HttpNotFound();
            }
            return View(giochi);
        }

        // GET Crea giochi
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(Db.CategoriaGiochi, "IdCategoria", "NomeCategoria");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "IdGioco,TitoloDelGioco,Descrizione,Prezzo,StatoDelGioco,DataDiInserimento,FotoGioco,CategoriaId")] Giochi giochi, HttpPostedFileBase Immagine)
        {
            if (ModelState.IsValid)
            {
                var currentUserName = User.Identity.Name;
                var user = Db.Utenti.FirstOrDefault(u => u.Username == currentUserName);

                if (user == null)
                {
                    return RedirectToAction("Index", "Giochi");
                }

                giochi.UtenteId = user.IdUtente;

                if (ViewBag.CategoriaId == null)
                {
                    ModelState.AddModelError("CategoriaId", "Seleziona una categoria");
                }

                if (giochi.FotoGioco != null && giochi.FotoGioco.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(giochi.FotoGioco.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/img/"), fileName);
                    giochi.FotoGioco.SaveAs(path);

                    giochi.Immagine = fileName;
                }

                Db.Giochi.Add(giochi);
                Db.SaveChanges();
                return RedirectToAction("Index", "Giochi");
            }

            ViewBag.CategoriaId = new SelectList(Db.CategoriaGiochi, "IdCategoria", "NomeCategoria", giochi.CategoriaId);
            return View(giochi);
        }

        // GET Modifica giochi
        [HttpGet]
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giochi giochi = Db.Giochi.Find(id);
            if (giochi == null)
            {
                return HttpNotFound();
            }

            var currentUserName = User.Identity.Name;

            if (giochi.Utenti.Username != currentUserName)
            {
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(Db.CategoriaGiochi, "IdCategoria", "NomeCategoria", giochi.CategoriaId);
            return View(giochi);
        }

        // POST Modifica giochi
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(Giochi giochi, HttpPostedFileBase Immagine)
        {
            if (ModelState.IsValid)
            {
                var currentUserName = User.Identity.Name;
                var existingGame = Db.Giochi.Find(giochi.IdGioco);

                if (existingGame == null || existingGame.Utenti.Username != currentUserName)
                {
                    return RedirectToAction("Index");
                }

                // Handle image upload
                if (Immagine != null && Immagine.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(Immagine.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/img/"), fileName);
                    Immagine.SaveAs(path);

                    existingGame.Immagine = fileName;
                }

                existingGame.TitoloDelGioco = giochi.TitoloDelGioco;
                existingGame.Descrizione = giochi.Descrizione;
                existingGame.Prezzo = giochi.Prezzo;
                existingGame.StatoDelGioco = giochi.StatoDelGioco;
                existingGame.DataDiInserimento = giochi.DataDiInserimento;
                existingGame.CategoriaId = giochi.CategoriaId;

                Db.Entry(existingGame).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(Db.CategoriaGiochi, "IdCategoria", "NomeCategoria", giochi.CategoriaId);
            return View(giochi);
        }

        // GET Elimina giochi
        [Authorize] 
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giochi giochi = Db.Giochi.Find(id);
            if (giochi == null)
            {
                return HttpNotFound();
            }

            var currentUserName = User.Identity.Name;

            if (giochi.Utenti.Username != currentUserName)
            {
                return RedirectToAction("Index");
            }

            return View(giochi);
        }

        // POST Elimina giochi
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize] 
        public ActionResult DeleteConfirmed(int id)
        {
            var currentUserName = User.Identity.Name;
            Giochi giochi = Db.Giochi.Find(id);

            if (giochi == null || giochi.Utenti.Username != currentUserName)
            {
                return RedirectToAction("Index");
            }

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
    }
}
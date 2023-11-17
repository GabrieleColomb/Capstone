using System;
using System.Linq;
using System.Web.Mvc;
using Capstone.Models;
using System.Data.Entity;
using System.Diagnostics;

namespace Capstone.Controllers
{
    [Authorize(Roles = "User")]
    public class CarrelloController : Controller
    {
        private Model1 Db = new Model1();

        // Visualizza il carrello dell'utente
        public ActionResult VisualizzaCarrello()
        {
            try
            {
                if (Session["UtenteId"] != null)
                {
                    int utenteId = (int)Session["UtenteId"];

                    var dettagliOrdine = Db.DettaglioOrdine
                        .Include(d => d.Giochi)
                        .Where(d => d.Ordine.UtenteId == utenteId)
                        .ToList();

                    decimal totale = dettagliOrdine.Sum(d => d.Quantita * d.Giochi.Prezzo);

                    ViewBag.Totale = totale;

                    if (dettagliOrdine.Count == 0)
                    {
                        ViewBag.MessaggioCarrelloVuoto = "Il tuo carrello è vuoto.";
                    }

                    return View(dettagliOrdine);
                }

                return RedirectToAction("Index", "Giochi");
            }
            catch (Exception)
            {
                // Gestisci l'eccezione come preferisci, ad esempio, reindirizzando a una pagina di errore
                return RedirectToAction("Index", "Giochi");
            }
        }

        [HttpPost]
        public ActionResult AggiungiAlCarrello(int id)
        {
            try
            {
                if (Session["UtenteId"] == null)
                {
                    return RedirectToAction("Index", "Giochi");
                }

                int utenteId = Convert.ToInt32(Session["UtenteId"]);

                Ordine ordine = new Ordine
                {
                    DataOrdine = DateTime.Today,
                    TotaleOrdine = 0,
                    UtenteId = utenteId,
                    StatoOrdine = false,
                };

                Db.Ordine.Add(ordine);
                Db.SaveChanges();

                Session["IdOrdine"] = ordine.IdOrdine;

                var existingOrder = Db.Ordine.Find(Convert.ToInt32(Session["IdOrdine"]));

                if (existingOrder != null)
                {
                    var gioco = Db.Giochi.Find(id);

                    if (gioco != null)
                    {
                        var dettaglioOrdine = new DettaglioOrdine
                        {
                            Quantita = 1,
                            GiocoId = id,
                            OrdineId = existingOrder.IdOrdine,
                        };

                        Db.DettaglioOrdine.Add(dettaglioOrdine);
                        Db.SaveChanges();
                    }
                }

                return RedirectToAction("VisualizzaCarrello");
            }
            catch (Exception)
            {
                // Gestisci l'eccezione come preferisci, ad esempio, reindirizzando a una pagina di errore
                return RedirectToAction("Index", "Giochi");
            }
        }

        // Rimuovi un gioco dal carrello dell'utente
        [HttpPost]
        public ActionResult RimuoviDalCarrello(int id)
        {
            try
            {
                Debug.WriteLine($"RimuoviDalCarrello called with id: {id}");

                var dettaglioOrdine = Db.DettaglioOrdine.Find(id);

                if (dettaglioOrdine != null)
                {
                    Db.DettaglioOrdine.Remove(dettaglioOrdine);
                    Db.SaveChanges();
                    Debug.WriteLine($"DettaglioOrdine with id {id} removed successfully.");

                    // Calcola il nuovo totale e restituiscilo come parte della risposta JSON
                    decimal nuovoTotale = CalcolaTotaleCarrello();
                    Session["TotaleCarrello"] = nuovoTotale;

                    // Aggiorna la sessione dopo la rimozione
                    var updatedOrdine = Db.DettaglioOrdine.ToList();
                    Session["Carrello"] = updatedOrdine;

                    return Json(new { success = true, nuovoTotale = nuovoTotale });
                }

                return Json(new { success = false, error = "DettaglioOrdine non trovato" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return Json(new { success = false, error = "Si è verificato un errore durante la rimozione dal carrello." });
            }
        }

        private decimal CalcolaTotaleCarrello()
        {
            // Calcola il totale del carrello
            int utenteId = Convert.ToInt32(Session["UtenteId"]);
            var dettagliOrdine = Db.DettaglioOrdine
                .Where(d => d.Ordine.UtenteId == utenteId)
                .ToList();

            return dettagliOrdine.Sum(d => d.Quantita * d.Giochi.Prezzo);
        }

        [HttpPost]
        public ActionResult SvuotaCarrello(int ordineId)
        {
            try
            {
                if (Session["UtenteId"] == null)
                {
                    return Json(new { success = false, error = "Utente non autenticato" });
                }

                int utenteId = Convert.ToInt32(Session["UtenteId"]);

                var ordine = Db.Ordine.SingleOrDefault(o => o.IdOrdine == ordineId && o.UtenteId == utenteId);

                if (ordine != null)
                {
                    // Assuming you have a navigation property DettagliOrdine in the Ordine entity
                    ordine.DettaglioOrdine.Clear();
                    Db.SaveChanges();

                    return Json(new { success = true });
                }

                return Json(new { success = false, error = "Nessun ordine da svuotare" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Json(new { success = false, error = "Si è verificato un errore durante lo svuotamento del carrello." });
            }
        }
    }
}

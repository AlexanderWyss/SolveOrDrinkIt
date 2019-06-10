using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolveOrDrinkIt.Models;

namespace SolveOrDrinkIt.Controllers
{
    public class GamesController : Controller
    {
        private SolveOrDrinkItEntities db = new SolveOrDrinkItEntities();

    

       
        // GET: Games/Create
        public ActionResult Index()
        {
            ViewBag.deckId = new SelectList(db.Decks, "id", "name");
            return View();
        }
        // GET: Games/JoinGame
        public ActionResult JoinGame()
        {
            return View();
        }

        // POST: Games/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "id,deckId,playDatetime")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.deckId = new SelectList(db.Decks, "id", "name", game.deckId);
            return View(game);
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

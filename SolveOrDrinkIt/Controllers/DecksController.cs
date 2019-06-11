using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolveOrDrinkIt.Models;
using SolveOrDrinkIt.Repositories;
using Task = System.Threading.Tasks.Task;

namespace SolveOrDrinkIt.Controllers
{
    [Authorize]
    public class DecksController : Controller
    {
        private SolveOrDrinkItEntities db = new SolveOrDrinkItEntities();
        private TaskRepository taskRepo;

        public DecksController()
        {
            taskRepo = new TaskRepository(db);
        }

        // GET: Decks
        public ActionResult Index()
        {
            return View(db.Decks.ToList());
        }

        // GET: Decks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = db.Decks.Find(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        // GET: Decks/Create
        public ActionResult Create()
        {
            return View(new DeckViewModel(db.Tasks.ToList()));
        }

        // POST: Decks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DeckViewModel deck)
        {
            if (ModelState.IsValid)
            {
                db.Decks.Add(toModel(deck));
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deck);
        }

        private Deck toModel(DeckViewModel deckVM)
        {
            Deck deck = new Deck()
            {
                id = deckVM.id,
                name = deckVM.name
            };
            foreach (int selectedTaskId in deckVM.selectedIds)
            {
                deck.Tasks.Add(taskRepo.Get(selectedTaskId));
            }
            return deck;
        }

        // GET: Decks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = db.Decks.Find(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        // POST: Decks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] Deck deck)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deck).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deck);
        }

        // GET: Decks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = db.Decks.Find(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        // POST: Decks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deck deck = db.Decks.Find(id);
            db.Decks.Remove(deck);
            db.SaveChanges();
            return RedirectToAction("Index");
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

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
        private DeckRepository repo; 
        private TaskRepository taskRepo;

        public DecksController()
        {
            SolveOrDrinkItEntities db = new SolveOrDrinkItEntities();
            repo = new DeckRepository(db);
            taskRepo = new TaskRepository(db);
        }

        // GET: Decks
        public ActionResult Index()
        {
            return View(repo.GetAll());
        }

        // GET: Decks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = repo.Get(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        // GET: Decks/Create
        public ActionResult Create()
        {
            return View(new DeckViewModel(taskRepo.GetAll()));
        }

        // POST: Decks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DeckViewModel deckVM)
        {
            if (ModelState.IsValid)
            {
                repo.Add(ToModel(deckVM));
                repo.Save();
                return RedirectToAction("Index");
            }

            return View(deckVM);
        }

        // GET: Decks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = repo.Get(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(new DeckViewModel(deck, taskRepo.GetAll()));
        }

        // POST: Decks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DeckViewModel deckVM)
        {
            if (ModelState.IsValid)
            {
                repo.Update(ToModel(deckVM));
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(deckVM);
        }

        // GET: Decks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = repo.Get(id);
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
            repo.Remove(id);
            repo.Save();
            return RedirectToAction("Index");
        }

        private Deck ToModel(DeckViewModel deckVM)
        {
            Deck deck;
            if (deckVM.id == null)
            {
                 deck = new Deck()
                {
                    name = deckVM.name
                };
            }
            else
            {
                deck = repo.Get(deckVM.id);
                deck.name = deckVM.name;
            }
            ReplaceTasks(deck, deckVM.selectedIds);
            return deck;
        }

        private void ReplaceTasks(Deck deck, int[] selectedIds)
        {
            deck.Tasks.Clear();
            foreach (int selectedTaskId in selectedIds)
            {
                deck.Tasks.Add(taskRepo.Get(selectedTaskId));
            }
        }
    }
}

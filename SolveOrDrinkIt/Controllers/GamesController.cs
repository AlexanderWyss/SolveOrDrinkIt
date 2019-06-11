using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SolveOrDrinkIt.Models;
using SolveOrDrinkIt.Repositories;

namespace SolveOrDrinkIt.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private static List<int> currentGames = new List<int>();

        private GameRepository repo;
        private DeckRepository deckRepo;
        public GamesController()
        {
            SolveOrDrinkItEntities db = new SolveOrDrinkItEntities();
            repo = new GameRepository(db);
            deckRepo = new DeckRepository(db);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            ViewBag.deckId = new SelectList(deckRepo.GetAll(), "id", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,deckId,playDatetime,name")] Game game)
        {
            if (ModelState.IsValid)
            {
                game.playDatetime = DateTime.Now;
                repo.Add(game);
                repo.Save();
                currentGames.Add(game.id);
                return RedirectToAction("Game", new { game.id });
            }

            ViewBag.deckId = new SelectList(deckRepo.GetAll(), "id", "name", game.deckId);
            return View(game);
        }

        // GET: Games/JoinGame
        public ActionResult JoinGame()
        {
            ViewBag.id = new SelectList(repo.Get(currentGames), "id", "name");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JoinGame(Game game)
        {
            if (ModelState.IsValid && currentGames.Contains(game.id))
            {
                return RedirectToAction("Game", new { game.id });
            }
            ViewBag.id = new SelectList(repo.Get(currentGames), "id", "name");
            return View(game);
        }

        public ActionResult Game(int id)
        {

            string userId = User.Identity.GetUserId();
            Game game = repo.Get(id);
            if (!game.GameUsers.Any(gameUser => gameUser.userId == userId && gameUser.gameId == game.id))
            {
                game.GameUsers.Add(new GameUser()
                {
                    gameId = id,
                    userId = userId
                });
                repo.Save();
            }
            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Game(Game game)
        {
            return Content("");
        }
    }
}

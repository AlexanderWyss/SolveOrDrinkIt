using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SolveOrDrinkIt.Models;
using SolveOrDrinkIt.Repositories;

namespace SolveOrDrinkIt.Controllers
{

    [Authorize]
    public class ScoresController : Controller
    {
        private ScoreRepository repo = new ScoreRepository(new SolveOrDrinkItEntities());

        private UserManager<ApplicationUser> userManager =
            new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));

        // GET: Scores
        public ActionResult Index()
        {
            List<Score> scores = repo.GetAll().OrderBy(s => s.Game.name).ThenBy(s => s.score1).ToList();
            return View(ToViewModel(scores));
        }
        private IEnumerable<ScoreViewModel> ToViewModel(List<Score> scores)
        {
            List<ScoreViewModel> scoreViewModels = new List<ScoreViewModel>();
            foreach (var score in scores)
            {
                scoreViewModels.Add(new ScoreViewModel(score, userManager));
            }
            return scoreViewModels;
        }

    }
}

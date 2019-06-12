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

    [Authorize]
    public class ScoresController : Controller
    {
        private SolveOrDrinkItEntities db = new SolveOrDrinkItEntities();

        // GET: Scores
        public ActionResult Index()
        {
            List<Score> scores = db.Scores.Include(s => s.Game).ToList();
            return View(ToViewModel(scores));
        }
        private IEnumerable<ScoreViewModel> ToViewModel(List<Score> scores)
        {
            List<ScoreViewModel> taskViewModels = new List<ScoreViewModel>();
            foreach (var score in scores)
            {
                taskViewModels.Add(new ScoreViewModel(score));
            }
            return taskViewModels;
        }

    }
}

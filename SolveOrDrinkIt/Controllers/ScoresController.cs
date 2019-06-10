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
            var scores = db.Scores.Include(s => s.Game);
            return View(scores.ToList());
        }
 
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace SolveOrDrinkIt.Models
{
    public class ScoreViewModel
    {
        public ScoreViewModel()
        {

        }
        public ScoreViewModel (Score score, UserManager<ApplicationUser> userManager)
        {
            id = score.id;
            gameName = score.Game.name;
            username = userManager.FindById(score.userId).Email.Split('@')[0];
            this.score = score.score1;
        }
        public int id { get; set; }

        [DisplayName("Game-Name")]
        public string gameName { get; set; }
        [DisplayName("Username")]
        public string username { get; set; }
        [DisplayName("Score")]
        public int score { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SolveOrDrinkIt.Models
{
    public class ScoreViewModel
    {
        public ScoreViewModel()
        {

        }
        public ScoreViewModel (Score score)
        {
            id = score.id;
            name = score.Game.name;
            userId = score.userId;
            this.score = score.score1;
            Game= new GameViewModel( score.Game);
        }
        public int id { get; set; }
    
        public string name { get; set; }
        [DisplayName("Username")]
        public string userId { get; set; }
        [DisplayName("Score")]
        public int score { get; set; }

        public GameViewModel Game { get; set; }
    }
}
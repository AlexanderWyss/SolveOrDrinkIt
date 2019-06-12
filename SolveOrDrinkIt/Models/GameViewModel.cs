using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolveOrDrinkIt.Models
{
    public class GameViewModel
    {
        public GameViewModel()
        {
        }

        public GameViewModel(IEnumerable<Deck> availabeDecks)
        {
            this.availabeDecks = new SelectList(availabeDecks, "id", "name"); ;
        }

        public GameViewModel(Game game, IEnumerable<Deck> availabeDecks)
        {
            id = game.id;
            name = game.name;
            deckId = game.deckId;
            this.availabeDecks = new SelectList(availabeDecks, "id", "name", game.deckId);
        }

        public int id { get; set; }

        [MaxLength(255)]
        [Required]
        public string name { get; set; }

        [Required]
        public int deckId { get; set; }

        public IEnumerable<SelectListItem> availabeDecks { get; set; }
    }
}
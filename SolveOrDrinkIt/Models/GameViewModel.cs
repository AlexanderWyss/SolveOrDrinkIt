using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public GameViewModel(Game game)
        {
            id = game.id;
            name = game.name;
            deckId = game.deckId;
        }
        public int id { get; set; }

        [MaxLength(255)]
        [Required]
        [DisplayName("Game-Name")]
        public string name { get; set; }

        [Required]
        [DisplayName("Deck")]
        public int deckId { get; set; }

        public IEnumerable<SelectListItem> availabeDecks { get; set; }
    }
}
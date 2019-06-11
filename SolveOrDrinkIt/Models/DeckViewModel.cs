using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolveOrDrinkIt.Models
{
    public class DeckViewModel
    {
        public DeckViewModel()
        {

        }
        public DeckViewModel(IEnumerable<Task> tasks)
        {
            setItems(tasks);
        }

        public DeckViewModel(Deck deck, IEnumerable<Task> tasks)
        {
            id = deck.id;
            name = deck.name;
            setItems(tasks);
        }

        private void setItems(IEnumerable<Task> tasks)
        {
            items = tasks.Select(task => new SelectListItem
            {
                Value = task.id.ToString(),
                Text = task.text
            });
        }

        public int id { get; set; }

        [Required]
        public string name { get; set; }
        public IEnumerable<SelectListItem> items { get; set; }
        [Required]
        public int[] selectedIds { get; set; }
    }
}
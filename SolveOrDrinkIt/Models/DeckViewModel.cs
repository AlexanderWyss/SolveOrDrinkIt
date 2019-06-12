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
            setItems(new List<Task>(), tasks);
        }
        public DeckViewModel(Deck deck)
        {
            id = deck.id;
            name = deck.name;
            setItems(deck.Tasks, deck.Tasks);
        }
        public DeckViewModel(Deck deck, IEnumerable<Task> tasks)
        {
            id = deck.id;
            name = deck.name;
            setItems(deck.Tasks, tasks);
        }

        private void setItems(IEnumerable<Task> checkedTasks, IEnumerable<Task> allTasks)
        {
            tasks = allTasks.Select(task => new CheckBoxListItem()
            {
                id = task.id,
                Display = task.text,
                IsChecked = checkedTasks.Any(checkedTask => checkedTask.id == task.id)
            });
        }

        public int? id { get; set; }

        [MaxLength(255)]
        [Required]
        public string name { get; set; }
        public IEnumerable<CheckBoxListItem> tasks { get; set; }

        public IEnumerable<CheckBoxListItem> selectedTasks
        {
            get { return tasks.Where(task => task.IsChecked); }
        }
    }
}
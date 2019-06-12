using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SolveOrDrinkIt.Models
{
    public class TaskViewModel
    {
        public TaskViewModel()
        {
        }

        public TaskViewModel(Task task)
        {
            id = task.id;
            text = task.text;
            drinks = task.drinks; 
            type = (TaskType) task.type;
        }

        public int id { get; set; }

        [MaxLength(255)]
        [Required]
        public string text { get; set; }

        [Required]
        public int drinks { get; set; }

        [Required]
        public TaskType type { get; set; }

    }
}
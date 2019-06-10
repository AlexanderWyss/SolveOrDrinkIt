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
            type = (Type) task.type;
        }

        public int id { get; set; }

        [Required]
        public string text { get; set; }

        [Required]
        public int drinks { get; set; }

        [Required]
        public Type type { get; set; }

    }
    public enum Type
    {
        Test1,
        Test2
    };
}
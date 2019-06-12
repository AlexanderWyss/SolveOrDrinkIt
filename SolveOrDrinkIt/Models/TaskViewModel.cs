using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Foolproof;

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
        [TaskTypeValidation]
        [Required]
        [DisplayName("Text")]
        public string text { get; set; }

        [Required]
        [DisplayName("Drinks")]
        public int drinks { get; set; }

        [Required]
        [DisplayName("Type")]
        public TaskType type { get; set; }

        public Task ToModel()
        {
            return new Task()
            {
                id = id,
                text = text,
                drinks = drinks,
                type = (int) type
            };
        }
    }
}
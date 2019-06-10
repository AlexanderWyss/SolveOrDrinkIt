using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SolveOrDrinkIt.Models;

namespace SolveOrDrinkIt.Repositories
{
    public class TaskRepository : Repository<Task>
    {
        public TaskRepository(SolveOrDrinkItEntities context)
           : base(context)
        {
        }
    }
}
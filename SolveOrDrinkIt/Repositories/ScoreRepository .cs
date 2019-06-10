using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SolveOrDrinkIt.Models;

namespace SolveOrDrinkIt.Repositories
{
    public class ScoreRepository : Repository<Score>
    {
        public ScoreRepository(SolveOrDrinkItEntities context)
           : base(context)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SolveOrDrinkIt.Models;

namespace SolveOrDrinkIt.Repositories
{
    public class DeckRepository : Repository<Deck>
    {
        public DeckRepository(SolveOrDrinkItEntities context)
           : base(context)
        {
        }
    }
}
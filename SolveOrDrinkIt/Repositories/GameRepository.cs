﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SolveOrDrinkIt.Models;

namespace SolveOrDrinkIt.Repositories
{
    public class GameRepository : Repository<Game>
    {
        public GameRepository(SolveOrDrinkItEntities context)
           : base(context)
        {
        }

        public IEnumerable<Game> Get(IEnumerable<int> ids)
        {
            return Context.Games.Where(game => ids.Contains(game.id));
        }
    }
}
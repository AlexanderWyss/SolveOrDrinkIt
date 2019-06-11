using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using Microsoft.AspNet.SignalR;
using SolveOrDrinkIt.Models;
using SolveOrDrinkIt.Repositories;
using Task = System.Threading.Tasks.Task;

namespace SolveOrDrinkIt.Controllers
{
    public class GameHub : Hub
    {
        private GameRepository repo = new GameRepository(new SolveOrDrinkItEntities());

        public Task JoinGame(string gameId)
        {
            return Groups.Add(Context.ConnectionId, gameId);
        }

        public void NextTask(string gameId)
        {
            List<Models.Task> tasks = repo.Get(gameId.AsInt()).Deck.Tasks.ToList();
            string task = tasks[new Random().Next(tasks.Count)].text;
            Clients.Group(gameId).newTask(task);

        }
        public void EndGame(string gameId)
        {
            GamesController.currentGames.Remove(gameId.AsInt());
            Clients.Group(gameId).endGame();
        }
    }
}
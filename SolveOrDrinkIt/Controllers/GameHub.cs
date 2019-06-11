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
        private static Dictionary<string, ComputedTask> lastTasks = new Dictionary<string, ComputedTask>();
        private GameRepository repo = new GameRepository(new SolveOrDrinkItEntities());

        public Task JoinGame(string gameId)
        {
            return Groups.Add(Context.ConnectionId, gameId);
        }

        public void GetTask(string gameId)
        {
            if (lastTasks.ContainsKey(gameId))
            {
                SendTask(gameId, lastTasks[gameId]);
            }
            else
            {
                NextTask(gameId);
            }
        }

        public void Drink(string gameId)
        {
            //TODO add score
            NextTask(gameId);
        }

        public void Completed(string gameId)
        {
           NextTask(gameId);
        }

        private void NextTask(string gameId)
        {
            List<Models.Task> tasks = repo.Get(gameId.AsInt()).Deck.Tasks.ToList();
            ComputedTask task  = new ComputedTask() //TODO
            {
                Task = tasks[new Random().Next(tasks.Count)]
                //TODO user
            };
            lastTasks[gameId] = task;
            SendTask(gameId, task);
        }

        private void SendTask(string gameId, ComputedTask task)
        {
            Clients.Group(gameId).newTask(ShowCompletedButton(task), RenderText(task));
        }

        private bool ShowCompletedButton(ComputedTask task)
        {
            return task.Task.type == 0; //TODO
        }

        private string RenderText(ComputedTask task)
        {
            return task.Task.text; //TODO
        }

        public void EndGame(string gameId)
        {
            GamesController.currentGames.Remove(gameId.AsInt());
            lastTasks.Remove(gameId);
            Clients.Group(gameId).endGame();
        }
    }

    public class ComputedTask
    {
        public Models.Task Task { get; set; }
    }
}
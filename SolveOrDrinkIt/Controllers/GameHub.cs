using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;
using SolveOrDrinkIt.Models;
using SolveOrDrinkIt.Repositories;
using Task = System.Threading.Tasks.Task;

namespace SolveOrDrinkIt.Controllers
{
    public class GameHub : Hub
    {
        private static Dictionary<string, ComputedTask> lastTasks = new Dictionary<string, ComputedTask>();
        private GameRepository repo;
        private ScoreRepository scoreRepo;

        private UserManager<ApplicationUser> userManager =
            new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));

        private Random random = new Random();

        public GameHub()
        {
            SolveOrDrinkItEntities solveOrDrinkItEntities = new SolveOrDrinkItEntities();
            repo = new GameRepository(solveOrDrinkItEntities);
            scoreRepo = new ScoreRepository(solveOrDrinkItEntities);
        }

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
            ComputedTask lastTask = lastTasks[gameId];
            if (((TaskType) lastTask.Task.type).IsSingleUser())
            {
                AddScore(gameId.AsInt(), lastTask.User.Id, lastTask.Task.drinks);
            }
            else
            {
                foreach (GameUser gameUser in repo.Get(gameId.AsInt()).GameUsers)
                {
                    AddScore(gameId.AsInt(), gameUser.userId, lastTask.Task.drinks);
                }
            }
            
            NextTask(gameId);
        }

        private void AddScore(int gameId, string userId, int drinks)
        {
            Score score = scoreRepo.getByGameAndUser(gameId, userId);
            if (score == null)
            {
                scoreRepo.Add(new Score()
                {
                    gameId = gameId,
                    userId = userId,
                    score1 = drinks
                });
            }
            else
            {
                score.score1 += drinks;
                scoreRepo.Update(score);
            }
            scoreRepo.Save();
        }

        public void Completed(string gameId)
        {
            NextTask(gameId);
        }

        private void NextTask(string gameId)
        {
            Game game = repo.Get(gameId.AsInt());
            List<Models.Task> tasks = game.Deck.Tasks.ToList();
            Models.Task task;
            do
            {
                task = tasks[random.Next(tasks.Count)];
            } while (lastTasks.ContainsKey(gameId) && task.id == lastTasks[gameId].Task.id);
            GameUser gameUser = game.GameUsers.ToList()[random.Next(game.GameUsers.Count)];
            ComputedTask computedTask = new ComputedTask()
            {
                Task = task,
                User = userManager.FindById(gameUser.userId)
            };
            lastTasks[gameId] = computedTask;
            SendTask(gameId, computedTask);
        }

        private void SendTask(string gameId, ComputedTask task)
        {
            Clients.Group(gameId).newTask(ShowCompletedButton(task), RenderText(task));
        }

        private bool ShowCompletedButton(ComputedTask task)
        {
            return ((TaskType) task.Task.type).IsCompletable();
        }

        private string RenderText(ComputedTask task)
        {
            return task.Task.text
                .Replace(TaskTypeExtention.DrinkPlaceholder, task.Task.drinks.ToString())
                .Replace(TaskTypeExtention.UserPlaceholder, task.User.Email.Split('@')[0]);
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
        public ApplicationUser User { get; set; }
    }
}
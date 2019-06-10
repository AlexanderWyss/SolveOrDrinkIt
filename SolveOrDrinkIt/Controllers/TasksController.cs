using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolveOrDrinkIt.Models;
using SolveOrDrinkIt.Repositories;

namespace SolveOrDrinkIt.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private TaskRepository repo = new TaskRepository(new SolveOrDrinkItEntities());
        
        public ActionResult Index()
        {
            return View(ToViewModel(repo.GetAll()));
        }

        private IEnumerable<TaskViewModel> ToViewModel(IEnumerable<Task> getAll)
        {
            List<TaskViewModel> taskViewModels = new List<TaskViewModel>();
            foreach (var task in getAll)
            {
                taskViewModels.Add(new TaskViewModel(task));
            }
            return taskViewModels;
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Task task = repo.Get(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(new TaskViewModel(task));
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,text,drinks,type")] Task task)
        {
            if (ModelState.IsValid)
            {
                repo.Add(task);
                repo.Save();
                return RedirectToAction("Index");
            }

            return View(new TaskViewModel(task));
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = repo.Get(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(new TaskViewModel(task));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,text,drinks,type")] Task task)
        {
            if (ModelState.IsValid)
            {
                repo.Update(task);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(new TaskViewModel(task));
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = repo.Get(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(new TaskViewModel(task));
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.Remove(id);
            repo.Save();
            return RedirectToAction("Index");
        }
    }
}

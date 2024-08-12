using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.BLL;
using TaskManagementSystem.ViewModel;

namespace TaskManagementSystem.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class TaskController : Controller
    {
        private readonly TaskServices _taskServices;
        public TaskController()
        {
            _taskServices = new TaskServices();
        }
       

        public ActionResult TaskList(string status)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            // Fetch all tasks for the authenticated user
            List<TaskViewModel> taskList = _taskServices.GetAllTasksByUsername(User.Identity.Name);

            // Filter tasks based on the status if provided
            if (!string.IsNullOrEmpty(status))
            {
                int statusInt;
                if (int.TryParse(status, out statusInt))
                {
                    taskList = taskList.Where(t => t.Status == statusInt).ToList();
                }
            }

            // Return the filtered tasks to the view
            return View(taskList);
        }
    
        public ActionResult CreateTask()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {

                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateTask(TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                task.UserName = User.Identity.Name;
                task.Status = 1;
                task.Date = DateTime.Now;
                bool isVerify = _taskServices.CreateTask(task);
                if (isVerify)
                    return RedirectToAction("TaskList");
                task.ErrorMessage = "Task Creation Failed";
            }

            return View(task);
        }

        public ActionResult Edit(int? taskId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (taskId != 0 && taskId != null)
                {
                    TaskViewModel _task = _taskServices.GetTaskById(int.Parse(taskId.ToString()));
                    ViewBag.StatusList = TaskStatusList();
                    return View(_task);
                }
                else
                {
                    return RedirectToAction("TaskList");
                }
            }
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = _taskServices.UpdateTask(task);
                if (isUpdated)
                {
                    return RedirectToAction("TaskList");
                }
                task.ErrorMessage = "Task Update Failed";
            }

            ViewBag.StatusList = TaskStatusList();
            return View(task);
        }

        public ActionResult Delete(int? taskId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (taskId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var task = _taskServices.GetTaskById(taskId.Value);
            if (task == null)
            {
                return HttpNotFound();
            }

            return View(task);
        }
        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int taskId)
        {
            bool isDeleted = _taskServices.DeleteTask(taskId);
            if (isDeleted)
            {
                return RedirectToAction("TaskList");
            }

            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }

        public IEnumerable<SelectListItem> TaskStatusList()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem(){Text="New", Value="1"},
                new SelectListItem(){Text="InProgress", Value="2"},
                new SelectListItem(){Text="Completed", Value="3"},
                new SelectListItem(){Text="Cancelled", Value="4"}
            };
        }



    }
}
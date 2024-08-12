using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaskManagementSystem.BLL;
using TaskManagementSystem.ViewModel;

namespace TaskManagementSystem.Controllers
{
    public class AccountController : Controller
    {

        private AccountServices accountServices;
        public AccountController()
        {
            accountServices = new AccountServices();
        }
        //GET : Create New Account
        public ActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                TempData["Message"] = "You are already logged in.";
                return RedirectToAction("TaskList", "Task");
            }
        }
        // POST: Create New Account
        [HttpPost]
        public ActionResult Register(RegisterViewModel _register)
        {
            ModelState.Remove("Email");
            if (ModelState.IsValid)
            {
                _register.RoleId = 1;
                
                bool isAccountCreated = accountServices.Register(_register);

                if (isAccountCreated)
                    return RedirectToAction("Login", "Account");
            }

            return View(_register);
        }
        public ActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("TaskList", "Task");
            }
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel _login)
        {
            if (ModelState.IsValid)
            {


                bool isVerify = accountServices.Login(_login);
                if (isVerify)
                {
                    FormsAuthentication.SetAuthCookie(_login.Username, false);
                    if (_login.Username != "admin" && _login.Username != "Admin")
                    {
                        return RedirectToAction("TaskList", "Task");
                    }
                    else
                    {
                        return RedirectToAction("TaskList", "Admin");
                    }
                }

            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
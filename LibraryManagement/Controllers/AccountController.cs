using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.Auth;
using LibraryManagement.DTOs;
using LibraryManagement.EF;

namespace LibraryManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager _userManager = new UserManager();

        // GET: Login
        public ActionResult Login()
        {
            if (Session["User"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = _userManager.GetUser(email, password);
            if (user != null)
            {
                // Box user into session as UserDTO
                var userDTO = new UserDTO
                {
                    Name = user.UserName,
                    Email = user.UserEmail,
                    Role = user.Role
                };
                Session["User"] = userDTO;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid Email or Password";
            return View();
        }

        // GET: Register
        public ActionResult Register()
        {
            if (Session["User"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Register
        [HttpPost]
        public ActionResult Register(string userName, string userEmail, string password)
        {
            if (!_userManager.IsEmailUnique(userEmail))
            {
                ViewBag.Error = "Email already exists.";
                return View();
            }

            var newUser = new User
            {
                UserName = userName,
                UserEmail = userEmail,
                Password = password,
                Role = "User"
            };

            _userManager.AddUser(newUser);
            return RedirectToAction("Login");
        }

        // Logout
        public ActionResult Logout()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login");
            }

            Session["User"] = null;
            TempData["Message"] = "You have been logged out.";
            return RedirectToAction("Login");
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
}
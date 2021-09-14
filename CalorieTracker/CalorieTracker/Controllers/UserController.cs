using CalorieTracker.Services.Interfaces;
using CalorieTrackerApi.Dtos;
using CalorieTrackerApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTracker.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserViewService _userViewService;
        private readonly IUserService _userService;

        public UserController(IUserViewService userViewService, IUserService userService)
        {
            _userViewService = userViewService;
            _userService = userService;
        }

        // GET: UserController
        public ActionResult Index()
        {
            return View(_userService.GetUsers());
        }

        // GET: UserController/Details/5
        public ActionResult Details(string userName)
        {
            return View(_userService.GetUser(userName).Item2);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserDto userDto)
        {
            try
            {
                _userViewService.GetOrCreateUser(userDto, HttpContext);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(string userName)
        {
            return View(_userService.GetUser(userName).Item2);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string userName, UserDto userDto)
        {
            try
            {
                _userService.UpdateUser(userDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: UserController/Delete/5
        public ActionResult DeleteUser(string userName)
        {
            try
            {
                _userService.DeleteUser(userName);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(string userName, IFormCollection collection)
        {
            try
            {
                _userService.DeleteUser(userName);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}

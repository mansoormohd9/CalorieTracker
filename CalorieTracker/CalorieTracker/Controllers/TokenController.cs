using CalorieTrackerApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTracker.Controllers
{
    public class TokenController : Controller
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        // GET: TokenController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TokenController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TokenController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TokenController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TokenController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TokenController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TokenController/Delete/5
        public ActionResult DeleteToken()
        {
            try
            {
                _tokenService.DeleteUserToken(Guid.Parse(HttpContext.Session.GetString(Constants.Constants.ApiKey)));
                HttpContext.Session.Clear();
                return RedirectToAction("Create", "User");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}

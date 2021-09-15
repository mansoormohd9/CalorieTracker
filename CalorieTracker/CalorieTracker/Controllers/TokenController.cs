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
            try
            {
                return View(_tokenService.GetUserTokens());
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: TokenController/Details/5
        public ActionResult Details(string userName)
        {
            try
            {
                return View(_tokenService.GetUserToken(userName));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: TokenController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TokenController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTokenDto createTokenDto)
        {
            try
            {
                _ = _tokenService.CreateUserToken(createTokenDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: TokenController/Edit/5
        public ActionResult Edit(string userName)
        {
            try
            {
                return View(_tokenService.GetUserToken(userName));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: TokenController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateTokenDto createTokenDto)
        {
            try
            {
                _ = _tokenService.RefreshUserToken(createTokenDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: TokenController/Delete/5
        public ActionResult DeleteToken(Guid guid)
        {
            try
            {
                _tokenService.DeleteUserToken(guid);
                if (guid.Equals(HttpContext.Session.GetString(Constants.Constants.ApiKey)))
                {
                    HttpContext.Session.Clear();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}

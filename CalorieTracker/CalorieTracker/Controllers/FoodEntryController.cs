using CalorieTrackerApi.Dtos;
using CalorieTrackerApi.Models;
using CalorieTrackerApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTracker.Controllers
{
    public class FoodEntryController : Controller
    {
        private readonly IFoodEntryService _foodEntryService;

        public FoodEntryController(IFoodEntryService foodEntryService)
        {
            _foodEntryService = foodEntryService;
        }

        // GET: FoodEntryController
        public ActionResult Index()
        {
            try
            {
                return View(_foodEntryService.GetFoodEntries(HttpContext.Session.GetString(Constants.Constants.UserNamekey)));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: FoodEntryController/Details/5
        public ActionResult Details(Guid guid)
        {
            try
            {
                return View(_foodEntryService.GetFoodEntry(HttpContext.Session.GetString(Constants.Constants.UserNamekey), guid).Item2);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: FoodEntryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FoodEntryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateFoodEntryDto foodEntry)
        {
            try
            {
                _foodEntryService.CreateFoodEntry(HttpContext.Session.GetString(Constants.Constants.UserNamekey), foodEntry);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: FoodEntryController/Edit/5
        public ActionResult Edit(Guid guid)
        {
            try
            {
                return View(_foodEntryService.GetFoodEntry(HttpContext.Session.GetString(Constants.Constants.UserNamekey), guid).Item2);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: FoodEntryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UpdateFoodEntryDto foodEntry)
        {
            try
            {
                _foodEntryService.UpdateFoodEntry(HttpContext.Session.GetString(Constants.Constants.UserNamekey), foodEntry);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: FoodEntryController/Delete/5
        public ActionResult DeleteFoodEntry(Guid guid)
        {
            try
            {
                _foodEntryService.DeleteFoodEntry(HttpContext.Session.GetString(Constants.Constants.UserNamekey), guid);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}

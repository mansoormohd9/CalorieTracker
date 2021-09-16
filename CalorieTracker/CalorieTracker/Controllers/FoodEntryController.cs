using AutoMapper;
using CalorieTracker.Authentication;
using CalorieTracker.ViewModel;
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
    [AuthenticationRequired]
    public class FoodEntryController : Controller
    {
        private readonly IFoodEntryService _foodEntryService;
        private readonly IMapper _mapper;

        public FoodEntryController(IFoodEntryService foodEntryService, IMapper mapper)
        {
            _foodEntryService = foodEntryService;
            _mapper = mapper;
        }

        // GET: FoodEntryController
        public ActionResult Index(FoodEntryFilter foodEntryFilter)
        {
            try
            {
                var foodEntries = _foodEntryService.GetFoodEntries(HttpContext.Session.GetString(Constants.Constants.UserNamekey), foodEntryFilter);
                var foodEntryViewModel = new FoodEntryViewModel
                {
                    FoodEntries = foodEntries,
                    FoodEntryFilter = foodEntryFilter
                };
                return View(foodEntryViewModel);
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
                var createResult = _foodEntryService.CreateFoodEntry(HttpContext.Session.GetString(Constants.Constants.UserNamekey), foodEntry);
                if (!createResult.Item1)
                {
                    ModelState.AddModelError(nameof(FoodEntryDto.Calories), createResult.Item2);
                    return View(_mapper.Map<FoodEntryDto>(foodEntry));
                }
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
                var updateResult = _foodEntryService.UpdateFoodEntry(HttpContext.Session.GetString(Constants.Constants.UserNamekey), foodEntry);
                if (!updateResult.Item1)
                {
                    ModelState.AddModelError(nameof(FoodEntryDto.Calories), updateResult.Item2);
                    return View(_mapper.Map<FoodEntryDto>(foodEntry));
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Filter(int id, FoodEntryFilter foodEntryFilter)
        {
            try
            {
                var foodEntries = _foodEntryService.GetFoodEntries(HttpContext.Session.GetString(Constants.Constants.UserNamekey), foodEntryFilter);
                var foodEntryViewModel = new FoodEntryViewModel
                {
                    FoodEntries = foodEntries,
                    FoodEntryFilter = foodEntryFilter
                };
                return RedirectToAction(nameof(Index), foodEntryViewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}

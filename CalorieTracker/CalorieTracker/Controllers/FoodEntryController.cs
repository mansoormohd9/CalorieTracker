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
        // GET: FoodEntryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FoodEntryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FoodEntryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FoodEntryController/Create
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

        // GET: FoodEntryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FoodEntryController/Edit/5
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

        // GET: FoodEntryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FoodEntryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}

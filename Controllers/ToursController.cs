using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TourWebsite.Data;
using TourWebsite.Data.Entities;
using TourWebsite.Services;
using TourWebsite.ViewModels;

namespace TourWebsite.Controllers
{
    public class ToursController : Controller
    {
        private readonly IToursService _toursService;
        private readonly ICategoriesService _categoriesService;
        private int PAGESIZE = 10;
        public ToursController(IToursService toursService, ICategoriesService categoriesService)
        {
            _toursService = toursService;
            _categoriesService = categoriesService;
        }

        // GET: tours
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber, int? categoryId)
        {
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("TourName") ? "TourName_desc" : "";
            ViewData["ReleaseDateSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("DateCreated") ? "DateCreated_desc" : "DateCreated";
            var categories = await _categoriesService.GetAll();
            ViewData["CategoryId"] = categories.Select(x => new SelectListItem()
            {
                Text = x.CategoryName,
                Value = x.Id.ToString(),
                Selected = categoryId.HasValue && categoryId == x.Id
            });
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;

            return View(await _toursService.GetAllFilter(sortOrder, currentFilter, searchString, pageNumber, PAGESIZE, categoryId));
        }

        // GET: tours/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var tour = await _toursService.GetById(id);
            if (tour == null)
            {
                return NotFound();
            }
            return View(tour);
        }

        // GET: tours/Create
        public async Task<ActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _categoriesService.GetAll(), "Id", "CategoryName");
            return View();
        }

        // POST: tours/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToursRequest request)
        {
            if (ModelState.IsValid)
            {
                await _toursService.Create(request);
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        // GET: tours/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var tour = await _toursService.GetById(id);
            if (tour == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _categoriesService.GetAll(), "Id", "CategoryName");
            return View(tour);
        }

        // POST:tours/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ToursViewModel tour)
        {
            if (id != tour.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _toursService.Update(tour);
                return RedirectToAction(nameof(Index));
            }
            return View(tour);
        }

        // GET: Tours/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var tour = await _toursService.GetById(id);
            if (tour == null)
            {
                return NotFound();
            }
            return View(tour);
        }

        // POST:Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _toursService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

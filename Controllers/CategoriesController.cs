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
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private int PAGESIZE = 10;
        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        // GET: categorys
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("CategoryName") ? "categoryName_desc" : "";
            ViewData["ReleaseDateSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("release_date") ? "release_date_desc" : "release_date";

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;

            return View(await _categoriesService.GetAllFilter(sortOrder, currentFilter, searchString, pageNumber, PAGESIZE));
        }

        // GET: categorys/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoriesService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: categorys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: categorys/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriesRequest request)
        {
            if (ModelState.IsValid)
            {
                await _categoriesService.Create(request);
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: categorys/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoriesService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: categorys/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriesViewModel category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _categoriesService.Update(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: categorys/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoriesService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: categorys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoriesService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

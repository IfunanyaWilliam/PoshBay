﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PoshBay.Contracts;
using PoshBay.Data.Models;
using PoshBay.Data.ViewModels;

namespace PoshBay.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, 
                                 IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var newCategory = _mapper.Map<Category>(model);
            var result =  await _categoryRepository.AddAsync(newCategory);
            if (!result)
            {
                TempData["Error"] = "Category could not be added";
                return View(model);
            }
            var message = TempData["Success"] = "Category successfully added";
            return RedirectToAction("List", message);
        }

        public async Task<IActionResult> List()
        {
            var category = await _categoryRepository.GetAll();
            if (category == null)
            {
                TempData["Error"] = "Category not found";
                return View();
            }
            //var model = _mapper.Map<IEnumerable<CategoryViewModel>>(category);
            return View(category);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                TempData["Error"] = "Category not found";
                return View();
            }
            var model = _mapper.Map<CategoryViewModel>(category);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var category = await _categoryRepository.GetByIdAsync(model.CategoryId);
            category.Name = model.Name;
            var result = await _categoryRepository.UpdateAsync(category);
            if (!result)
            {
                TempData["Error"] = "Category could not be updated";
                return View(category);
            }
            var message = TempData["Success"] = "Category successfully updated";
            return RedirectToAction("List");
            return await Task.Run(() => Ok());
        }

        public async Task<IActionResult> Delete(string id)
        {
            var categoryN = await _categoryRepository.DeleteAsync(id);
            if (!categoryN)
            {
                return View();
            }
            TempData["Delete-Success"] = "Category successfully deleted";
            return RedirectToAction("List");
        }
    }
}

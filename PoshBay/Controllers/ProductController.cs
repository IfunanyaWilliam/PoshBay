using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PoshBay.Contracts;
using PoshBay.Data.Models;

namespace PoshBay.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepo;

        public ProductController(IMapper mapper, IProductRepository? poductRepo)
        {
            _mapper = mapper;
            _productRepo = poductRepo;
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            
            var productToAdd = await _productRepo.AddAsync(product);
            if(!productToAdd)
            {
                TempData["Error"] = "Product could not be added";
                return View(product);
            }
            return RedirectToAction("Details", new { id = product.ProductId });
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}

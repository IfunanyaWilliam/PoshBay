using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PoshBay.Contracts;
using PoshBay.Data.Models;
using PoshBay.Data.ViewModels;
using PoshBay.Services;
using System.IO;
using System.Threading.Tasks;

namespace PoshBay.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepo;
        private readonly IImageService _imageService;

        public ProductController(IMapper mapper, 
                                 IProductRepository poductRepo,
                                 IImageService imageService)
        {
            _mapper = mapper;
            _productRepo = poductRepo;
            _imageService = imageService;
        }
        
        public async Task<IActionResult> Create()
        {
            ViewBag.Message = await _productRepo.GetAllCategoryAsync();
            var Categories = ViewBag.Message;
            //Console.WriteLine(ViewBag.Message);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //Add product photo to Cloudinary and generate absolute path
            string imgUrl = "";
            
            if (model.ImagePath is not null)
            {
                var filePath = Path.GetTempFileName();

                using (var stream = System.IO.File.Create(filePath))
                {
                    await model.ImagePath.CopyToAsync(stream);
                }
                var uploadResult = await _imageService.UploadImage(filePath);
                imgUrl = uploadResult;

            }

            var product = _mapper.Map<Product>(model);
            product.ImagePath = imgUrl;
            var productToAdd = await _productRepo.AddAsync(product);
            if(!productToAdd)
            {
                TempData["Error"] = "Product could not be added";
                return View(product);
            }
            return RedirectToAction("Index", "Home");
            //return RedirectToAction("Details", new { id = product.ProductId });
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var productToEdit = _productRepo.GetByIdAsync(product.ProductId);
            if (productToEdit != null)
            {
                TempData["Error"] = "Product could not be found";
                return View(product);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}

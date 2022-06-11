using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PoshBay.Contracts;
using PoshBay.Data.Models;
using PoshBay.Data.ViewModels;
using PoshBay.DTO;
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
        [ValidateAntiForgeryToken]
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
            return RedirectToAction("Detail", new { id = product.ProductId });
            //return RedirectToAction("Details", new { id = product.ProductId });
        }

        public async Task<IActionResult> Edit(string id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            var prodToDTO = _mapper.Map<ProductEditDTO>(product);
            ViewBag.Message = await _productRepo.GetAllCategoryAsync();
            return View(prodToDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductEditDTO product)
        {

            var productToEdit = _productRepo.GetByIdAsync(product.ProductId);
            if (productToEdit == null)
            {
                TempData["Error"] = "Product could not be found";

                //ViewBag.Message = await _productRepo.GetAllCategoryAsync();
                return View(product);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string productId) //the parameter name productId should match the route parameter int the view that calls this method
        {
            var product = await _productRepo.GetByIdAsync(productId);
            if (product == null)
            {
                TempData["Error"] = "Product could not be found";
                return RedirectToAction("Index", "Home");
            }
            var prodToDetail = _mapper.Map<ProductDetailViewModel>(product);
            return View(prodToDetail);
        }
    }
}

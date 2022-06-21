using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoshBay.Contracts;
using PoshBay.Data.Models;
using PoshBay.Data.ViewModels;
using PoshBay.DTO;
using PoshBay.Services;

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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Message =  _productRepo.GetAllCategory();
            var Categories = ViewBag.Message;
            //Console.WriteLine(ViewBag.Message);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = _productRepo.GetAllCategory();
                var Categories = ViewBag.Message;
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            var prodToDTO = _mapper.Map<ProductEditDTO>(product);
            ViewBag.Message =  _productRepo.GetAllCategory();
            return View(prodToDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(ProductEditDTO product)
        {

            var productToEdit = await _productRepo.GetByIdAsync(product.ProductId);
            if (productToEdit == null)
            {
                TempData["Error"] = "Product could not be Modified";

                ViewBag.Message =  _productRepo.GetAllCategory();
                //var prod = _mapper.Map<ProductEditDTO>(productToEdit);
                return View(product);
            }

            if (product.NewImagePath != null || product.NewImagePath?.Length > 0)
            {
                TempData["Image"] = "Image file selected";
                var filePath = Path.GetTempFileName();

                using (var stream = System.IO.File.Create(filePath))
                {
                    await product.NewImagePath.CopyToAsync(stream);
                }
                var imageUplaodURL = await _imageService.UploadImage(filePath);

                if(imageUplaodURL == null)
                {
                    //Map product back to productDTO and return to view
                    var prodToDTO = _mapper.Map<ProductEditDTO>(productToEdit);
                    ViewBag.Message = _productRepo.GetAllCategory(); 
                    TempData["Error"] = "Could not update product";
                    return View(productToEdit);
                }
                //Assign modified values to products
                productToEdit.ImagePath = imageUplaodURL;
            }
            productToEdit.Name = product.Name;
            productToEdit.Price = product.Price;
            productToEdit.Description = product.Description;
            productToEdit.QuantityInStock = product.QuantityInStock;

            var updateProd = await _productRepo.UpdateAsync(productToEdit);

            if(updateProd)
            {
                TempData["Success"] = $"Product successfully modified";
                return RedirectToAction("Detail", new { id = productToEdit.ProductId });
            }

            return View(product);
        }

        

        [HttpGet]
        public async Task<IActionResult> Detail(string id) //the parameter name productId should match the route parameter in the view(Index) that calls this method
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null)
            {
                TempData["Error"] = "Product could not be found";
                return RedirectToAction("Index", "Home");
            }
            var prodToDetail = _mapper.Map<ProductDetailViewModel>(product);
            return View(prodToDetail);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null)
            {
                TempData["Error"] = "Product not found";
                return View();
            }

            var prodToDelete = _mapper.Map<ProductDetailViewModel>(product);
            return View(prodToDelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(ProductDetailViewModel product)
        {
            var prod = await _productRepo.GetByIdAsync(product.ProductId);
            if(prod == null)
            {
                TempData["Error"] = "Product not found";
                return View(product);
            }

            
             bool deletedProd = await _productRepo.DeleteAsync(prod);
            if (!deletedProd)
            {
                return View(product);
            }
            TempData["Product-Delete-Success"] = "Product successfully deleted";
            return RedirectToAction("Index", "Home");
        }

    }
}

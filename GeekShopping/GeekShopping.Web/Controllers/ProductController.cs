using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IServiceProduct _serviceProduct;

        public ProductController(IServiceProduct serviceProduct)
        {
            _serviceProduct = serviceProduct ?? throw new ArgumentNullException(nameof(serviceProduct));
        }

        public async Task<IActionResult> ProductIndex()
        {
           var products = await _serviceProduct.FindaAllProducts();
            return View(products);
        }


        public async Task<IActionResult> ProductCreate()
        {           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel productModel)
        {

            if (ModelState.IsValid)
            {
                var response = await _serviceProduct.CreateProduct(productModel);
                if (response != null) return RedirectToAction(nameof(ProductIndex));
            }
            return View(productModel);
        }

        public async Task<IActionResult> ProductUpdate(int id)
        {
            var model = await _serviceProduct.FindbProductById(id);
            if(model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _serviceProduct.UpdateProduct(model);
                if (response != null) return RedirectToAction(nameof(ProductIndex));
            }
            return View(model);
        }

        public async Task<IActionResult> ProductDelete(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _serviceProduct.DeleteProductById(id);
                if (response) return RedirectToAction(nameof(ProductIndex));
            }
            return View(false);
        }

        //[HttpPost]
        //public async Task<IActionResult> ProductDelete(ProductModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var response = await _serviceProduct.DeleteProductById(model.Id);
        //        if (response) return RedirectToAction(nameof(ProductIndex));
        //    }
        //    return View(model);
        //}
        
        //public async Task<IActionResult> ProductIndex()
        //{
        //   var products = await _serviceProduct.FindaAllProducts();
        //    return View(products);
        //}


    }
}

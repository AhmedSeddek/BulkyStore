using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BulkyStore.Models;
using BulkyStore.Models.ViewModels;
using BulkyStore.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Utility;
using BulkyStore.Utility;
using Microsoft.AspNetCore.Http;

namespace BulkyStore.Areas.Customer.Controllers.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductModel> productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim !=null)
            {
                var count = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value).ToList()
                                                                                        .ToList().Count();
                    
                HttpContext.Session.SetInt32(SD.ssShoppingCart, count);
            }


            return View(productList);
        }
        public IActionResult Details(int id)
        {
            var productFromDb = _unitOfWork.Product.
                GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,CoverType");
            ShoppingCart cartObj = new ShoppingCart() { 
                Product = productFromDb,
                ProductId = productFromDb.Id
                
            };

            return View(cartObj);
        }

       

            public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart cartObject)
        {
            cartObject.Id = 0;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                
                //add to cart
                var claimIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
                cartObject.ApplicationUserId = claim.Value;
                ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                    u => u.ApplicationUserId == cartObject.ApplicationUserId 
                    && u.ProductId == cartObject.ProductId,
                    includeProperties:"Product"
                    );
                if (cartFromDb == null)
                {
                    //no product records in database for this user 
                    _unitOfWork.ShoppingCart.Add(cartObject);
                }
                else {
                    cartFromDb.Count += cartObject.Count;
                    //_unitOfWork.ShoppingCart.Update(cartFromDb);
                }
                _unitOfWork.Save();
                var count = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == cartObject.ApplicationUserId)
                                                                                         .ToList().Count();
                //HttpContext.Session.SetObject(SD.ssShoppingCart, count);
                HttpContext.Session.SetInt32(SD.ssShoppingCart, count);
                return RedirectToAction(nameof(Index));
            } 
            else {
                var productFromDb = _unitOfWork.Product.
                    GetFirstOrDefault(u => u.Id == cartObject.ProductId, includeProperties: "Category,CoverType");
                ShoppingCart cartObj = new ShoppingCart()
                {
                    Product = productFromDb,
                    ProductId = productFromDb.Id

                };

                return View(cartObj);

            }
        }
    }
}

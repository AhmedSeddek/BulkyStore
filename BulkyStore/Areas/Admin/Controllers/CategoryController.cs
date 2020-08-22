using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyStore.DataAccess.Repository.IRepository;
using BulkyStore.Models;
using BulkyStore.Models.ViewModels;
using BulkyStore.Utility;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(int productPage = 1)
        {
            CategoryVM categoryVM = new CategoryVM()
            {
                Categories = await _unitOfWork.Category.GetAllAsync()

            };
            var count = categoryVM.Categories.Count();
            categoryVM.Categories = categoryVM.Categories.OrderBy(p => p.Name).Skip((productPage - 1) * 2).Take(2).ToList();
            categoryVM.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = 2,
                TotalItem = count,
                UrlParam = "/Admin/Category/Index?productPage=:"
            };
            return View(categoryVM);
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            CategoryModel category = new CategoryModel();
            if (id == null) {
                //create
                return View(category);
            }
            //edit
            category = await _unitOfWork.Category.GetAsync(id.GetValueOrDefault());
            if (category == null) {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CategoryModel category) {
            if (ModelState.IsValid) {
                if (category.Id == 0)
                {
                    await _unitOfWork.Category.AddAsync(category);
                }
                else {
                    _unitOfWork.Category.Update(category);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAllAsync() {
            var allObj = await _unitOfWork.Category.GetAllAsync();
            return Json(new { data = allObj});
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            var objFromDb = await _unitOfWork.Category.GetAsync(id);
            if (objFromDb == null) {
                //TempData["Error"] = "error deleting category";
                return Json(new { success = false, message = "Error while deleting"});
            }
            await _unitOfWork.Category.RemoveAsync(objFromDb);
            _unitOfWork.Save();
            //TempData["Success"] = "deleted successfully";
            return Json(new { success = true, message = "Delete Successful"});
        }
        #endregion
    }
}

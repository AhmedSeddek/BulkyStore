using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyStore.DataAccess.Repository.IRepository;
using BulkyStore.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BulkyStore.Utility;

namespace BulkyStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            CompanyModel company = new CompanyModel();
            if (id == null) {
                //create
                return View(company);
            }
            //edit
            company = _unitOfWork.Company.Get(id.GetValueOrDefault());
            if (company == null) {
                return NotFound();
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CompanyModel company) {
            if (ModelState.IsValid) {
                if (company.Id == 0)
                {
                    _unitOfWork.Company.Add(company);
                }
                else {
                    _unitOfWork.Company.Update(company);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() {
            var allObj = _unitOfWork.Company.GetAll();
            return Json(new { data = allObj});
        }
        [HttpDelete]
        public IActionResult Delete(int id) {
            var objFromDb = _unitOfWork.Company.Get(id);
            if (objFromDb == null) {
                return Json(new { success = false, message = "Error while deleting"});
            }
            _unitOfWork.Company.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful"});
        }
        #endregion
    }
}

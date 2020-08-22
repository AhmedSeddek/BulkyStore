using BulkyStore.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BulkyStore.ViewComponents
{
    public class UserNameViewComponent: ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserNameViewComponent(IUnitOfWork unitOfwork)
        {
            _unitOfWork = unitOfwork;
        }

        public async Task<IViewComponentResult> InvokeAsync() {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims =  claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userFromDb =  _unitOfWork.User.GetFirstOrDefault(u => u.Id == claims.Value);
            return View(userFromDb); 
        }
    }
}

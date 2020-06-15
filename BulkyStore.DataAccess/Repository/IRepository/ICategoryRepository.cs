using BulkyStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyStore.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository: IRepository<CategoryModel>
    {
        void Update(CategoryModel category);

    }
}

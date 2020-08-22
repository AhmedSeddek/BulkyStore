using BulkyStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyStore.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository: IRepositoryAsync<CategoryModel>
    {
        void Update(CategoryModel category);

    }
}

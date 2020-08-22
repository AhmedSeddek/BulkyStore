using BulkyStore.DataAccess.Data;
using BulkyStore.DataAccess.Repository.IRepository;
using BulkyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyStore.DataAccess.Repository
{
    public class CategoryRepository : RepositoryAsync<CategoryModel>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CategoryModel category)
        {
            var objFromDb = _db.Categories.FirstOrDefault(s => s.Id == category.Id);
            if(objFromDb != null) { 
                objFromDb.Name = category.Name;
            }
            
        }
    }
}

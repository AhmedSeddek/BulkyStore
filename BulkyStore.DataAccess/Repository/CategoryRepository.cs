﻿using BulkyStore.DataAccess.Data;
using BulkyStore.DataAccess.Repository.IRepository;
using BulkyStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyStore.DataAccess.Repository
{
    public class CategoryRepository : Repository<CategoryModel>, ICategoryRepository
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

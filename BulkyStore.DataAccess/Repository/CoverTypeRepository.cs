using BulkyStore.DataAccess.Data;
using BulkyStore.DataAccess.Repository.IRepository;
using BulkyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyStore.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverTypeModel>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(CoverTypeModel coverType)
        {
            var objFromDb = _db.CoverTypes.FirstOrDefault(s => s.Id == coverType.Id);
            if (objFromDb != null) {
                objFromDb.Name = coverType.Name;
            }
        }
    }
}

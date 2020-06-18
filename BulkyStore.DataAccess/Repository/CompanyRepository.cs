using BulkyStore.DataAccess.Data;
using BulkyStore.DataAccess.Repository.IRepository;
using BulkyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyStore.DataAccess.Repository
{
    public class CompanyRepository : Repository<CompanyModel>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CompanyModel company)
        {
            //var objFromDb = _db.Companies.FirstOrDefault(s => s.Id == company.Id);
            //if (objFromDb != null)
            //{
            //    objFromDb.Name = company.Name;
            //}
            _db.Update(company);

        }
    }
}

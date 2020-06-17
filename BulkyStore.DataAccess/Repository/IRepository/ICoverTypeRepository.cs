using BulkyStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyStore.DataAccess.Repository.IRepository
{
    public interface ICoverTypeRepository: IRepository<CoverTypeModel>
    {
        void Update(CoverTypeModel coverType);
    }
}

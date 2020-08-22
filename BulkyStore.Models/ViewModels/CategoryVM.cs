using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyStore.Models.ViewModels
{
    public class CategoryVM
    {
        public IEnumerable<CategoryModel> Categories { get; set; }
        public PagingInfo PagingInfo { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgmtSlices.Repository.Repository.ProductCategoryRepo
{
    public interface IProductCategoryRepository : IGenericRepository
    {
        Task<int> GetCountByCategoryNameAsync(string categoryName);
    }
}

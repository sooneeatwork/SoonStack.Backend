﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.UseCases.ProductSlices
{
    public interface IProductQueryServices
    {
        public Task<int> GetProductStockCount(int productId);
    }

}

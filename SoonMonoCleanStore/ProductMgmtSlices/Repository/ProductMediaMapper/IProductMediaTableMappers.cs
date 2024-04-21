﻿using Core.Domain.DomainModel.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgmtSlices.Repository.ProductMediaMapper
{
    public interface IProductMediaTableMappers
    {
        Dictionary<string,object> CreateMapForInsert(ProductMedia productMedia);
    }
}

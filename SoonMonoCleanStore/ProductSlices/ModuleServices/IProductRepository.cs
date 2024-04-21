﻿using Core.Domain.RepoInterface;

namespace ProductSlices.ModuleServices
{
    public interface IProductRepository: IGenericRepository
    {
        Task<int> GetStockCountByIdAsync(int productId);
    }
}
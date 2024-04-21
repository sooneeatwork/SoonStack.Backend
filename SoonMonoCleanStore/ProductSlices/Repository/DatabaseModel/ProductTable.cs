﻿using ProductSlices.UseCases;

namespace ProductSlices.Repository.DatabaseModel
{
    public class ProductTable
    {
        public const string TableName = "Product";
        public int Id { get; set; }
        public string Name { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public int StockQuantity { get; private set; }


       
        
    }
}

using ProductMgmtSlices.UseCases.ProductUseCases;
using System;

namespace ProductMgmtSlices.Repository.DatabaseModel
{
    public class ProductTable
    {
        // Corresponding to the database fields
        public const string TableName = "products.products";
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public decimal price { get; set; }
        public int stock_quantity { get; set; }
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }


        public static IEnumerable<ProductDto> ToProductDTO(IEnumerable<ProductTable> products)
        {
            ArgumentNullException.ThrowIfNull(products, nameof(products));
            List<ProductDto> productsDto = new List<ProductDto>();

            foreach (var product in products)
            {
                productsDto.Add(new ProductDto(product.id,
                                                product.name,
                                                product.description,
                                                product.price,
                                                product.stock_quantity));
            }

            return productsDto;
        }




        // Additional methods if needed for database-related operations
        // ...
    }
}

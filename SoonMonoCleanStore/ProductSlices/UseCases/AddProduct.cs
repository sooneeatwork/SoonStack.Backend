namespace ProductSlices.UseCases
{
    public record AddProductCommand(
        string Name,
        decimal Price,
        int Quantity,
        string Description) : IRequest<Result<int>>;

    public class AddProductHandler : IRequestHandler<AddProductCommand, Result<int>>
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductTableMappers _productTableMappers;

        public AddProductHandler(IGenericRepository genericRepository,
                                 IProductRepository productRepository,
                                 IProductTableMappers productTableMappers)
        {
            _genericRepository = genericRepository;
            _productRepository = productRepository;
            _productTableMappers = productTableMappers;
        }

        public async Task<Result<int>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(request);
                var productCount = await _productRepository.GetCountByProductNameAsync(request.Name);

                if (Product.IsProductExists(productCount))
                    return Result<int>.Failure($"Product with Nmae {request.Name} existed.");

                var product = Product.CreateProduct(request.Name, request.Price, request.Description, request.Quantity);
                var productTableData = _productTableMappers.CreateMapForInsert(product);
                int productId = await _genericRepository.InsertOneAsync<ProductTable>(productTableData);

                return productId > 0
                    ? Result<int>.Success(productId)
                    : Result<int>.Failure("Failed to add the product.");
            }
            catch (Exception ex)
            {
                // Log the exception if logging is setup
                // Consider the type of exception to provide a meaningful message
                return Result<int>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}

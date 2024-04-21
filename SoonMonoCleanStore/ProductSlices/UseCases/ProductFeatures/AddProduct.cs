using SharedKernel.Domain.DomainModel.ProductModel;

namespace ProductSlices.UseCases
{
    public record AddProductCommand(
        string Name,
        decimal Price,
        int Quantity,
        string Description) : IRequest<Result<int>>;

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Result<int>>
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductTableMappers _productTableMappers;
        private readonly ILogger _logger;

        public AddProductCommandHandler(IGenericRepository genericRepository,
                                        IProductRepository productRepository,
                                        IProductTableMappers productTableMappers,
                                        ILogger logger)
        {
            _genericRepository = genericRepository;
            _productRepository = productRepository;
            _productTableMappers = productTableMappers;
            _logger = logger;
        }

        public async Task<Result<int>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            Result<int> result;

            try
            {
                
                var productCount = await _productRepository.GetCountByProductNameAsync(request.Name);

                if (Product.IsProductExists(productCount))
                    return Result<int>.Failure($"Product with Name {request.Name} existed.");

                var product = Product.CreateProduct(request.Name, request.Price, request.Description, request.Quantity);
                var productTableData = _productTableMappers.CreateMapForInsert(product);
                int productId = await _genericRepository.InsertOneAsync<ProductTable>(productTableData);

                result = productId > 0
                    ? Result<int>.Success(productId)
                    : Result<int>.Failure("Failed to add the product.");
            }
            catch (Exception ex)
            {
                // Log the exception if logging is setup
                // Consider the type of exception to provide a meaningful message
                _logger.LogError(nameof(AddProductCommand), ex);
                result = Result<int>.Failure($"An error occurred: {ex.Message}");
            }

            return result;
        }
    }
}

using ProductSlices.Repository.Repository.ProductRepo;
using SharedKernel.UseCases.Wrapper;
using System.Collections.Generic;

namespace ProductSlices.UseCases.ProductFeatures
{
    // Query to get all products
    public struct SearchAllProductQuery : IRequest<Result<IEnumerable<ProductDto>>> { }
    public record ProductDto(long Id, string Name, decimal Price, string Description, int StockQuantity);

    // Handler for the query
    public class SearchAllProductHandler : IRequestHandler<SearchAllProductQuery, Result<IEnumerable<ProductDto>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public SearchAllProductHandler(IProductRepository productRepository, IMapper mapper, ILogger logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<ProductDto>>> Handle(SearchAllProductQuery request, CancellationToken cancellationToken)
        {
            Result<IEnumerable<ProductDto>> result;

            try
            {
                var products = await _productRepository.GetAllAsync<ProductTable>();
                if (products == null || !products.Any())
                    return Result<IEnumerable<ProductDto>>.Failure("No products found.");

                var productDtos = _mapper.MapEnumerable<ProductTable, ProductDto>(products);
                result = Result<IEnumerable<ProductDto>>.Success(productDtos);

            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(SearchAllProductQuery), ex);
                result = Result<IEnumerable<ProductDto>>.Failure("Failed to get product info");
            }

            return result;
        }
    }
}

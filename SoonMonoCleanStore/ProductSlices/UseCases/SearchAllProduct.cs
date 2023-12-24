using ProductSlices.Repository.DatabaseModel;
using ProductSlices.Repository.Repository;

namespace ProductSlices.UseCases
{
    // Query to get all products
    public struct SearchAllProductQuery : IRequest<Result<IEnumerable<ProductDto>>> { }
    public record ProductDto(long Id, string Name, decimal Price, string Description, int StockQuantity);

    // Handler for the query
    public class SearchAllProductHandler : IRequestHandler<SearchAllProductQuery, Result<IEnumerable<ProductDto>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public SearchAllProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ProductDto>>> Handle(SearchAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync<ProductTable>();
            if (products == null || !products.Any())
                return Result<IEnumerable<ProductDto>>.Failure("No products found.");

            var productDtos = _mapper.MapEnumerable<ProductTable, ProductDto>(products);

            return Result<IEnumerable<ProductDto>>.Success(productDtos);
        }
    }
}

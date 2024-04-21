using Core.UseCases.MapperInterface;
using Core.UseCases.Wrapper;
using ProductSlices.Repository.Repository.ProductRepo;

namespace ProductSlices.UseCases.ProductFeatures
{
    public record SearchProductByIdQuery(long Id) : IRequest<Result<ProductDetailDto>> { }
    public record ProductDetailDto(long Id, string Name, decimal Price, string Description, int StockQuantity);

    public class SearchProductByIdHandler : IRequestHandler<SearchProductByIdQuery, Result<ProductDetailDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SearchProductByIdHandler(IProductRepository productRepository, IMapper mapper, ILogger logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<ProductDetailDto>> Handle(SearchProductByIdQuery request, CancellationToken cancellationToken)
        {
            Result<ProductDetailDto> result;

            try
            {
                var product = await _productRepository.GetByIdAsync<ProductTable>(request.Id);

                if (product == null)
                    return Result<ProductDetailDto>.Failure($"Product with ID {request.Id} not found.");

                var productDto = _mapper.Map<ProductTable, ProductDetailDto>(product);

                result = Result<ProductDetailDto>.Success(productDto);

            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(SearchProductByIdQuery), ex);
                result = Result<ProductDetailDto>.Failure($"Failed to search product info with id {request.Id}");
            }

            return result;
        }
    }
}

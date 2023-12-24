using ProductSlices.Repository.DatabaseModel;
using ProductSlices.Repository.Repository;

namespace ProductSlices.UseCases
{
    public record SearchProductByIdQuery(long Id) : IRequest<Result<ProductDetailDto>> { }
    public record ProductDetailDto(long Id, string Name, decimal Price, string Description, int StockQuantity);
   
    public class SearchProductByIdHandler : IRequestHandler<SearchProductByIdQuery, Result<ProductDetailDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public SearchProductByIdHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<ProductDetailDto>> Handle(SearchProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync<ProductTable>(request.Id);

            if (product == null)
                return Result<ProductDetailDto>.Failure($"Product with ID {request.Id} not found.");

            var productDto = _mapper.Map<ProductTable, ProductDetailDto>(product);
            return Result<ProductDetailDto>.Success(productDto);
        }
    }
}

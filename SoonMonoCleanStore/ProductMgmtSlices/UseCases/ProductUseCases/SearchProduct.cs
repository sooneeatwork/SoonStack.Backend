using SharedKernel.UseCases.QueryBase;

namespace ProductMgmtSlices.UseCases.ProductUseCases
{
    public record SearchProductsQuery(
        string ProductName,
        decimal? MinPrice,
        decimal? MaxPrice,
        int PageNumber,
        int PageSize) : IRequest<Result<PaginatedList<ProductDto>>>;

    public record ProductDto(
        long id,
        string name,
        string description,
        decimal price,
        int stock_quantity);

    public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, Result<PaginatedList<ProductDto>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SearchProductsQueryHandler(IProductRepository productRepository, IMapper mapper, ILogger logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<PaginatedList<ProductDto>>> Handle(SearchProductsQuery request,
                                                                    CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Start handling {nameof(SearchProductsQuery)}");

                var products = await _productRepository.SearchAsync(request.ProductName,
                                                                    request.MinPrice,
                                                                    request.MaxPrice,
                                                                    request.PageNumber,
                                                                    request.PageSize);


                if (products == null)
                    return Result<PaginatedList<ProductDto>>.Failure("No Result");

                var productDtos = ProductTable.ToProductDTO(products);
                return Result<PaginatedList<ProductDto>>.Success(new PaginatedList<ProductDto>(productDtos,
                                                                                               request.PageNumber,
                                                                                               request.PageSize));



            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(SearchProductsQuery)}", ex);
                return Result<PaginatedList<ProductDto>>.Failure("An error occurred during the search");
            }
        }


    }





}

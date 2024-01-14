namespace ProductMgmtSlices.UseCases
{
    public record AddProductCommand(
      string Name,
      decimal Price,
      int Quantity,
      string Description) : IRequest<Result<long>>;

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Result<long>>
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductTableMappers _productTableMappers;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AddProductCommandHandler(IGenericRepository genericRepository,
                                        IProductRepository productRepository,
                                        IProductTableMappers productTableMappers,
                                        IMapper mapper,
                                        ILogger logger)
        {
            _genericRepository = genericRepository;
            _productRepository = productRepository;
            _productTableMappers = productTableMappers;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<long>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start handling {nameof(AddProductCommand)}");

            try
            {
                var product = _mapper.Map<AddProductCommand, Product>(request);
                var (isValid, errorMessage) = Product.Validate(product);

                if (!isValid)
                    return Result<long>.Failure(errorMessage);

                if (await _productRepository.GetCountByProductNameAsync(request.Name) > 0)
                    return Result<long>.Failure($"Product with Name {request.Name} already exists.");

                var productData = _productTableMappers.MapToTableForInsert(product);

                long productId = await _genericRepository.InsertOneGetIdPgAsync<ProductTable>(productData);
                _logger.LogInformation($"Product added with ID: {productId}");

                return Result<long>.Success(productId);
            }
            catch (DbException ex)
            {
                _logger.LogError($"Database error in {nameof(AddProductCommand)}", ex);
                return Result<long>.Failure("Database error");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddProductCommand)}", ex);
                return Result<long>.Failure("An error occurred");
            }
        }

    }

}

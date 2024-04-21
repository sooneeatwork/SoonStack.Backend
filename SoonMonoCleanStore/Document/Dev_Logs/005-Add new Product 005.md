using Core.Domain.DomainModel.ProductModel;
using System.Data.Common;

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
            Result<long> result;
            _logger.LogInformation($"Start to handler {nameof(AddProductCommand)}");

            try
            {
                var productCount = await _productRepository.GetCountByProductNameAsync(request.Name);

                if (Product.IsProductExists(productCount))
                    return Result<long>.Failure($"Product with Name {request.Name} exist.");

                var mappedProduct = _mapper.Map<AddProductCommand,Product>(request);
                //var newProduct =  Product.CreateProduct(mappedProduct);
                var productData = _productTableMappers.MapToTableForInsert(mappedProduct);

                long productId = await _genericRepository.InsertOneGetIdAsync<ProductTable>(productData);
                _logger.LogInformation($"Product added with ID: {productId}");
                result =  Result<long>.Success(productId);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(nameof(AddProductCommand), ex);
                result = Result<long>.Failure($"request is null: {ex.Message}");
            }
            catch (DbException ex)
            {
                _logger.LogError(nameof(AddProductCommand), ex);
                result = Result<long>.Failure($"Database error : {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(AddProductCommand), ex);
                result =  Result<long>.Failure($"An error occurred: {ex.Message}");
            }

            return result;
        }

    }

}

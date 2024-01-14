---

## Log 005: Development Use Case: AddProduct.cs

---

After i complete the Domain model development,i proceed to create AddProduct for my use case layer. I apply the concept of screaming architecture where i will name my use case class same like the feature.

This will help new developer to navigate the source code easily because the name of the class is same as feature.

In my use case layer Add product class, i follow 1 structure, the data transfer object that i will receive from api and the command handler. 1 use case feature will be develop 1 class.

The purpose of use case layer is to act as coordinator, in our case, if i want to implement a add product function, i will need business logic from domain layer and also database lofic from repository layer.

Use case layer should not contain any business logic, all business logic is belong to domain layer

```csharp
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
                var newProduct =  Product.CreateProduct(mappedProduct);
                var productData = _productTableMappers.MapToTableForInsert(newProduct);

                long productId = await _genericRepository.InsertOneGetIdPgAsync<ProductTable>(productData);
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

```
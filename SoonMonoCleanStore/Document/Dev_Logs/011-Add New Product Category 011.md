---

## Log 011: Add New Product Category Use case

---
This development work will be simpler as I already have sample code from my insert logic. Now, I am going to develop an add new product category feature, with a style similar to adding a new product.


**Use Case Code**
```csharp
namespace ProductMgmtSlices.UseCases.ProductCategoryUseCases
{
    public record AddProductCategoryCommand(
     string categoryName,
     decimal categoryDescription,
     int isActive) : IRequest<Result<long>>;

  
    public class AddProductCategoryCommandHandler : IRequestHandler<AddProductCategoryCommand, Result<long>>
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductCategoryTableMappers _productCategoryTableMappers;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AddProductCategoryCommandHandler(IGenericRepository genericRepository,
                                                IProductCategoryRepository productCategoryRepository,
                                                IProductCategoryTableMappers productCategoryTableMappers,
                                                IMapper mapper,
                                                ILogger logger)
        {
            _genericRepository = genericRepository;
            _productCategoryRepository = productCategoryRepository;
            _productCategoryTableMappers = productCategoryTableMappers;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<long>> Handle(AddProductCategoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start handling {nameof(AddProductCategoryCommand)}");

            try
            {
                var productCategory = _mapper.Map<AddProductCategoryCommand, ProductCategory>(request);
                var (isValid, errorMessage) = ProductCategory.Validate(productCategory);

                if (!isValid)
                    return Result<long>.Failure(errorMessage);

                if (await _genericRepository.GetCountByFieldsAsync<ProductCategoryTable>(request.categoryName) > 0)
                    return Result<long>.Failure($"Product with Name {request.categoryName} already exists.");

                var productCategoryData = _productCategoryTableMappers.CreateMapForInsert(productCategory);

                long insertedId = await _genericRepository.InsertOneGetIdPgAsync<ProductCategory>(productCategoryData);
                _logger.LogInformation($"Product Category added with ID: {insertedId}");

                return Result<long>.Success(insertedId);
            }
            catch (DbException ex)
            {
                _logger.LogError($"Database error in {nameof(AddProductCategoryCommand)}", ex);
                return Result<long>.Failure("Database error");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddProductCategoryCommand)}", ex);
                return Result<long>.Failure("An error occurred");
            }
        }

    }
}
```

using Core.Domain.DomainModel.ProductModel;
using Core.Domain.RepoInterface;
using Core.UseCases.Wrapper;
using ProductSlices.Repository.ProductCategoryMapper;
using ProductSlices.Repository.Repository.ProductCategoryRepo;

namespace ProductSlices.UseCases.ProductCategoryFeatures
{
    public record AddProductCategoryCommand(string CategoryName, string CategoryDescription) : IRequest<Result<long>>;

    public class AddProductCategoryCommandHandler : IRequestHandler<AddProductCategoryCommand,Result<long>>
    {
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly IGenericRepository _genericRepository;
        private readonly IProductCategoryMappers _productCategoryMappers;
        private readonly ILogger _logger;

        public AddProductCategoryCommandHandler(IProductCategoryRepository categoryRepository,
                                                IGenericRepository genericRepository,
                                                IProductCategoryMappers productCategoryMappers,
                                                ILogger logger)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
            _productCategoryMappers = productCategoryMappers ?? throw new ArgumentNullException(nameof(productCategoryMappers));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<long>> Handle(AddProductCategoryCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            Result<long> result;

            try
            {
                var categoryCount = await _categoryRepository.GetCountByCategoryNameAsync(request.CategoryName);
                if (categoryCount > 0)
                    return Result<long>.Failure($"Category with Name '{request.CategoryName}' already exists.");

                var (validationError,productCategory) = ProductCategory.CreateProductCategory(request.CategoryName,
                                                                                             request.CategoryDescription);

                if (validationError != null && validationError.Count > 0)
                {
                    string errorMessage = string.Join(Environment.NewLine, validationError);
                    return Result<long>.Failure($"Invalid product category info. Errors: {errorMessage}");
                }


                var productCatTableData = _productCategoryMappers.CreateMapForInsert(productCategory);
                int categoryId = await _genericRepository.InsertOneAsync<ProductCategory>(productCatTableData);

                result = categoryId > 0
                    ? Result<long>.Success(categoryId)
                    : Result<long>.Failure("Failed to add the product category.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding a new product category: {ex.Message}", ex);
                result = Result<long>.Failure($"An error occurred: {ex.Message}");
            }

            return result;
        }
    }
}

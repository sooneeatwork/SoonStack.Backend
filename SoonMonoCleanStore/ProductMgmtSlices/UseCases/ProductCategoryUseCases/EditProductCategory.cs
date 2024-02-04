using ProductSlices.Repository.ProductCategoryMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgmtSlices.UseCases.ProductCategoryUseCases
{

    public record EditProductCategoryCommand(
        int productCategoryId,
        string categoryName,
        decimal categoryDescription,
        int isActive) : IRequest<Result<int>>;



    public class EditProductCategoryHandler : IRequestHandler<EditProductCategoryCommand, Result<int>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryTableMappers _productCategoryTableMapper;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EditProductCategoryHandler(IProductRepository productRepository,
                                          IMapper mapper,
                                          IProductCategoryTableMappers productCategoryTableMapper,
                                          ILogger logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productCategoryTableMapper = productCategoryTableMapper;
            _logger = logger;
        }

        public async Task<Result<int>> Handle(EditProductCategoryCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.productCategoryId);
            Result<int> result;

            try
            {
                var productCategoryData = await _productRepository.GetByIdAsync<ProductCategoryTable>(request.productCategoryId);

                if (productCategoryData == null)
                    return Result<int>.Failure($"Product Category with ID {request.productCategoryId} not found.");

                var productCategory = _productCategoryTableMapper.MapToDomain(productCategoryData);
                productCategory.UpdateProductCategory(request.categoryDescription,
                                                      request.categoryName,
                                                      request.isActive);


                var (dataFields, whereClause) = _productCategoryTableMapper.CreateMapForUpdate(productCategory, productCategoryData);
                int rowsAffected = await _productRepository.UpdateOneAsync<ProductCategoryTable>(dataFields, whereClause);
                result = Result<int>.Success(rowsAffected);

            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(EditProductCategoryCommand), ex);
                result = Result<int>.Failure("Failed to edit product category");
            }

            return result;
        }
    }
}

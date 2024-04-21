using Core.Domain.DomainModel.ProductModel;
using Core.UseCases.Wrapper;
using Microsoft.Extensions.Logging;
using ProductMgmtSlices.Repository.CategoryIHierarchyMappers;
using ProductMgmtSlices.Repository.Repository.ProductCategoryRepo;

namespace ProductMgmtSlices.UseCases.ProductCategoryUseCases.Commands
{
    public record AddChildCategoryCommand(long ParentCategoryId, long ChildCategoryId, int Depth) : IRequest<Result<long>>;

    public class AddChildCategoryCommandHandler : IRequestHandler<AddChildCategoryCommand, Result<long>>
    {
        private readonly ICategoryHierarchyRepository _hierarchyRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ILogger<AddChildCategoryCommandHandler> _logger;
        private readonly IHierarchyTableMappers _hierarchyTableMappers;

        public AddChildCategoryCommandHandler(ILogger<AddChildCategoryCommandHandler> logger,
                                              IHierarchyTableMappers hierarchyTableMappers,
                                              ICategoryHierarchyRepository hierarchyRepository,
                                              IProductCategoryRepository productCategoryRepository)
        {

            _logger = logger;
            _hierarchyTableMappers = hierarchyTableMappers;
            _productCategoryRepository = productCategoryRepository;
            _hierarchyRepository = hierarchyRepository;
        }

        public async Task<Result<long>> Handle(AddChildCategoryCommand request, CancellationToken cancellationToken)
        {
            Result<long> result;
            try
            {
                var (parentCategory, childCategory) = await _productCategoryRepository
                                                            .GetParentAndChildCategoriesAsync(request.ParentCategoryId,
                                                                                              request.ChildCategoryId);

                if (parentCategory == null || childCategory == null)
                {
                    _logger.LogWarning("Parent or Child category does not exist.");
                    return Result<long>.Failure("Parent or Child category does not exist.");
                }

                // Use the factory method to create a new CategoryHierarchy instance
                var hierarchy = CategoryHierarchy.Create(parentCategory, childCategory, request.Depth);
                var hierarchyData = _hierarchyTableMappers.CreateMapForInsert(hierarchy);
                var hierarchyId = await _productCategoryRepository.AddCategoryHierarchyAsync(hierarchyData);

                _logger.LogInformation($"Successfully added child category {childCategory.Id} to parent {parentCategory.Id}.");
                result = Result<long>.Success(hierarchyId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding child category.");
                result = Result<long>.Failure("Error occurred while adding child category.");
            }
            return result;
        }

    }
}

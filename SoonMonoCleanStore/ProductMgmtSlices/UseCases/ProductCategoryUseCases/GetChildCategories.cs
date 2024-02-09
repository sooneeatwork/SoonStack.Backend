using MediatR;
using ProductMgmtSlices.Repository.Repository.ProductCategoryRepo;

namespace ProductMgmtSlices.UseCases.ProductCategoryUseCases.Queries
{
    public record GetChildCategoriesQuery(long ParentCategoryId) : IRequest<Result<IEnumerable<CategoryDto>>>;

    public record CategoryDto
    {
        public long CategoryId { get; init; }
        public string CategoryName { get; init; } = string.Empty;
    }

    public class GetChildCategoriesQueryHandler : IRequestHandler<GetChildCategoriesQuery, Result<IEnumerable<CategoryDto>>>
    {
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly ILogger _logger;

        public GetChildCategoriesQueryHandler(IProductCategoryRepository categoryRepository, ILogger logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<CategoryDto>>> Handle(GetChildCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var childCategories = await _categoryRepository.GetChildCategoriesAsync(request.ParentCategoryId);
                var categoryDtos = childCategories.Select(category => new CategoryDto
                {
                    CategoryId = category.Id,
                    CategoryName = category.CategoryName
                    // Map other needed properties
                }).ToList();

                return Result<IEnumerable<CategoryDto>>.Success(categoryDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred retrieving child categories for parent ID {request.ParentCategoryId}", ex);
                return Result<IEnumerable<CategoryDto>>.Failure("Failed to retrieve child categories.");
            }
        }
    }
}

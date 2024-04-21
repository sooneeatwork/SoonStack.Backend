using MediatR;
using Microsoft.Extensions.Logging;
using ProductMgmtSlices.Domain;
using ProductMgmtSlices.Repository.Repository.ProductCategoryRepo;
using SharedKernel.Domain.DomainModel.ProductModel;
using SharedKernel.UseCases.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProductMgmtSlices.UseCases.ProductCategoryUseCases.Queries
{
    public record GetFullCategoryHierarchyQuery() : IRequest<Result<IEnumerable<CategoryHierarchyDto>>>;

    public record CategoryHierarchyDto
    {
        public long CategoryId { get; init; }
        public string CategoryName { get; init; } = string.Empty;
        public long? ParentCategoryId { get; init; }
        public List<CategoryHierarchyDto> SubCategories { get; init; } = new();
    }

    public class GetFullCategoryHierarchyQueryHandler : IRequestHandler<GetFullCategoryHierarchyQuery, Result<IEnumerable<CategoryHierarchyDto>>>
    {
        private readonly IProductCategoryRepository _categoryRepository; // Adjusted to use a domain-focused repository
        private readonly ILogger<GetFullCategoryHierarchyQueryHandler> _logger;

        public GetFullCategoryHierarchyQueryHandler(IProductCategoryRepository categoryRepository, ILogger<GetFullCategoryHierarchyQueryHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<CategoryHierarchyDto>>> Handle(GetFullCategoryHierarchyQuery request, CancellationToken cancellationToken)
        {
            Result<IEnumerable<CategoryHierarchyDto>> result;

            try
            {
                var categories = await _categoryRepository.GetAllWithHierarchyAsync();
                var rootCategories = categories.Where(c => c.ParentHierarchies == null); // Assuming a direct ParentCategoryId property
                var hierarchyDtos = rootCategories.Select(category => new CategoryHierarchyDto
                {
                    CategoryId = category.Id,
                    CategoryName = category.CategoryName,
                    ParentCategoryId = category.ParentHierarchies?.FirstOrDefault()?.ParentCategoryId, // Assuming direct property for parent ID
                    SubCategories = category.SubCategories.Select(BuildHierarchyDto).ToList() // Recursively build subcategories
                }).ToList();

                result = Result<IEnumerable<CategoryHierarchyDto>>.Success(hierarchyDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve category hierarchy.");
                result = Result<IEnumerable<CategoryHierarchyDto>>.Failure("Failed to retrieve category hierarchy.");
            }

            return result;
        }

        private CategoryHierarchyDto BuildHierarchyDto(ProductCategory category)
        {
            return new CategoryHierarchyDto
            {
                CategoryId = category.Id,
                CategoryName = category.CategoryName,
                ParentCategoryId = category.ParentHierarchies?.FirstOrDefault()?.ParentCategoryId, // Corrected to use a direct property
                SubCategories = category.SubCategories.Select(BuildHierarchyDto).ToList() // Recursively build subcategories
            };
        }
    }
}

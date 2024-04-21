using Core.UseCases.Wrapper;
using DapperPersistance;
using ProductMgmtSlices.Repository.CategoryIHierarchyMappers;

namespace ProductMgmtSlices.UseCases.ProductCategoryUseCases.Commands
{
    public record MoveCategoryCommand(long CategoryId, long NewParentId) : IRequest<Result<bool>>;

    public class MoveCategoryCommandHandler : IRequestHandler<MoveCategoryCommand, Result<bool>>
    {
        private readonly ICategoryHierarchyRepository _hierarchyRepository;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHierarchyTableMappers _hierarchyTableMappers;

        public MoveCategoryCommandHandler(ICategoryHierarchyRepository hierarchyRepository,
                                          ILogger logger,
                                          IUnitOfWork unitOfWork,
                                          IHierarchyTableMappers hierarchyTableMappers)
        {
            _hierarchyRepository = hierarchyRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hierarchyTableMappers = hierarchyTableMappers;
        }

        public async Task<Result<bool>> Handle(MoveCategoryCommand request, CancellationToken cancellationToken)
        {
            Result<bool> result;
            try
            {
                // Verify both categories exist and fetch necessary data
                var (parentExists, categoryToMoveExists) = await _hierarchyRepository
                                                                 .CheckCategoryExistsAsync(request.NewParentId,
                                                                                           request.CategoryId);

                if (!parentExists || !categoryToMoveExists)
                {
                    _logger.LogWarning("Either the parent or the child category does not exist.");
                    return Result<bool>.Failure("Invalid categories specified.");
                }

                // Perform the move operation including depth recalculation for the moved category and its subtree
                bool isSuccess = await _hierarchyRepository.MoveCategoryAndUpdateHierarchyAsync(request.CategoryId,
                                                                                                request.NewParentId);
                result = isSuccess ? Result<bool>.Success(isSuccess) :
                                     Result<bool>.Failure("Failed");

                _logger.LogInformation($"Successfully moved category {request.CategoryId} to new parent {request.NewParentId}.");
            }
            catch (Exception ex)
            {
               
                _logger.LogError($"Error occurred while moving category {request.CategoryId} to new parent {request.NewParentId}.", ex);
                result = Result<bool>.Failure("Error occurred while moving the category.");
            }

            return result;
        }
    }
}

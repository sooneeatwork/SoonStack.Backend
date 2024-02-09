namespace ProductMgmtSlices.UseCases.ProductCategoryUseCases.Commands
{
    public record RemoveChildCategoryCommand(long ChildCategoryId, long? ParentCategoryId = null) : IRequest<Result<bool>>;

    public class RemoveChildCategoryCommandHandler : IRequestHandler<RemoveChildCategoryCommand, Result<bool>>
    {
        private readonly ICategoryHierarchyRepository _hierarchyRepository;
        private readonly ILogger _logger;

        public RemoveChildCategoryCommandHandler(ICategoryHierarchyRepository hierarchyRepository, 
                                                 ILogger logger)
        {
            _hierarchyRepository = hierarchyRepository;
            _logger = logger;
        }

        public async Task<Result<bool>> Handle(RemoveChildCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool removedSuccessfully = await _hierarchyRepository.RemoveChildFromParentAsync(request.ChildCategoryId, 
                                                                                                 request.ParentCategoryId);

                string logMessage = removedSuccessfully
                                    ? $"Successfully removed child category {request.ChildCategoryId} from its parent."
                                    : $"Failed to remove child category {request.ChildCategoryId} from its parent.";

                _logger.LogInformation(logMessage);
                return removedSuccessfully ? Result<bool>.Success(true) : 
                                             Result<bool>.Failure(logMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while removing child category.",ex);
                return Result<bool>.Failure("Error occurred while removing child category.");
            }
        }

    }
}

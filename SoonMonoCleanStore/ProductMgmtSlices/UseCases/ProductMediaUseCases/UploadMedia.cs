using Infrastructure.MediaStorage;
using ProductMgmtSlices.Repository.ProductMediaMapper;

namespace ProductMgmtSlices.UseCases.ProductMediaUseCases
{
    public record UploadMediaCommand(long ProductId, string FileName, Stream FileContent, string ContentType) : IRequest<Result<string>>;

    public class UploadMediaCommandHandler : IRequestHandler<UploadMediaCommand, Result<string>>
    {
        private readonly IMediaStorageServices _storageService;
        private readonly IProductMediaRepository _productMediaRepository;
        private readonly IProductMediaTableMappers _productMediaTableMappers;
        private readonly ILogger _logger;

        public UploadMediaCommandHandler(IMediaStorageServices storageService, 
                                         IProductMediaRepository productMediaRepository,
                                         IProductMediaTableMappers productMediaTableMappers,
                                         ILogger logger)
        {
            _storageService = storageService;
            _productMediaRepository = productMediaRepository;
            _productMediaTableMappers = productMediaTableMappers;
            _logger = logger;
        }

        public async Task<Result<string>> Handle(UploadMediaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mediaUrl = await _storageService.UploadFileAsync("your-bucket-name", 
                                                                       request.FileName, 
                                                                       request.FileContent, 
                                                                       request.ContentType);

                if (string.IsNullOrWhiteSpace(mediaUrl))
                    return Result<string>.Failure("Failed to upload media.");

                var productMedia = ProductMedia.CreateProductMedia(request.ProductId,
                                                                   mediaUrl,
                                                                   request.ContentType);

                var productMediaData = _productMediaTableMappers.CreateMapForInsert(productMedia);
                await _productMediaRepository.InsertOneGetIdAsync<ProductMedia>(productMediaData);
               

                return Result<string>.Success(mediaUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to upload media.", ex);
                return Result<string>.Failure("Failed to upload media.");
            }
        }
    }

}

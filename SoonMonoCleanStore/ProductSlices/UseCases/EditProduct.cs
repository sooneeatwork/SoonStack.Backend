namespace ProductSlices.UseCases
{

    public record EditProductCommand(
        int ProductId,
        string NewName,
        decimal NewPrice,
        string NewDescription,
        int NewStockQuantity) : IRequest<Result<int>>;



    public class EditCustomerProfileHandler : IRequestHandler<EditProductCommand, Result<int>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTableMappers _productTableMapper;
        private readonly IMapper _mapper;

        public EditCustomerProfileHandler(IProductRepository productRepository,
                                          IMapper mapper,
                                          IProductTableMappers productTableMappers)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productTableMapper = productTableMappers;
        }

        public async Task<Result<int>> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.ProductId);

            var productData = await _productRepository.GetByIdAsync<ProductTable>(request.ProductId);

            if (productData == null)
                return Result<int>.Failure($"Product with ID {request.ProductId} not found.");

            var product = _mapper.Map<ProductTable, Product>(productData);
            
            product.UpdateProductInfo(request.NewName,
                                        request.NewDescription,
                                        request.NewPrice,
                                        request.NewStockQuantity);

          
            var (dataFields, whereClause) = _productTableMapper.CreateMapForUpdate(product, productData);
            int rowsAffected = await _productRepository.UpdateOneAsync<ProductTable>(dataFields, whereClause);

            return Result<int>.Success(rowsAffected);
        }
    }
}

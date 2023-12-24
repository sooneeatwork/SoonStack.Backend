using ProductSlices.Repository.DatabaseModel;
using ProductSlices.Repository.ProductTableMapper;
using ProductSlices.Repository.Repository;
using SharedKernel.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // Update customer profile information based on the request
            product.UpdateProductInfo(request.NewName,
                                        request.NewDescription,
                                        request.NewPrice,
                                        request.NewStockQuantity);

            // Save the updated customer information
            var (dataFields, whereClause) = _productTableMapper.CreateMapForUpdate(product, productData);
            int rowsAffected = await _productRepository.UpdateOneAsync<ProductTable>(dataFields, whereClause);

            return Result<int>.Success(rowsAffected);
        }
    }
}

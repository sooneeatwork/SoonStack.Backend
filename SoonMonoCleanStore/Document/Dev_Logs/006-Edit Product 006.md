------------------------------------------------------------------------------------------------

## Log 006: Development Use Case: EditProduct.cs

------------------------------------------------------------------------------------------------

Feature Overview:

The latest addition to our product management module is the EditProduct function. This feature is pivotal in empowering users to update existing product details, reflecting changes in product information as needed.


Development Insights:

The EditProduct class is named for clarity and ease of discovery - a straightforward approach in line with our ongoing commitment to a user-friendly codebase. This naming strategy makes it easy for anyone in the team, whether a seasoned developer or a newbie, to quickly identify and work with the relevant parts of the system.


```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgmtSlices.UseCases
{
    public record EditProductCommand(
       int ProductId,
       string NewName,
       decimal NewPrice,
       string NewDescription,
       int NewStockQuantity) : IRequest<Result<int>>;



    public class EditProductHandler : IRequestHandler<EditProductCommand, Result<int>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTableMappers _productTableMapper;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EditProductHandler(IProductRepository productRepository,
                                    IMapper mapper,
                                    IProductTableMappers productTableMappers,
                                    ILogger logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productTableMapper = productTableMappers;
            _logger = logger;
        }

        public async Task<Result<int>> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.ProductId);
            Result<int> result;

            try
            {
                var productData = await _productRepository.GetByIdAsync<ProductTable>(request.ProductId);

                if (productData == null)
                    return Result<int>.Failure($"Product with ID {request.ProductId} not found.");

                var product = _productTableMapper.MapToDomain(productData);
                product.UpdateProductInfo(request.NewName,
                                            request.NewDescription,
                                            request.NewPrice,
                                            request.NewStockQuantity);


                var (dataFields, whereClause) = _productTableMapper.CreateMapForUpdate(product, productData);
                int rowsAffected = await _productRepository.UpdateOneAsync<ProductTable>(dataFields, whereClause);
                result = Result<int>.Success(rowsAffected);

            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(EditProductCommand), ex);
                result = Result<int>.Failure("Failed to edit product info");
            }

            return result;
        }
    }
}


```

------------------------------------------------------------------------------------------------

## Log 006: Development Use Case: EditProduct.cs

------------------------------------------------------------------------------------------------

Feature Overview:

The latest addition to our product management module is the EditProduct function. This feature is pivotal in empowering users to update existing product details, reflecting changes in product information as needed.


Development Insights:

The EditProduct class is named for clarity and ease of discovery - a straightforward approach in line with our ongoing commitment to a user-friendly codebase. This naming strategy makes it easy for anyone in the team, whether a seasoned developer or a newbie, to quickly identify and work with the relevant parts of the system.

Lets break the code into different part

Command Definition using Record:


```csharp

public record EditProductCommand(
    int ProductId,
    string NewName,
    decimal NewPrice,
    string NewDescription,
    int NewStockQuantity) : IRequest<Result<int>>;

```
Purpose: Defines the structure of the edit product command.

Why Record: The record keyword in C# is used for immutable reference types. By using record here, we ensure that once an EditProductCommand is created, it cannot be altered. This immutability is beneficial for commands in CQRS patterns, as it ensures the command won't be modified after its creation, preserving the original intent.
Components: Includes the product ID to identify the product and the new values for updating the product's details.


Command Definition using Record:


```csharp

public record EditProductCommand(
    int ProductId,
    string NewName,
    decimal NewPrice,
    string NewDescription,
    int NewStockQuantity) : IRequest<Result<int>>;

```
Purpose: Defines the structure of the edit product command.

Why Record: The record keyword in C# is used for immutable reference types. By using record here, we ensure that once an EditProductCommand is created, it cannot be altered. This immutability is beneficial for commands in CQRS patterns, as it ensures the command won't be modified after its creation, preserving the original intent.
Components: Includes the product ID to identify the product and the new values for updating the product's details.

===============================
***Handler Class Definition:***
================================
```csharp

 public class EditProductHandler : IRequestHandler<EditProductCommand, Result<int>>

```
Purpose: 
Handles the logic to process the EditProductCommand.
Pattern: 
Implements the Handler pattern, part of the Mediator pattern, where each use case has its own handler in a decoupled manner, improving code maintainability and scalability.


=============================================
***Constructor with Dependency Injection:***
==============================================
```csharp
public EditProductHandler(IProductRepository productRepository, IMapper mapper, IProductTableMappers productTableMappers, ILogger logger)
{
    // Assigning dependencies to private fields...
}
```
Purpose: 
Initializes the handler with necessary dependencies.

Explanation: 
The constructor uses dependency injection for repository access, data mapping, and logging. This aligns with the principles of clean architecture, promoting testability and loose coupling.

==============================================
***Handle Method Implementation:***
==============================================
```csharp

public async Task<Result<int>> Handle(EditProductCommand request, CancellationToken cancellationToken)
{
    // Detailed implementation...
}
```
Purpose: Executes the update operation for a product.
Process: Includes validation, data retrieval, and mapping to domain models. The method updates product information and saves changes through the repository.
Concurrency: The use of async and Task indicates that the method is asynchronous, enhancing the performance by not blocking the calling thread, especially important for I/O-bound operations.



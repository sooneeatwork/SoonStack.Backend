---

## Log 005: Development Use Case: AddProduct.cs

---

After i complete the Domain model development,i proceed to create AddProduct for my use case layer. I apply the concept of screaming architecture where i will name my use case class same like the feature.

This will help new developer to navigate the source code easily because the name of the class is same as feature.

In my use case layer Add product class, i follow 1 structure, the data transfer object that i will receive from api and the command handler. 1 use case feature will be develop 1 class.

The purpose of use case layer is to act as coordinator, in our case, if i want to implement a add product function, i will need business logic from domain layer and also database lofic from repository layer.

Use case layer should not contain any business logic, all business logic is belong to domain layer

```csharp
public record AddProductCommand(
      string Name,
      decimal Price,
      int Quantity,
      string Description) : IRequest<Result<long>>;

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Result<long>>
    {
       //private variables
        public AddProductCommandHandler(){}
       
        public async Task<Result<long>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            Result<long> result;
            _logger.LogInformation($"Start to handler {nameof(AddProductCommand)}");
            try
            {
               //use case logic, simplified this code to cater for discord message limit
                result =  Result<long>.Success(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(AddProductCommand), ex);
                result =  Result<long>.Failure($"An error occurred: {ex.Message}");
            }

            return result;
        }
```
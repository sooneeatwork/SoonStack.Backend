namespace CleanStoreTest.ProductMgmTest
{
    public class AddProductCommandTests
    {
        private IGenericRepository genericRepository = Substitute.For<IGenericRepository>();
        private IProductRepository productRepository = Substitute.For<IProductRepository>();
        private IProductTableMappers productTableMappers = Substitute.For<IProductTableMappers>();
        private IMapper mapper = Substitute.For<IMapper>();
        private ILogger logger = Substitute.For<ILogger>();

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldAddProduct()
        {
            // Arrange
            productRepository.GetCountByProductNameAsync(Arg.Any<string>()).Returns(Task.FromResult(0));

            var productObject = new Product
            {
                Name = "Test Product",
                Price = 100,
                StockQuantity = 10,
                Description = "Description"
            };

            var product = Product.CreateProduct(productObject);
            mapper.Map<AddProductCommand, Product>(Arg.Any<AddProductCommand>()).Returns(product);
             var productTableData = productTableMappers.MapToTableForInsert(productObject);

            genericRepository.InsertOneGetIdAsync<ProductTable>(productTableData).Returns(Task.FromResult(1L));

            var handler = new AddProductCommandHandler(genericRepository, productRepository, productTableMappers, mapper, logger);
            var command = new AddProductCommand("Test Product", 100, 10, "Description");

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.Equal(1L, result.Data);
            await productRepository.Received(1).GetCountByProductNameAsync(Arg.Any<string>());
            await genericRepository.Received(1).InsertOneGetIdAsync<ProductTable>(productTableData);
        }


        [Fact]
        public async Task AddProductCommand_ShouldReturnFailure_WhenProductNameIsInvalid()
        {
            // Arrange


            var handler = new AddProductCommandHandler(genericRepository, productRepository, productTableMappers, mapper, logger);
            var command = new AddProductCommand("", 100.00m, 10, "Description"); // Empty product name

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.False(result.IsSuccess);
            // Optionally check the specific error message or error type if your implementation supports it
        }

        [Fact]
        public async Task AddProductCommand_ShouldReturnFailure_WhenPriceIsNegative()
        {
            // Arrange


            var handler = new AddProductCommandHandler(genericRepository, productRepository, productTableMappers, mapper, logger);
            var command = new AddProductCommand("Sample Product", -50.00m, 10, "Description"); // Negative price

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task AddProductCommand_ShouldReturnFailure_WhenStockQuantityIsNegative()
        {
            // Arrange


            var handler = new AddProductCommandHandler(genericRepository, productRepository, productTableMappers, mapper, logger); ;
            var command = new AddProductCommand("Sample Product", 100.00m, -5, "Description"); // Negative stock quantity

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task AddProductCommand_ShouldReturnFailure_WhenStockQuantityIsZero()
        {
            // Arrange


            var handler = new AddProductCommandHandler(genericRepository, productRepository, productTableMappers, mapper, logger);
            var command = new AddProductCommand("Sample Product", 100.00m, 0, "Description"); // zero stock quantity

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.False(result.IsSuccess);
        }


    }
}

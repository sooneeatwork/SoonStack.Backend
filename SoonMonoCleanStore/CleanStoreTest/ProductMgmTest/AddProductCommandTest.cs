using Infrastructure.Logging;
using NSubstitute;
using ProductMgmtSlices.Domain;
using ProductMgmtSlices.Domain.RepoInterface;
using ProductMgmtSlices.Repository.DatabaseModel;
using ProductMgmtSlices.Repository.ProductTableMapper;
using ProductMgmtSlices.UseCases;
using SharedKernel.Domain.RepoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanStoreTest.ProductMgmTest
{
    public class AddProductCommandTests
    {

        [Fact]
        public async Task AddProductCommandHandler_ShouldExecuteOnce()
        {
            // Arrange
            var mockGenericRepository = Substitute.For<IGenericRepository>();
            var mockProductRepository = Substitute.For<IProductRepository>();
            var mockProductTableMappers = Substitute.For<IProductTableMappers>();
            var mockLogger = Substitute.For<ILogger>();

            
            var handler = new AddProductCommandHandler(mockGenericRepository, mockProductRepository, mockProductTableMappers, mockLogger);
            var command = new AddProductCommand("Sample Product", 100.00m, 10, "Description");

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(result.IsSuccess);
        }



        [Fact]
        public async Task AddProductCommand_ShouldReturnSuccessAndProductId_WhenValid()
        {
            // Arrange
            var mockGenericRepository = Substitute.For<IGenericRepository>();
            var mockProductRepository = Substitute.For<IProductRepository>();
            var mockProductTableMappers = Substitute.For<IProductTableMappers>();
            var mockLogger = Substitute.For<ILogger>();

            // Assuming the InsertOneAsync method returns the product ID
            int expectedProductId = 1;
            mockProductRepository.InsertOneAsync<ProductTable>(Arg.Any<Dictionary<string, object>>())
                                 .Returns(expectedProductId);

            var handler = new AddProductCommandHandler(mockGenericRepository, mockProductRepository, mockProductTableMappers, mockLogger);
            var command = new AddProductCommand("Sample Product", 100.00m, 10, "Description");

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedProductId, result.Data);
            await mockProductRepository.Received(1).InsertOneAsync<ProductTable>(Arg.Any<Dictionary<string, object>>());
        }

        //[Fact]
        //public async Task AddProductCommand_ShouldReturnFailure_WhenProductNameIsInvalid()
        //{
        //    // Arrange
        //    var mockGenericRepository = Substitute.For<IGenericRepository>();
        //    var mockProductRepository = Substitute.For<IProductRepository>();
        //    var mockProductTableMappers = Substitute.For<IProductTableMappers>();
        //    var mockLogger = Substitute.For<ILogger>();

        //    var handler = new AddProductCommandHandler(mockGenericRepository, mockProductRepository, mockProductTableMappers, mockLogger);
        //    var command = new AddProductCommand("", 100.00m, 10, "Description"); // Empty product name

        //    // Act
        //    var result = await handler.Handle(command, new CancellationToken());

        //    // Assert
        //    Assert.False(result.IsSuccess);
        //    // Optionally check the specific error message or error type if your implementation supports it
        //}

        //[Fact]
        //public async Task AddProductCommand_ShouldReturnFailure_WhenPriceIsNegative()
        //{
        //    // Arrange
        //    var mockGenericRepository = Substitute.For<IGenericRepository>();
        //    var mockProductRepository = Substitute.For<IProductRepository>();
        //    var mockProductTableMappers = Substitute.For<IProductTableMappers>();
        //    var mockLogger = Substitute.For<ILogger>();

        //    var handler = new AddProductCommandHandler(mockGenericRepository, mockProductRepository, mockProductTableMappers, mockLogger);
        //    var command = new AddProductCommand("Sample Product", -50.00m, 10, "Description"); // Negative price

        //    // Act
        //    var result = await handler.Handle(command, new CancellationToken());

        //    // Assert
        //    Assert.False(result.IsSuccess);
        //}

        //[Fact]
        //public async Task AddProductCommand_ShouldReturnFailure_WhenStockQuantityIsNegative()
        //{
        //    // Arrange
        //    var mockGenericRepository = Substitute.For<IGenericRepository>();
        //    var mockProductRepository = Substitute.For<IProductRepository>();
        //    var mockProductTableMappers = Substitute.For<IProductTableMappers>();
        //    var mockLogger = Substitute.For<ILogger>();

        //    var handler = new AddProductCommandHandler(mockGenericRepository, mockProductRepository, mockProductTableMappers, mockLogger);
        //    var command = new AddProductCommand("Sample Product", 100.00m, -5, "Description"); // Negative stock quantity

        //    // Act
        //    var result = await handler.Handle(command, new CancellationToken());

        //    // Assert
        //    Assert.False(result.IsSuccess);
        //}

        //[Fact]
        //public async Task AddProductCommand_ShouldReturnFailure_WhenStockQuantityIsZero()
        //{
        //    // Arrange
        //    var mockGenericRepository = Substitute.For<IGenericRepository>();
        //    var mockProductRepository = Substitute.For<IProductRepository>();
        //    var mockProductTableMappers = Substitute.For<IProductTableMappers>();
        //    var mockLogger = Substitute.For<ILogger>();

        //    var handler = new AddProductCommandHandler(mockGenericRepository, mockProductRepository, mockProductTableMappers, mockLogger);
        //    var command = new AddProductCommand("Sample Product", 100.00m, 0, "Description"); // zero stock quantity

        //    // Act
        //    var result = await handler.Handle(command, new CancellationToken());

        //    // Assert
        //    Assert.False(result.IsSuccess);
        //}


    }
}

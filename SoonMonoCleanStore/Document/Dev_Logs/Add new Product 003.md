---

## Log 003: Test Driven Development (TDD)

---

I haven't delved deeply into executing Test Driven Development properly, so I'm taking this opportunity to explore its benefits. My approach is to adopt the practices based on what I currently understand, and later, I'll cross-check with some materials to see what improvements can be made.

Based on the pre-conditions, actions, and post-conditions, I've created an xUnit test project and a test class. Below is the code snippet:

```csharp
// Example test code snippet
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
            // more code in GitHub
        }
    }
}


I'm using NSubstitute as my mocking library. After completing the setup, I run the test, which, as expected, fails initially. I then proceed with development.

My key takeaway from Test Driven Development is that you don’t have to wait for everything (like the database, logging library) to be ready before you start testing. You can focus on the business logic first, which only requires an IDE and C# to write your logic. With the help of a test library, you can quickly ensure that your program is functioning as intended.
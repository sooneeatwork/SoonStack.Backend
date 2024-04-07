using OrderByAlgorithmApp;

namespace OrderByAlgorithmAppTests
{
    [TestFixture]
    public class OrderByAlgorithmTests
    {
        [Test]
        public void Order_WithNullInput_ShouldReturnFailure()
        {
            var algorithm = new OrderByAlgorithm();
            var result = algorithm.Order(null, 1);
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Error.Message, Is.EqualTo("Input cannot be null or empty."));
        }

        [Test]
        public void Order_WithEmptyInput_ShouldReturnFailure()
        {
            var algorithm = new OrderByAlgorithm();
            var result = algorithm.Order(Array.Empty<char>(), 1);
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Error.Message, Is.EqualTo("Input cannot be null or empty."));
        }

        [Test]
        public void Order_WithInvalidOrdering_ShouldReturnFailure()
        {
            var algorithm = new OrderByAlgorithm();
            var result = algorithm.Order(new char[] { 'a', 'b', 'c' }, 0);
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Error.Message, Is.EqualTo("Ordering must be greater than zero."));
        }

        [Test]
        public void Order_WithComplexInputAndHighOrdering_ShouldOrderCorrectly()
        {
            //
            var algorithm = new OrderByAlgorithm();
            char[] input = "GHA14SFSD6K92".ToCharArray();
            int ordering = 16;

            
            char[] expectedOutput = "AF9-SHG-4K2-61D-S".ToCharArray(); // You should define this method based on algorithm's logic or manually set the expected output

            var result = algorithm.Order(input, ordering);

            Assert.IsTrue(result.IsSuccess);
            Assert.That(result.Value, Is.EqualTo(expectedOutput));
        }
    }
}
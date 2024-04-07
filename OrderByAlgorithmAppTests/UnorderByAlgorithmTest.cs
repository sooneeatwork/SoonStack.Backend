using OrderByAlgorithmApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderByAlgorithmAppTests
{

    [TestFixture]
    public class UnorderByAlgorithmTests
    {
        [Test]
        public void Order_WithValidInput_ShouldUnorderCorrectly()
        {
            var algorithm = new UnorderByAlgorithm();
            char[] input = "e  rbml s nngeshsr etaet.loaldtryadaimt di ghtoeaeuse aC cuciy afskh ss t ovgo tna atstanmeempaa  Itrf oee!oenotou".ToCharArray();
            int ordering = 24;

            // The scrambled message is expected to be unscrambled back into the original message
            char[] expectedOutput = "Welcome to the Code Assessment! Please try your best.".ToCharArray();

            var result = algorithm.Order(input, ordering);

            Assert.IsTrue(result.IsSuccess);
            Assert.That(result.Value, Is.EqualTo(expectedOutput));
        }

        // It's also good practice to include additional tests covering a range of inputs and edge cases
        [Test]
        public void Order_WithNullInput_ShouldReturnFailure()
        {
            var algorithm = new UnorderByAlgorithm();
            var result = algorithm.Order(null, 24);
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Error.Message, Is.EqualTo("Input cannot be null or empty."));
        }

        [Test]
        public void Order_WithEmptyInput_ShouldReturnFailure()
        {
            var algorithm = new UnorderByAlgorithm();
            var result = algorithm.Order(Array.Empty<char>(), 24);
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Error.Message, Is.EqualTo("Input cannot be null or empty."));
        }

        [Test]
        public void Order_WithInvalidOrdering_ShouldReturnFailure()
        {
            var algorithm = new UnorderByAlgorithm();
            var result = algorithm.Order(new char[] { 'a', 'b', 'c' }, -1); // Using an invalid ordering value
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Error.Message, Is.EqualTo("Ordering must be greater than zero."));
        }
    }

}

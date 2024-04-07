using LoanCalculator.Application.Feature;

namespace LoanCalculatorTest
{
    [TestFixture]
    public class AdvanceTask1Test
    {
        [Test]
        public void CalculateLoanTenure_For1750MonthlyInstallment_ShouldReturnExpectedTenure()
        {
            var command = new CalculateLoanTenureBasedOnRepaymentCommand(95000, 1750);
            var handler = new CalculateLoanTenureBasedOnRepaymentCommandHandler();
            var result = handler.Handle(command);
            Assert.IsTrue(result.IsSuccess);
            // Replace "expectedYears" with the actual calculated value based on your loan formula and conditions
            // Assert.AreEqual(expectedYears, result.Value);
        }

        [Test]
        public void CalculateLoanTenure_WithNegativeMonthlyInstallment_ShouldReturnError()
        {
            var command = new CalculateLoanTenureBasedOnRepaymentCommand(95000, -1750); 
            var handler = new CalculateLoanTenureBasedOnRepaymentCommandHandler();
            var result = handler.Handle(command);

            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Error.Message, Is.EqualTo("Invalid input values. Ensure all inputs are greater than zero."));
        }

        [Test]
        public void CalculateLoanTenure_WithNegativePrinciple_ShouldReturnError()
        {
            var command = new CalculateLoanTenureBasedOnRepaymentCommand(-95000, 1750); 
            var handler = new CalculateLoanTenureBasedOnRepaymentCommandHandler();
            var result = handler.Handle(command);

            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Error.Message, Is.EqualTo("Invalid input values. Ensure all inputs are greater than zero."));
        }

        [Test]
        public void CalculateLoanTenure_WithNegativePrincipleAndMonthInstallament_ShouldReturnError()
        {
            var command = new CalculateLoanTenureBasedOnRepaymentCommand(-95000, -1750);
            var handler = new CalculateLoanTenureBasedOnRepaymentCommandHandler();
            var result = handler.Handle(command);

            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Error.Message, Is.EqualTo("Invalid input values. Ensure all inputs are greater than zero."));
        }
    }

}
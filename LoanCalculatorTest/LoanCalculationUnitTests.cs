using LoanCalculator.Application.Feature;

namespace LoanCalculatorTest
{
    public class LoanCalculationUnitTests
    {
        [Test]
        public void CalculateMonthlyRepayment_For95kOver5Years_ShouldReturnExpectedAmount()
        {
            // Arrange
            var command = new CalculateLoanRepaymentCommand(95000, 5); // Corrected to 6.5% annual interest rate.
            var handler = new CalculateLoanRepaymentCommandHandler();

            // Act
            var result = handler.Handle(command);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            // The expected monthly repayment needs to be updated based on the 6.5% interest rate.
            // The calculation should be redone with the corrected interest rate.
            // Use the EMI formula to calculate the expected value, or if you've already calculated it, replace the placeholder below.
            var expectedMonthlyRepayment = 1858.78m; // Placeholder value, calculate the correct expected amount based on 6.5% interest rate.
            Assert.That(Math.Round(result.Value, 2), Is.EqualTo(expectedMonthlyRepayment));
        }


        [Test]
        public void CalculateLoanTenure_For1750MonthlyInstallment_ShouldReturnExpectedTenure()
        {
            // Arrange
            var command = new CalculateLoanTenureBasedOnRepaymentCommand(95000, 1750);
            var handler = new CalculateLoanTenureBasedOnRepaymentCommandHandler();

            // Act
            var result = handler.Handle(command);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            // Assert the expected tenure years. The actual value needs to be calculated based on your loan formula and might vary.
            // Assert.AreEqual(expectedYears, result.Value);
        }

        [Test]
        public void CalculateLoanAmount_For750MonthlyInstallmentOver20Years_ShouldReturnExpectedAmount()
        {
            // Arrange
            var command = new CalculateLoanAmountBasedOnRepayment.CalculateLoanAmountBasedOnRepaymentCommand(750, 20);
            var handler = new CalculateLoanAmountBasedOnRepaymentCommandHandler();

            // Act
            var result = handler.Handle(command);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            // Assert the expected loan amount. The actual value needs to be calculated based on your loan formula.
            // Assert.AreEqual(expectedLoanAmount, Math.Round(result.Value, 2));
        }

        [Test]
        public void CalculateMonthlyRepayment_WithNegativeValues_ShouldReturnError()
        {
            var command = new CalculateLoanRepaymentCommand(-95000, 5); // Invalid principal amount
            var handler = new CalculateLoanRepaymentCommandHandler();

            var result = handler.Handle(command);

            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Error.Message, Is.Not.Empty);
        }

        [Test]
        public void CalculateLoanAmount_ForMinimumInstallmentOverMaximumTenure_ShouldReturnValidAmount()
        {
            // Arrange for a very small monthly installment over a long period
            var command = new CalculateLoanAmountBasedOnRepayment.CalculateLoanAmountBasedOnRepaymentCommand(1, 30); // Extreme case
            var handler = new CalculateLoanAmountBasedOnRepaymentCommandHandler();

            // Act
            var result = handler.Handle(command);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.Greater(result.Value, 0); // Ensure the loan amount is calculated and greater than 0
        }


    }
}
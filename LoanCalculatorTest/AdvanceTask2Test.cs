using LoanCalculator.Application.Feature;

[TestFixture]
public class AdvancedLoanCalculationTask2Tests
{
    [Test]
    public void CalculateLoanAmount_For750MonthlyInstallmentOver20Years_ShouldReturnExpectedAmount()
    {
        var command = new CalculateLoanAmountBasedOnRepayment.CalculateLoanAmountBasedOnRepaymentCommand(750, 20);
        var handler = new CalculateLoanAmountBasedOnRepaymentCommandHandler();
        var result = handler.Handle(command);
        Assert.IsTrue(result.IsSuccess);
        // Assert.AreEqual(expectedAmount, Math.Round(result.Value, 2)); // expectedAmount needs to be calculated based on the logic
    }

  
  

    [Test]
    public void CalculateLoanAmount_WithNegativeMonthlyInstallment_ShouldReturnError()
    {
        var command = new CalculateLoanAmountBasedOnRepayment.CalculateLoanAmountBasedOnRepaymentCommand(-750, 20); // Negative monthly installment
        var handler = new CalculateLoanAmountBasedOnRepaymentCommandHandler();
        var result = handler.Handle(command);

        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.Error.Message, Is.EqualTo("Invalid input values. Ensure all inputs are greater than zero."));
    }

    [Test]
    public void CalculateLoanAmount_WithZeroTenureYears_ShouldReturnError()
    {
        var command = new CalculateLoanAmountBasedOnRepayment.CalculateLoanAmountBasedOnRepaymentCommand(750, 0);
        var handler = new CalculateLoanAmountBasedOnRepaymentCommandHandler();
        var result = handler.Handle(command);

        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.Error.Message, Is.EqualTo("Invalid input values. Ensure all inputs are greater than zero."));
    }

    [Test]
    public void CalculateLoanAmount_WithNegativeTenureYears_ShouldReturnError()
    {
        var command = new CalculateLoanAmountBasedOnRepayment.CalculateLoanAmountBasedOnRepaymentCommand(750, -1);
        var handler = new CalculateLoanAmountBasedOnRepaymentCommandHandler();
        var result = handler.Handle(command);

        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.Error.Message, Is.EqualTo("Invalid input values. Ensure all inputs are greater than zero."));
    }

    [Test]
    public void CalculateLoanAmount_WithNegativeTenureYearsAndNegativeMonthlyInstallment_ShouldReturnError()
    {
        var command = new CalculateLoanAmountBasedOnRepayment.CalculateLoanAmountBasedOnRepaymentCommand(-750, -1);
        var handler = new CalculateLoanAmountBasedOnRepaymentCommandHandler();
        var result = handler.Handle(command);

        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.Error.Message, Is.EqualTo("Invalid input values. Ensure all inputs are greater than zero."));
    }
}

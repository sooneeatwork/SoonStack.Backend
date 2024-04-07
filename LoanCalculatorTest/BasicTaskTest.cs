using LoanCalculator.Application.Feature;

[TestFixture]
public class BasicLoanCalculationTests
{
    [Test]
    public void CalculateMonthlyRepayment_For95kOver5Years_ShouldReturnExpectedAmount()
    {
        var command = new CalculateLoanRepaymentCommand(95000, 5);
        var handler = new CalculateLoanRepaymentCommandHandler();
        var result = handler.Handle(command);
        Assert.IsTrue(result.IsSuccess);
        Assert.That(Math.Round(result.Value, 2), Is.EqualTo(1858.78m));
    }

    [Test]
    public void CalculateMonthlyRepayment_WithNegativePrincipal_ShouldReturnError()
    {
        var command = new CalculateLoanRepaymentCommand(-95000, 5);
        var handler = new CalculateLoanRepaymentCommandHandler();
        var result = handler.Handle(command);
        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.Error.Message, Is.Not.Empty);
    }
}

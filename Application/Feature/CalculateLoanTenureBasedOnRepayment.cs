using LoadCalculator.Domain.Model.Formulas;
using LoanCalculator.Application.Util;


namespace LoanCalculator.Application.Feature
{

    public record CalculateLoanTenureBasedOnRepaymentCommand(decimal Principal,
                                                             decimal TargetMonthlyInstallment);

    public class CalculateLoanTenureBasedOnRepaymentCommandHandler
    {
        public Result<int> Handle(CalculateLoanTenureBasedOnRepaymentCommand command)
        {
            // Basic input validation.
            if (command.Principal <= 0 || command.TargetMonthlyInstallment <= 0)
            {
                return Result<int>.Failure(new Error("Invalid input values. Ensure all inputs are greater than zero."));
            }

         
            int maxTenureMonths = (int)TieredLoanFormula.MaxTenureMonths; 

            try
            {
                var loanFormula = new TieredLoanFormula(command.Principal, 1); // TenureMonths initially set to 1; actual value determined by calculation.
                int calculatedTenureMonths = loanFormula.CalculateLoanPeriod(command.TargetMonthlyInstallment);

                if (calculatedTenureMonths > maxTenureMonths)
                {
                    return Result<int>.Failure(new Error($"Calculated tenure exceeds the maximum allowed limit of {maxTenureMonths / 12} years."));
                }

                return Result<int>.Success(calculatedTenureMonths/12); // Convert months to years for the result.
            }
            catch (Exception ex) // Catching general exceptions for simplicity; consider more specific exception handling.
            {
                return Result<int>.Failure(new Error($"An unexpected error occurred during calculation: {ex.Message}"));
            }
        }
    }
}



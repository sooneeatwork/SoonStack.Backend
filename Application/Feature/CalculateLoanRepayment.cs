using LoadCalculator.Domain.Model.Formulas;
using LoanCalculator.Application.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Feature
{
    public record CalculateLoanRepaymentCommand(decimal Principal, 
                                                int TenureYears, 
                                                decimal AnnualInterestRate);

    public class CalculateLoanRepaymentCommandHandler
    {
        public Result<decimal> Handle(CalculateLoanRepaymentCommand command)
        {
            // Validate command inputs
            var validationErrors = ValidateCommand(command);

            if (validationErrors.Any())
                return Result<decimal>.Failure(new Error(string.Join("; ", validationErrors)));

            Result<decimal> result;

            try
            {
                int tenureMonths = command.TenureYears * 12;
                var loanFormula = new TieredLoanFormula(command.Principal, tenureMonths);
                var monthlyRepayment = loanFormula.CalculateMonthlyInstallment();

                result = Result<decimal>.Success(monthlyRepayment);
            }
            catch (ArgumentOutOfRangeException)
            {
                // Specific exception handling can be refined based on the domain model's exceptions
                result = Result<decimal>.Failure(new Error("Invalid input: one or more input values are out of the allowable range."));
            }
            catch (Exception)
            {
                // General exception handling; consider logging the exception details in a real application
                result = Result<decimal>.Failure(new Error("An unexpected error occurred. Please try again later."));
            }

            return result;
        }

        private IEnumerable<string> ValidateCommand(CalculateLoanRepaymentCommand command)
        {
            if (command.Principal < 0)
                yield return "Principal must be greater than zero.";
            
            if (command.TenureYears < 0)
                yield return "Tenure years must be greater than zero.";
        }
    }


}


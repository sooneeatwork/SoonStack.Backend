using LoadCalculator.Domain.Model.Formulas;
using LoanCalculator.Application.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LoanCalculator.Application.Feature.CalculateLoanAmountBasedOnRepayment;

namespace LoanCalculator.Application.Feature
{
    public class CalculateLoanAmountBasedOnRepayment
    {
        public record CalculateLoanAmountBasedOnRepaymentCommand(decimal TargetMonthlyInstallment, int TenureYears);

    }

    public class CalculateLoanAmountBasedOnRepaymentCommandHandler
    {
        public Result<decimal> Handle(CalculateLoanAmountBasedOnRepaymentCommand command)
        {
            if (command.TargetMonthlyInstallment <= 0 || command.TenureYears <= 0)
            {
                return Result<decimal>.Failure(new Error("Invalid input values. Ensure all inputs are greater than zero."));
            }

            int tenureMonths = command.TenureYears * 12; // Convert years to months.

            try
            {
               
                var loanFormula = new TieredLoanFormula(0, tenureMonths); // Initial principal is set to 0 as a placeholder.
                // Calculate the loan amount that achieves the target monthly installment for the specified tenure.
                decimal loanAmount = loanFormula.CalculateLoanAmount(command.TargetMonthlyInstallment, tenureMonths);

                return Result<decimal>.Success(loanAmount);
            }
            catch (Exception ex) // Catching general exceptions; consider more specific handling based on your domain model's design.
            {
                return Result<decimal>.Failure(new Error($"An unexpected error occurred during calculation: {ex.Message}"));
            }
        }
    }
}

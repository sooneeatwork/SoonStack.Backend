using System;

namespace LoadCalculator.Domain.Model.Formulas
{
    public class TieredLoanFormula : LoanFormula
    {
        public const decimal MaxTenureMonths = 30 * 12; // Example: 30 years in months

        public TieredLoanFormula(decimal principal, int tenureMonths) : base(principal, tenureMonths)
        {
            Principal = principal;
            TenureMonths = tenureMonths;
        }

        protected override decimal GetAnnualInterestRate(decimal principal)
        {
            if (principal <= 20000) return 0.08m; // 8%
            else if (principal <= 50000) return 0.07m; // 7%
            else return 0.065m; // 6.5%

          
        }

       

        public override decimal CalculateMonthlyInstallment()
        {
            decimal monthlyInterestRate = GetAnnualInterestRate(Principal) / 12;
            decimal onePlusRPowerN = (decimal)Math.Pow((double)(1 + monthlyInterestRate), TenureMonths);
            decimal monthInstallment = Principal * monthlyInterestRate * onePlusRPowerN / (onePlusRPowerN - 1);
            decimal result = decimal.Round(monthInstallment, 2);

            return result;
        }

        public override int CalculateLoanPeriod(decimal maxMonthlyInstallment)
        {
            int minTenureMonths = 1;
            int maxTenureMonths = (int)MaxTenureMonths;

            while (minTenureMonths < maxTenureMonths)
            {
                int midPoint = (minTenureMonths + maxTenureMonths) / 2;
                TenureMonths = midPoint; // Set the loan period for correct EMI calculation

                decimal emi = CalculateMonthlyInstallment(); // Calculate the monthly installment with the adjusted loan period

                if (emi > maxMonthlyInstallment)
                {
                    minTenureMonths = midPoint + 1; // If the calculated EMI is greater than the target, adjust the lower bound
                }
                else
                {
                    maxTenureMonths = midPoint; // If the calculated EMI is less than or equal to the target, adjust the upper bound
                }
            }

            return minTenureMonths;
        }




        public override decimal CalculateLoanAmount(decimal targetMonthlyInstallment, int loanTermMonths)
        {
            decimal minPossibleLoan = 5000; // Minimum possible loan amount that can result in the target EMI.
            decimal maxPossibleLoan = 100000; // Start with a minimal high value to find the maximum possible loan amount.


            // Dynamically determine a reasonable upper bound for the loan amount
            while (true)
            {
                Principal = maxPossibleLoan;
                TenureMonths = loanTermMonths; // Set the loan term for the calculation.
                decimal calculatedMonthlyInstallment = CalculateMonthlyInstallment();
                if (calculatedMonthlyInstallment > targetMonthlyInstallment) break; // Found an upper loan amount where targetMonthlyInstallment exceeds the target.
                maxPossibleLoan *= 2; // Double to expand the search range efficiently.
            }

            // Perform binary search within the determined loan amount range
            while (maxPossibleLoan - minPossibleLoan > 1)
            {
                Principal = (minPossibleLoan + maxPossibleLoan) / 2;
                TenureMonths = loanTermMonths; // Reconfirm the loan term for each calculation.
                decimal calculatedEmi = CalculateMonthlyInstallment();
                if (calculatedEmi < targetMonthlyInstallment) minPossibleLoan = Principal; // Adjust the lower bound up if EMI is below target.
                else maxPossibleLoan = Principal; // Adjust the upper bound down if EMI meets or exceeds the target.
            }

            // Return the lowest loan amount that closely achieves the target EMI.
            return minPossibleLoan;
        }
    }
}

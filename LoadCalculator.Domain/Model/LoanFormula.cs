namespace LoadCalculator.Domain.Model
{
    public abstract class LoanFormula
    {
        protected decimal Principal { get; set; }
        protected int TenureMonths { get; set; }
        protected decimal MonthlyInterestRate => GetAnnualInterestRate(Principal) / 12 / 100;

        // Constructor
        protected LoanFormula(decimal principal, int tenureMonths)
        {
            Principal = principal;
            TenureMonths = tenureMonths;
        }

        // Abstract method to calculate the monthly installment based on the principal, tenure, and interest rate
        public abstract decimal CalculateMonthlyInstallment();

        // Method to determine the appropriate annual interest rate based on the principal amount
        protected abstract decimal GetAnnualInterestRate(decimal principal);

        // Method to calculate the monthly interest rate from the annual interest rate
        protected decimal GetMonthlyInterestRate(decimal principal)
        {
            return GetAnnualInterestRate(principal) / 12 / 100;
        }

        // Calculate the loan period (in months) for a given EMI amount
        public abstract int CalculateLoanPeriod(decimal loanAmt);

        // Calculate the loan amount for a given EMI and tenure
        public abstract decimal CalculateLoanAmount(decimal targetEmi, int tenureMonths);
    }


}

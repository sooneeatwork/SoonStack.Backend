using LoanCalculator.Application.Feature;


namespace LoanCalculator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool keepRunning = true;

            while (keepRunning)
            {
                Console.WriteLine("Loan Calculator");
                Console.WriteLine("1. Calculate Loan Repayment");
                Console.WriteLine("2. Calculate Loan Tenure Based on Repayment");
                Console.WriteLine("3. Calculate Loan Amount Based on Monthly Repayment");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        CalculateLoanRepayment();
                        break;
                    case 2:
                        CalculateLoanTenureBasedOnRepayment();
                        break;
                    case 3:
                        CalculateLoanAmountBasedOnMonthlyRepayment();
                        break;
                    case 4:
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void CalculateLoanRepayment()
        {
            Console.Write("Enter Principal Amount: ");
            decimal principal = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Tenure Years: ");
            int tenureYears = int.Parse(Console.ReadLine());

            Console.Write("Enter Annual Interest Rate: ");
            decimal annualInterestRate = decimal.Parse(Console.ReadLine());

            var command = new CalculateLoanRepaymentCommand(principal, tenureYears);
            var handler = new CalculateLoanRepaymentCommandHandler();
            var result = handler.Handle(command);

            if (result.IsSuccess)
                Console.WriteLine($"Monthly Repayment: {result.Value}");
            else
                Console.WriteLine($"Error: {result.Error.Message}");
        }

        static void CalculateLoanTenureBasedOnRepayment()
        {
            Console.Write("Enter Principal Amount: ");
            decimal principal = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Target Monthly Installment: ");
            decimal targetMonthlyInstallment = decimal.Parse(Console.ReadLine());

            var command = new CalculateLoanTenureBasedOnRepaymentCommand(principal, targetMonthlyInstallment);
            var handler = new CalculateLoanTenureBasedOnRepaymentCommandHandler();
            var result = handler.Handle(command);

            if (result.IsSuccess)
                Console.WriteLine($"Loan Tenure: {result.Value} years");
            else
                Console.WriteLine($"Error: {result.Error.Message}");
        }

        static void CalculateLoanAmountBasedOnMonthlyRepayment()
        {
            Console.Write("Enter Target Monthly Installment: ");
            decimal targetMonthlyInstallment = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Tenure Years: ");
            int tenureYears = int.Parse(Console.ReadLine());

            var command = new CalculateLoanAmountBasedOnRepayment.CalculateLoanAmountBasedOnRepaymentCommand(targetMonthlyInstallment, tenureYears);
            var handler = new CalculateLoanAmountBasedOnRepaymentCommandHandler();
            var result = handler.Handle(command);

            if (result.IsSuccess)
                Console.WriteLine($"Loan Amount Needed: RM {result.Value:N2}");
            else
                Console.WriteLine($"Error: {result.Error.Message}");
        }
    }
}

using ReferenceNumberAlgo.Domain;
using ReferenceNumberAlgo.Domain.ReferencesNumberFormula;
using ReferencesNumberAlgo.Application.Feature;
using ReferencesNumberAlgo.Application.Util;
using System.Collections.Concurrent;

namespace ReferenceNumberAlgo.ConsoleApp
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Reference Number Generator and Special Digit Frequency Calculator!");
            bool continueRunning = true;

            while (continueRunning)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Generate Reference Number");
                Console.WriteLine("2. Calculate Special Digit Frequency");
                Console.WriteLine("3. Exit");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        GenerateReferenceNumber();
                        break;
                    case 2:
                        CalculateSpecialDigitFrequency();
                        break;
                    case 3:
                        continueRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void GenerateReferenceNumber()
        {
            Console.Write("Enter input string: ");
            string input = Console.ReadLine();

            // Create an instance of the GenerateReferenceNumberCommandHandler
            ReferenceNumberFormula referenceNumberFormula = new CurrentReferencesNumberFormula();
            GenerateReferenceNumberCommandHandler commandHandler = new GenerateReferenceNumberCommandHandler(referenceNumberFormula);

            // Handle the GenerateReferenceNumberCommand
            Result<string> result = commandHandler.Handle(new GenerateReferenceNumberCommand(input));

            if (result.IsSuccess)
            {
                Console.WriteLine($"Reference Number: {result.Value}");
            }
            else
            {
                Console.WriteLine(result.Error.Message);
            }
        }

        static void CalculateSpecialDigitFrequency()
        {
            Console.Write("Enter start range: ");
            int start = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter end range: ");
            int end = Convert.ToInt32(Console.ReadLine());

            // Create an instance of the CalculateSpecialDigitFrequencyCommandHandler
            ReferenceNumberFormula referenceNumberFormula = new CurrentReferencesNumberFormula();
            CalculateSpecialDigitFrequencyCommandHandler commandHandler = new CalculateSpecialDigitFrequencyCommandHandler(referenceNumberFormula);

            // Handle the CalculateSpecialDigitFrequencyCommand
            Result<ConcurrentDictionary<int, int>> result = commandHandler.Handle(new CalculateSpecialDigitFrequencyCommand(start, end));

            if (result.IsSuccess)
            {
                Console.WriteLine("Special Digit Frequency:");
                foreach (var item in result.Value)
                {
                    Console.WriteLine($"{item.Key}: {item.Value}");
                }
            }
            else
            {
                Console.WriteLine(result.Error.Message);
            }
        }
    }
}

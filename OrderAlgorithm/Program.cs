using System;

namespace OrderByAlgorithmApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TaskSolver.SolveTask(1, "GHA14SFSD6K92", 16, new OrderByAlgorithm());
            TaskSolver.SolveTask(2, "e  rbml s nngeshsr etaet.loaldtryadaimt di ghtoeaeuse aC cuciy afskh ss t ovgo tna atstanmeempaa  Itrf oee!oenotou", 24, new UnorderByAlgorithm());
        }
    }

    public class TaskSolver
    {
        /// <summary>
        /// Solves the specified task using the specified order algorithm.
        /// </summary>
        /// <typeparam name="T">The type of the order algorithm.</typeparam>
        /// <param name="taskNumber">The task number.</param>
        /// <param name="input">The input string.</param>
        /// <param name="ordering">The ordering value.</param>
        /// <param name="algorithm">The order algorithm.</param>
        public static void SolveTask<T>(int taskNumber, string input, int ordering, T algorithm) where T : IOrderAlgorithm
        {
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine($"Task {taskNumber}: Input cannot be null or empty.");
                return;
            }

            if (ordering <= 0)
            {
                Console.WriteLine($"Task {taskNumber}: Ordering must be greater than zero.");
                return;
            }

            var result = algorithm.Order(input.ToCharArray(), ordering);

            string message = result.IsSuccess ?
                                  new string(result.Value) :
                                  result.Error.Message;

            Console.WriteLine($"Task {taskNumber}: {message}");
        }
    }

    public interface IOrderAlgorithm
    {
        /// <summary>
        /// Orders the input array according to the specified ordering value.
        /// </summary>
        /// <param name="input">The input array.</param>
        /// <param name="ordering">The ordering value.</param>
        /// <returns>A <see cref="Result{T}"/> object containing the ordered array or an error message.</returns>
        Result<char[]> Order(char[] input, int ordering);
    }


    public  class OrderByAlgorithm : IOrderAlgorithm
    {
        private const char Separator = '-';

        public Result<char[]> Order(char[] input, int ordering)
        {
            if (input == null || input.Length == 0)
            {
                return Result<char[]>.Failure(new Error("Input cannot be null or empty."));
            }

            if (ordering <= 0)
            {
                return Result<char[]>.Failure(new Error("Ordering must be greater than zero."));
            }

            try
            {
                var isSelected = new bool[input.Length]; // Track if a character is already selected
                var output = new List<char>();
                int totalSelected = 0;
                int currentIndex = 0;

                while (totalSelected < input.Length)
                {
                    int steps = ordering;
                    while (steps > 0)
                    {
                        if (!isSelected[currentIndex])
                        {
                            steps--;
                            if (steps == 0) break;
                        }
                        currentIndex = (currentIndex + 1) % input.Length; // Move to next character, wrap around if necessary
                    }

                    // Mark the current character as selected and add it to the output
                    isSelected[currentIndex] = true;
                    output.Add(input[currentIndex]);
                    totalSelected++;

                    // Add a separator every 3 characters, but not at the end
                    if (totalSelected % 3 == 0 && totalSelected < input.Length)
                    {
                        output.Add(Separator);
                    }
                }

                return Result<char[]>.Success(output.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Result<char[]>.Failure(new Error("An error occurred while ordering the input."));
            }
        }

    }

    public  class UnorderByAlgorithm : IOrderAlgorithm
    {
        public Result<char[]> Order(char[] input, int ordering)
        {
            if (input == null || input.Length == 0)
            {
                return Result<char[]>.Failure(new Error("Input cannot be null or empty."));
            }

            if (ordering <= 0)
            {
                return Result<char[]>.Failure(new Error("Ordering must be greater than zero."));
            }

            try
            {
                var sequenceWithoutSeparators = input.Where(c => c != '-').ToArray();
                var output = new char[sequenceWithoutSeparators.Length];
                var used = new bool[sequenceWithoutSeparators.Length];

                // Initialize output positions based on input sequence excluding separators
                int pos = 0;
                for (int i = 0; i < sequenceWithoutSeparators.Length; i++)
                {
                    if (!used[pos])
                    {
                        output[pos] = sequenceWithoutSeparators[i];
                        used[pos] = true;
                        int count = 1;
                        while (count < ordering)
                        {
                            pos = (pos + 1) % sequenceWithoutSeparators.Length;
                            if (!used[pos])
                            {
                                count++;
                            }
                        }
                    }
                }

                return Result<char[]>.Success(output);
            }
            catch (Exception ex)
            {
                return Result<char[]>.Failure(new Error($"An error occurred while unordering the input: {ex.Message}"));
            }
        }
    }

    public class Result<T>
    {
        public T Value { get; }
        public bool IsSuccess { get; }
        public Error Error { get; }

        protected Result(T value, bool isSuccess, Error error)
        {
            Value = value;
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result<T> Success(T value) => new Result<T>(value, true, null);
        public static Result<T> Failure(Error error) => new Result<T>(default, false, error);
    }

    public class Error
    {
        public string Message { get; }
        public Error(string message)
        {
            Message = message;
        }
    }

}

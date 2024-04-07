using ReferenceNumberAlgo.Domain;
using ReferencesNumberAlgo.Application.Util;
using System.Collections.Concurrent;

namespace ReferencesNumberAlgo.Application.Feature
{
    public record CalculateSpecialDigitFrequencyCommand(int Start, int End);

    public class CalculateSpecialDigitFrequencyCommandHandler
    {
        private readonly ReferenceNumberFormula _referenceNumberFormula;

        public CalculateSpecialDigitFrequencyCommandHandler(ReferenceNumberFormula referenceNumberFormula)
        {
            _referenceNumberFormula = referenceNumberFormula;
        }

        public Result<ConcurrentDictionary<int, int>> Handle(CalculateSpecialDigitFrequencyCommand command)
        {
            if (command.Start < 0 || command.End < 0 || command.Start > command.End)
                return Result<ConcurrentDictionary<int, int>>.Failure(new Error("Invalid input range."));

            try
            {
                var frequency = _referenceNumberFormula.CalculateSpecialDigitFrequency(command.Start, command.End);
                return Result<ConcurrentDictionary<int, int>>.Success(frequency);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Result<ConcurrentDictionary<int, int>>.Failure(new Error("An error occurred while calculating the special digit frequency."));
            }
        }
    }
}


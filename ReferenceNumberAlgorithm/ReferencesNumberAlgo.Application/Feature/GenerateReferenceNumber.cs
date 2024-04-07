using ReferenceNumberAlgo.Domain;
using ReferencesNumberAlgo.Application.Util;

namespace ReferencesNumberAlgo.Application.Feature
{
    public record GenerateReferenceNumberCommand(string Input);

    public class GenerateReferenceNumberCommandHandler
    {
        private readonly ReferenceNumberFormula _referenceNumberFormula;

        public GenerateReferenceNumberCommandHandler(ReferenceNumberFormula referenceNumberFormula)
        {
            _referenceNumberFormula = referenceNumberFormula;
        }

        public Result<string> Handle(GenerateReferenceNumberCommand command)
        {
            if (string.IsNullOrEmpty(command.Input))
            {
                return Result<string>.Failure(new Error("Input cannot be empty."));
            }

            try
            {
                var specialDigit = _referenceNumberFormula.CalculateSpecialDigit(command.Input);
                return Result<string>.Success(command.Input + specialDigit);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Result<string>.Failure(new Error("An error occurred while generating the reference number."));
            }
        }
    }
}

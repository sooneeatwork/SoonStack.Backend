using NUnit.Framework;
using ReferenceNumberAlgo.Domain;
using ReferenceNumberAlgo.Domain.ReferencesNumberFormula;
using ReferencesNumberAlgo.Application.Feature;
using ReferencesNumberAlgo.Application.Util;
using System.Collections.Concurrent;

namespace Test
{
    public class CalculateSpecialDigitFrequencyCommandHandlerTests
    {
        private readonly CalculateSpecialDigitFrequencyCommandHandler _commandHandler;

        public CalculateSpecialDigitFrequencyCommandHandlerTests()
        {
            ReferenceNumberFormula referenceNumberFormula = new CurrentReferencesNumberFormula();
            _commandHandler = new CalculateSpecialDigitFrequencyCommandHandler(referenceNumberFormula);
        }

        [Test]
        public void CalculateSpecialDigitFrequency_ValidRange_ReturnsSuccessResult()
        {
            // Arrange
            int start = 1;
            int end = 10;

            // Act
            Result<ConcurrentDictionary<int, int>> result = _commandHandler.Handle(new CalculateSpecialDigitFrequencyCommand(start, end));

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.IsNotEmpty(result.Value);
        }

        [Test]
        public void CalculateSpecialDigitFrequency_InvalidRange_ReturnsFailureResult()
        {
            // Arrange
            int start = 10;
            int end = 5;

            // Act
            Result<ConcurrentDictionary<int, int>> result = _commandHandler.Handle(new CalculateSpecialDigitFrequencyCommand(start, end));

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual("Invalid input range.", result.Error.Message);
        }
    }
}

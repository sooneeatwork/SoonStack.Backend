using NUnit.Framework;
using ReferenceNumberAlgo.Domain;
using ReferenceNumberAlgo.Domain.ReferencesNumberFormula;
using ReferencesNumberAlgo.Application.Feature;
using ReferencesNumberAlgo.Application.Util;

namespace Test
{
    public class GenerateReferenceNumberCommandHandlerTests
    {
        private readonly GenerateReferenceNumberCommandHandler _commandHandler;

        public GenerateReferenceNumberCommandHandlerTests()
        {
            ReferenceNumberFormula referenceNumberFormula = new CurrentReferencesNumberFormula();
            _commandHandler = new GenerateReferenceNumberCommandHandler(referenceNumberFormula);
        }

        [Test]
        public void GenerateReferenceNumber_ValidInput_ReturnsSuccessResult()
        {
            // Arrange
            string input = "20191001187";

            // Act
            Result<string> result = _commandHandler.Handle(new GenerateReferenceNumberCommand(input));

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.That(result.Value, Does.Match("201910011877"));
        }

        [Test]
        public void GenerateReferenceNumber_EmptyInput_ReturnsFailureResult()
        {
            // Arrange
            string input = "";

            // Act
            Result<string> result = _commandHandler.Handle(new GenerateReferenceNumberCommand(input));

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual("Input cannot be empty.", result.Error.Message);
        }
    }
}

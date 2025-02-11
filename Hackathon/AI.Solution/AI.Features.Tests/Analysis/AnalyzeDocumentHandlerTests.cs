using AI.Features.Analysis;
using AI.Features.Tests.TestDoubles;
using AI.Shared.Models;
using Microsoft.Extensions.Logging;
using static AI.Shared.Models.AnalysisRequest;

namespace AI.Features.Tests.Analysis;

public class AnalyzeDocumentHandlerTests
{
    private readonly FakeLLMService _llmService;
    private readonly AnalyzeDocumentHandler _handler;
    private readonly ILogger<AnalyzeDocumentHandler> _logger;
    public AnalyzeDocumentHandlerTests()
    {
        _llmService = new FakeLLMService();
        _handler = new AnalyzeDocumentHandler(_llmService, _logger);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnAnalysisResponse()
    {
        // Arrange
        var request = new AnalysisRequest("Test content", "Test type");

        // Act
        var result = await _handler.HandleAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test analysis result", result.Result);
    }

    [Fact]
    public async Task HandleAsync_WithEmptyContent_ShouldStillWork()
    {
        // Arrange
        var request = new AnalysisRequest("", "Test type");

        // Act
        var result = await _handler.HandleAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Result);
    }
}
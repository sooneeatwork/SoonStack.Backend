// AI.Features/Analysis/AnalyzeDocumentHandler.cs
using AI.Shared.Models;
using AI.Shared.Services;
using Microsoft.Extensions.Logging;

namespace AI.Features.Analysis;

public class AnalyzeDocumentHandler : IAnalyzeDocumentHandler
{
    private readonly ILLMService _llm;
    private readonly ILogger<AnalyzeDocumentHandler> _logger;

    public AnalyzeDocumentHandler(
        ILLMService llm,
        ILogger<AnalyzeDocumentHandler> logger)
    {
        _llm = llm;
        _logger = logger;
    }

    public async Task<AnalysisResponse> HandleAsync(AnalysisRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            _logger.LogInformation("Processing analysis request of type {Type}", request.Type);

            var result = await _llm.GenerateAsync(request.Content);
            var confidence = CalculateConfidence(result);

            return new AnalysisResponse(result, confidence);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing analysis request");
            throw;
        }
    }

    private double CalculateConfidence(string result)
    {
        // Implement your confidence calculation logic
        return 0.95;
    }
}
using AI.Shared.Models;

namespace AI.Features.Analysis;

public interface IAnalyzeDocumentHandler
{
    Task<AnalysisResponse> HandleAsync(AnalysisRequest request);
}

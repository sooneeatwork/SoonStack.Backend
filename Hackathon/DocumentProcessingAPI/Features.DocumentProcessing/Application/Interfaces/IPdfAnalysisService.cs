using Features.DocumentProcessing.Domain.Models;

public interface IPdfAnalysisService
{
    Task<AnalysisResult> AnalyzeDocumentAsync(Stream pdfStream);
    Task<AnalysisResult> AnalyzeDocumentAsync(Stream pdfStream, AnalysisOptions options);
}
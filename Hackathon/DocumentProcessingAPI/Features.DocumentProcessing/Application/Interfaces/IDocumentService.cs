using Microsoft.AspNetCore.Http;
using Features.DocumentProcessing.Domain;

namespace Features.DocumentProcessing.Application.Interfaces
{
    public interface IDocumentService
    {
        Task<string> UploadDocumentAsync(IFormFile file);
        Task<DocumentSummary> GetDocumentSummaryAsync(string documentId);
        Task<DocumentDiff> GetDocumentDiffAsync(string documentId);
    }

}

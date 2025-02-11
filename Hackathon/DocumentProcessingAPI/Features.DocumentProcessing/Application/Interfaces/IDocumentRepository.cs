using Microsoft.AspNetCore.Http;
using Features.DocumentProcessing.Domain;

namespace Features.DocumentProcessing.Application.Repositories
{
    public interface IDocumentRepository
    {
        Task<string> SaveDocumentAsync(IFormFile file);
        Task<DocumentSummary> FetchDocumentSummaryAsync(string documentId);
        Task<DocumentDiff> FetchDocumentDiffAsync(string documentId);
    }
}


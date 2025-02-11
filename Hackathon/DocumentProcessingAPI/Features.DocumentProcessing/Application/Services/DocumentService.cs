using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Features.DocumentProcessing.Domain.Models;
using Features.DocumentProcessing.Application.Interfaces;
using Features.DocumentProcessing.Domain;

namespace Features.DocumentProcessing.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IPdfAnalysisService _pdfAnalysisService;
        private readonly ILogger<DocumentService> _logger;
        private readonly string _uploadDirectory;

        public DocumentService(
            IPdfAnalysisService pdfAnalysisService,
            ILogger<DocumentService> logger)
        {
            _pdfAnalysisService = pdfAnalysisService ?? throw new ArgumentNullException(nameof(pdfAnalysisService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            Directory.CreateDirectory(_uploadDirectory); // Ensure upload directory exists
        }

        public async Task<string> UploadDocumentAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    throw new ArgumentException("No file provided");

                // Generate unique file name
                string documentId = Guid.NewGuid().ToString();
                string filePath = Path.Combine(_uploadDirectory, $"{documentId}{Path.GetExtension(file.FileName)}");

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return documentId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading document");
                throw;
            }
        }

        public async Task<DocumentSummary> GetDocumentSummaryAsync(string documentId)
        {
            try
            {
                if (string.IsNullOrEmpty(documentId))
                    throw new ArgumentException("Document ID cannot be empty", nameof(documentId));

                string filePath = GetFilePath(documentId);
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                    return null;

                using var stream = File.OpenRead(filePath);
                var analysisResult = await _pdfAnalysisService.AnalyzeDocumentAsync(
                    stream,
                    new AnalysisOptions { AnalysisType = AnalysisType.FullAnalysis }
                );

                if (!analysisResult.Success)
                {
                    _logger.LogError("Failed to analyze document: {Error}", analysisResult.ErrorMessage);
                    throw new Exception($"Analysis failed: {analysisResult.ErrorMessage}");
                }

                return new DocumentSummary
                {
                    DocumentId = documentId,
                    Summary = analysisResult.Summary,
                    ProcessingTime = TimeSpan.FromMilliseconds(analysisResult.ProcessingTimeMs),
                    AnalysisTimestamp = analysisResult.AnalysisTimestamp
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting document summary for ID: {Id}", documentId);
                throw;
            }
        }

        public async Task<DocumentDiff> GetDocumentDiffAsync(string documentId)
        {
            try
            {
                if (string.IsNullOrEmpty(documentId))
                    throw new ArgumentException("Document ID cannot be empty", nameof(documentId));

                string filePath = GetFilePath(documentId);
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                    return null;

                using var stream = File.OpenRead(filePath);
                var analysisResult = await _pdfAnalysisService.AnalyzeDocumentAsync(
                    stream,
                    new AnalysisOptions { AnalysisType = AnalysisType.FullAnalysis }
                );

                if (!analysisResult.Success)
                {
                    _logger.LogError("Failed to analyze document: {Error}", analysisResult.ErrorMessage);
                    throw new Exception($"Analysis failed: {analysisResult.ErrorMessage}");
                }

                return new DocumentDiff
                {
                    DocumentId = documentId,
                    Analysis = analysisResult.Summary,
                    ProcessingTime = TimeSpan.FromMilliseconds(analysisResult.ProcessingTimeMs),
                    AnalysisTimestamp = analysisResult.AnalysisTimestamp
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting document diff for ID: {Id}", documentId);
                throw;
            }
        }

        private string GetFilePath(string id)
        {
            // Search for file with any extension
            var files = Directory.GetFiles(_uploadDirectory, $"{id}.*");
            return files.FirstOrDefault();
        }
    }
}
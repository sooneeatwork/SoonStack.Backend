using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Features.DocumentProcessing.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Features.DocumentProcessing.Domain;

namespace DocumentProcessingAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly long _maxFileSize = 50 * 1024 * 1024; // 50MB
        private readonly string[] _allowedFileTypes = { ".pdf" };

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        // POST api/documents
        [HttpPost]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(50 * 1024 * 1024)] // 50MB
        [RequestFormLimits(MultipartBodyLengthLimit = 50 * 1024 * 1024)] // 50MB
        public async Task<IActionResult> UploadDocument(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded.");

                // Check file size
                if (file.Length > _maxFileSize)
                    return BadRequest($"File size exceeds maximum limit of {_maxFileSize / (1024 * 1024)}MB");

                // Check file type
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_allowedFileTypes.Contains(fileExtension))
                    return BadRequest("Invalid file type. Only PDF files are allowed.");

                var documentId = await _documentService.UploadDocumentAsync(file);
                return Ok(new
                {
                    DocumentId = documentId,
                    Message = $"File uploaded successfully. Size: {file.Length / 1024.0:F2}KB",
                    FileName = file.FileName
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET api/documents/{id}/summary
        [HttpGet("{id}/summary")]
        [ProducesResponseType(typeof(DocumentSummary), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDocumentSummary(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return BadRequest("Document ID cannot be empty");

                var summary = await _documentService.GetDocumentSummaryAsync(id);
                if (summary == null)
                    return NotFound($"Document with ID {id} not found");

                return Ok(summary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET api/documents/{id}/diff
        [HttpGet("{id}/diff")]
        [ProducesResponseType(typeof(DocumentDiff), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDocumentDiff(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return BadRequest("Document ID cannot be empty");

                var diff = await _documentService.GetDocumentDiffAsync(id);
                if (diff == null)
                    return NotFound($"Document with ID {id} not found");

                return Ok(diff);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
// AI.Gateway.Api/Controllers/AnalysisController.cs
using AI.Features.Analysis;
using AI.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace AI.Gateway.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AnalysisController : ControllerBase
{
    private readonly IAnalyzeDocumentHandler _handler;
    private readonly ILogger<AnalysisController> _logger;

    public AnalysisController(
        IAnalyzeDocumentHandler handler,
        ILogger<AnalysisController> logger)
    {
        _handler = handler;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AnalysisResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AnalysisResponse>> Analyze(
        [FromBody] AnalysisRequest request)
    {
        try
        {
            _logger.LogInformation("Received analysis request of type {Type}", request.Type);
            var result = await _handler.HandleAsync(request);
            return Ok(result);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogWarning(ex, "Invalid request received");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing analysis request");
            return StatusCode(500, "An error occurred processing the request");
        }
    }
}
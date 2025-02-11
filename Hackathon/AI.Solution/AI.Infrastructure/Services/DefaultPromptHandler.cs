// AI.Infrastructure/Services/DefaultPromptHandler.cs
using AI.Shared.Services;
using Microsoft.Extensions.Logging;

namespace AI.Infrastructure.Services;

public class DefaultPromptHandler : IPromptHandler
{
    private readonly ILogger<DefaultPromptHandler> _logger;

    public DefaultPromptHandler(ILogger<DefaultPromptHandler> logger)
    {
        _logger = logger;
    }

    public Task<string> FormatPromptAsync(string prompt)
    {
        try
        {
            if (string.IsNullOrEmpty(prompt))
                throw new ArgumentException("Prompt cannot be empty", nameof(prompt));

            _logger.LogDebug("Formatting prompt: {Length} characters", prompt.Length);
            return Task.FromResult(prompt);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error formatting prompt");
            throw;
        }
    }
}

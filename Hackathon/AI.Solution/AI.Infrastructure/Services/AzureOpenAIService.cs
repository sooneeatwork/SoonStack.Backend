// AI.Infrastructure/Services/AzureOpenAIService.cs
using AI.Shared.Services;
using Microsoft.SemanticKernel;
using Microsoft.Extensions.Logging;

namespace AI.Infrastructure.Services;

public class AzureOpenAIService : ILLMService
{
    private readonly Kernel _kernel;
    private readonly IPromptHandler _promptHandler;
    private readonly ILogger<AzureOpenAIService> _logger;

    public AzureOpenAIService(
        IOpenAIConfigurationProvider configProvider,
        IPromptHandler promptHandler,
        ILogger<AzureOpenAIService> logger)
    {
        _promptHandler = promptHandler;
        _logger = logger;

        var config = configProvider.GetConfigurationAsync().GetAwaiter().GetResult();
        _kernel = Kernel.CreateBuilder()
            .AddAzureOpenAIChatCompletion(
                deploymentName: config.DeploymentName,
                endpoint: config.Endpoint,
                apiKey: config.ApiKey)
            .Build();


    }

    public async Task<string> GenerateAsync(string prompt)
    {
        try
        {
            _logger.LogInformation("Generating response for prompt: {Length} characters", prompt.Length);

            var formattedPrompt = await _promptHandler.FormatPromptAsync(prompt);
            var function = _kernel.CreateFunctionFromPrompt(formattedPrompt);
            var result = await _kernel.InvokeAsync(function);

            var response = result.GetValue<string>() ?? string.Empty;
            _logger.LogInformation("Generated response: {Length} characters", response.Length);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating response for prompt");
            throw;
        }
    }
}
// AI.Infrastructure/Configuration/AzureOpenAIConfigurationProvider.cs
using AI.Shared.Exceptions;
using AI.Shared.Models;
using AI.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AI.Infrastructure.Configuration;

public class AzureOpenAIConfigurationProvider : IOpenAIConfigurationProvider
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AzureOpenAIConfigurationProvider> _logger;

    public AzureOpenAIConfigurationProvider(
        IConfiguration configuration,
        ILogger<AzureOpenAIConfigurationProvider> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<OpenAIConfiguration> GetConfigurationAsync()
    {
        try
        {
            var config = new OpenAIConfiguration
            {
                DeploymentName = _configuration["Azure:OpenAI:DeploymentName"]
                    ?? throw new ConfigurationException("DeploymentName not found"),
                Endpoint = _configuration["Azure:OpenAI:Endpoint"]
                    ?? throw new ConfigurationException("Endpoint not found"),
                ApiKey = _configuration["Azure:OpenAI:ApiKey"]
                    ?? throw new ConfigurationException("ApiKey not found")
            };

            ValidateConfiguration(config);
            return await Task.FromResult(config);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving OpenAI configuration");
            throw;
        }
    }

    private void ValidateConfiguration(OpenAIConfiguration config)
    {
        if (string.IsNullOrWhiteSpace(config.DeploymentName))
            throw new ConfigurationException("DeploymentName cannot be empty");
        if (string.IsNullOrWhiteSpace(config.Endpoint))
            throw new ConfigurationException("Endpoint cannot be empty");
        if (string.IsNullOrWhiteSpace(config.ApiKey))
            throw new ConfigurationException("ApiKey cannot be empty");

        _logger.LogInformation("Configured endpoint: {Endpoint}", config.Endpoint);
        if (!Uri.TryCreate(config.Endpoint, UriKind.Absolute, out _))
            throw new ConfigurationException("Endpoint must be a valid URI");
    }
}
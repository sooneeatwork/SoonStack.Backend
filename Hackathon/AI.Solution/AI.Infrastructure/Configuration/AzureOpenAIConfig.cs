// AI.Infrastructure/Configuration/AzureOpenAIConfig.cs
using Microsoft.Extensions.Configuration;

namespace AI.Infrastructure.Configuration;

public class AzureOpenAIConfig
{
    public required string DeploymentName { get; set; }
    public required string Endpoint { get; set; }
    public required string ApiKey { get; set; }

    public static AzureOpenAIConfig Create(IConfiguration configuration)
    {
        return new AzureOpenAIConfig
        {
            DeploymentName = configuration["Azure:OpenAI:DeploymentName"]
                ?? throw new ArgumentNullException("DeploymentName"),
            Endpoint = configuration["Azure:OpenAI:Endpoint"]
                ?? throw new ArgumentNullException("Endpoint"),
            ApiKey = configuration["Azure:OpenAI:ApiKey"]
                ?? throw new ArgumentNullException("ApiKey")
        };
    }

    public Dictionary<string, string> ToDictionary()
    {
        return new Dictionary<string, string>
        {
            { "DeploymentName", DeploymentName },
            { "Endpoint", Endpoint },
            { "ApiKey", ApiKey }
        };
    }
}
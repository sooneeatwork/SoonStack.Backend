namespace AI.Shared.Models;

public record OpenAIConfiguration
{
    public required string DeploymentName { get; init; }
    public required string Endpoint { get; init; }
    public required string ApiKey { get; init; }
}
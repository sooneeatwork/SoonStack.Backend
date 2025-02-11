using AI.Shared.Models;

namespace AI.Shared.Services;

public interface IOpenAIConfigurationProvider
{
    Task<OpenAIConfiguration> GetConfigurationAsync();
}

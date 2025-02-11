// Infrastructure/Configuration/AWSConfigurationExtensions.cs
using Amazon;
using Amazon.BedrockRuntime;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentProcessingAPI.Infrastructure.Configuration
{
    public static class AWSConfigurationExtensions
    {
        public static IServiceCollection AddAWSServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Get and validate AWS configuration
            var awsConfig = configuration.GetSection("AWS").Get<AWSConfiguration>();
            if (awsConfig == null)
            {
                throw new InvalidOperationException("AWS configuration section is missing from configuration.");
            }

            // Validate configuration
            awsConfig.Validate();

            // Create AWS credentials
            var credentials = new BasicAWSCredentials(awsConfig.AccessKeyId, awsConfig.SecretAccessKey);

            // Register AWS Bedrock service
            services.AddAWSService<IAmazonBedrockRuntime>(new AWSOptions
            {
                Region = RegionEndpoint.GetBySystemName(awsConfig.Region),
                Credentials = credentials
            });

            // Register the configuration instance
            services.AddSingleton(awsConfig);

            return services;
        }

        public static AWSConfiguration GetAWSConfiguration(this IConfiguration configuration)
        {
            var awsConfig = configuration.GetSection("AWS").Get<AWSConfiguration>();
            if (awsConfig == null)
            {
                throw new InvalidOperationException("AWS configuration section is missing from configuration.");
            }

            awsConfig.Validate();
            return awsConfig;
        }
    }
}

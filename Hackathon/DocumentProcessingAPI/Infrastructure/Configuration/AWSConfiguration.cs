using System.ComponentModel.DataAnnotations;

namespace DocumentProcessingAPI.Infrastructure.Configuration
{
    public class AWSConfiguration
    {
        [Required(ErrorMessage = "AWS Profile is required")]
        public string Profile { get; set; }

        [Required(ErrorMessage = "AWS Region is required")]
        public string Region { get; set; }

        [Required(ErrorMessage = "AWS Access Key ID is required")]
        public string AccessKeyId { get; set; }

        [Required(ErrorMessage = "AWS Secret Access Key is required")]
        public string SecretAccessKey { get; set; }

        public void Validate()
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true))
            {
                throw new ValidationException(
                    $"AWS Configuration validation failed: {string.Join(", ", validationResults.Select(r => r.ErrorMessage))}");
            }
        }
    }
}

namespace DocumentProcessingAPI.Infrastructure.ExternalServices
{
    public interface IAWSTextractService
    {
        string ExtractTextFromPdf(byte[] pdfBytes);
    }

    public class AWSTextractService : IAWSTextractService
    {
        public string ExtractTextFromPdf(byte[] pdfBytes)
        {
            // TODO: Implement AWS Textract API calls here.
            return "Extracted text from PDF.";
        }
    }
}

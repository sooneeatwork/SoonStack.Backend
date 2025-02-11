using System.Net.Http.Json;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Logging;
using UglyToad.PdfPig;
using Features.DocumentProcessing.Domain.Models;


namespace DocumentProcessingAPI.Services
{
    public class GeminiPdfAnalysisService : IPdfAnalysisService, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GeminiPdfAnalysisService> _logger;
        private const int MaxTokenLimit = 30000;
        private const string ModelName = "gemini-pro";
        private const string ApiEndpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent";

        public GeminiPdfAnalysisService(string apiKey, ILogger<GeminiPdfAnalysisService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException(nameof(apiKey));

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("x-goog-api-key", apiKey);
        }

        public async Task<AnalysisResult> AnalyzeDocumentAsync(Stream pdfStream)
        {
            if (pdfStream == null || !pdfStream.CanRead)
                throw new ArgumentException("Invalid PDF stream provided", nameof(pdfStream));

            return await AnalyzeDocumentInternalAsync(pdfStream, AnalysisType.FullAnalysis);
        }

        public async Task<AnalysisResult> AnalyzeDocumentAsync(Stream pdfStream, AnalysisOptions options)
        {
            if (pdfStream == null || !pdfStream.CanRead)
                throw new ArgumentException("Invalid PDF stream provided", nameof(pdfStream));
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return await AnalyzeDocumentInternalAsync(pdfStream, options.AnalysisType);
        }

        private async Task<AnalysisResult> AnalyzeDocumentInternalAsync(Stream pdfStream, AnalysisType analysisType)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                string extractedText = await ExtractTextFromPdfAsync(pdfStream);
                if (string.IsNullOrWhiteSpace(extractedText))
                {
                    return CreateErrorResult("No text could be extracted from the PDF", stopwatch.ElapsedMilliseconds);
                }

                string prompt = GeneratePromptForAnalysisType(analysisType);
                var truncatedText = TruncateText(extractedText);

                var request = new
                {
                    contents = new[]
                    {
                        new
                        {
                            role = "user",
                            parts = new[]
                            {
                                new { text = $"{prompt}\n\nDocument text: {truncatedText}" }
                            }
                        }
                    }
                };

                var response = await _httpClient.PostAsJsonAsync($"{ApiEndpoint}", request);
                stopwatch.Stop();

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return CreateErrorResult($"API Error: {response.StatusCode} - {errorContent}", stopwatch.ElapsedMilliseconds);
                }

                var responseContent = await response.Content.ReadFromJsonAsync<GeminiResponse>();

                if (responseContent?.Candidates == null || responseContent.Candidates.Length == 0)
                {
                    return CreateErrorResult("No response generated from the model", stopwatch.ElapsedMilliseconds);
                }

                return new AnalysisResult
                {
                    Success = true,
                    Summary = responseContent.Candidates[0].Content.Parts[0].Text,
                    ProcessingTimeMs = stopwatch.ElapsedMilliseconds,
                    AnalysisTimestamp = DateTimeOffset.UtcNow,
                    ModelUsed = ModelName
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error analyzing document with Gemini");
                return CreateErrorResult($"Analysis error: {ex.Message}", stopwatch.ElapsedMilliseconds);
            }
        }

        private string GeneratePromptForAnalysisType(AnalysisType analysisType) => analysisType switch
        {
            AnalysisType.FullAnalysis => @"Analyze this document and present it in a clear, conversational manner:

1. QUICK OVERVIEW
Write a friendly, 2-3 sentence introduction explaining what this document is about and why it matters.

2. THE BIG PICTURE
- What's the main goal?
- Who needs to know about this?
- When do people need to act on this?

3. KEY POINTS YOU SHOULD KNOW
Break down the 3-5 most important things in simple, everyday language.
Start each point with 'Here's what you need to know about...'

4. BREAKING IT DOWN
For each main section of the document:
- What's this part about? (in plain language)
- Why does it matter to the reader?
- What are the practical takeaways?

5. IMPORTANT DETAILS
- Dates to remember (if any)
- Numbers that matter (if any)
- Must-do actions (if any)

6. WHAT THIS MEANS FOR YOU
- Explain the real-world impact
- What should readers do next?
- What to watch out for

7. HELPFUL TIPS
- Include practical advice
- Common questions answered
- Useful resources

Write in a conversational tone, as if explaining to a colleague over coffee. Avoid technical jargon unless absolutely necessary, and when used, explain it in simple terms.

Format using clear headings and bullet points for easy reading, but maintain a natural flow between sections.",

            AnalysisType.Summary => @"Present a friendly, easy-to-read summary of this document:

THE BASICS
In 2-3 conversational sentences, tell us what this document is and why we should care.

QUICK HIGHLIGHTS
Share 3-4 key points that anyone should know, written in simple, everyday language.

MAIN TAKEAWAYS
Break down the most important parts as if explaining to a friend:
- What's happening
- Who it affects
- What people need to do
- When they need to do it

BOTTOM LINE
Sum up why this matters and what readers should do next.

Use clear, simple language and avoid technical terms unless necessary. Write as if having a conversation.",

            AnalysisType.KeyPoints => @"Break down this document in a friendly, accessible way:

FIRST THINGS FIRST
Tell us in plain language what this document is about.

HERE'S WHAT YOU REALLY NEED TO KNOW
List the 5 most important points, explained in everyday language.
Start each point with 'Here's something important...'

DATES & DEADLINES
If there are any important dates, list them here in a friendly way
(e.g., 'Mark your calendar for...')

WHAT THIS MEANS FOR YOU
Explain the practical impact in simple terms:
- What changes
- What actions to take
- What to prepare for

HELPFUL TIPS
Add any useful advice or common questions answered.

Keep it conversational and easy to understand, like explaining to a friend.",

            _ => throw new ArgumentException($"Unsupported analysis type: {analysisType}")
        };

        private async Task<string> ExtractTextFromPdfAsync(Stream pdfStream)
        {
            try
            {
                var memoryStream = new MemoryStream();
                await pdfStream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                var textBuilder = new StringBuilder();

                using (var document = PdfDocument.Open(memoryStream))
                {
                    foreach (var page in document.GetPages())
                    {
                        string pageText = page.Text;
                        if (!string.IsNullOrWhiteSpace(pageText))
                        {
                            textBuilder.AppendLine(pageText.Trim());
                            textBuilder.AppendLine(); // Add separation between pages
                        }
                    }
                }

                return textBuilder.ToString().Trim();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error extracting text from PDF");
                throw new InvalidOperationException("Failed to extract text from PDF", ex);
            }
        }

        private string TruncateText(string text)
        {
            return text.Length > MaxTokenLimit ? text.Substring(0, MaxTokenLimit) : text;
        }

        private AnalysisResult CreateErrorResult(string errorMessage, long processingTime) => new()
        {
            Success = false,
            ErrorMessage = errorMessage,
            ProcessingTimeMs = processingTime,
            AnalysisTimestamp = DateTimeOffset.UtcNow,
            ModelUsed = ModelName
        };

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }

    public class GeminiResponse
    {
        public Candidate[] Candidates { get; set; }
        public PromptFeedback PromptFeedback { get; set; }
    }

    public class Candidate
    {
        public Content Content { get; set; }
        public string FinishReason { get; set; }
        public int Index { get; set; }
    }

    public class Content
    {
        public Part[] Parts { get; set; }
        public string Role { get; set; }
    }

    public class Part
    {
        public string Text { get; set; }
    }

    public class PromptFeedback
    {
        public SafetyRating[] SafetyRatings { get; set; }
    }

    public class SafetyRating
    {
        public string Category { get; set; }
        public string Probability { get; set; }
    }
}
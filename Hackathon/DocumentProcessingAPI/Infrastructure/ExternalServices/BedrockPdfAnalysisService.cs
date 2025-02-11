// using Amazon.BedrockRuntime;
// using Amazon.BedrockRuntime.Model;
// using UglyToad.PdfPig;
// using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;
// using System.Text.Json;
// using System.Diagnostics;
// using Features.DocumentProcessing.Domain.Models;
// using System.Text;

// namespace DocumentProcessingAPI.Infrastructure.Services
// {
//     public class BedrockPdfAnalysisService : IPdfAnalysisService
//     {
//         private readonly IAmazonBedrockRuntime _bedrockClient;
//         private const string MODEL_ID = "anthropic.claude-instant-v1";

//         public BedrockPdfAnalysisService(IAmazonBedrockRuntime bedrockClient)
//         {
//             _bedrockClient = bedrockClient;
//         }

//         public Task<AnalysisResult> AnalyzeDocumentAsync(Stream pdfStream)
//         {
//             // Use default options
//             var defaultOptions = new AnalysisOptions();
//             return AnalyzeDocumentAsync(pdfStream, defaultOptions);
//         }

//         public async Task<AnalysisResult> AnalyzeDocumentAsync(Stream pdfStream, AnalysisOptions options)
//         {
//             var stopwatch = Stopwatch.StartNew();
//             try
//             {
//                 // Extract text and metadata from PDF
//                 var (extractedText, metadata) = await ExtractTextAndMetadataAsync(pdfStream, options.IncludePageNumbers);

//                 // Get appropriate prompt based on analysis type
//                 var prompt = GetPromptForAnalysisType(extractedText, options.AnalysisType);

//                 // Create Bedrock request
//                 var request = new InvokeModelRequest
//                 {
//                     ModelId = MODEL_ID,
//                     Body = new MemoryStream(JsonSerializer.SerializeToUtf8Bytes(new
//                     {
//                         prompt = prompt,
//                         max_tokens = options.MaxTokens,
//                         temperature = options.Temperature
//                     }))
//                 };

//                 // Invoke Bedrock
//                 var response = await _bedrockClient.InvokeModelAsync(request);

//                 // Process response
//                 using var streamReader = new StreamReader(response.Body);
//                 var responseText = await streamReader.ReadToEndAsync();
//                 var bedrockResponse = JsonSerializer.Deserialize<BedrockResponse>(responseText);

//                 stopwatch.Stop();

//                 return new AnalysisResult
//                 {
//                     Success = true,
//                     Summary = ExtractRelevantContent(bedrockResponse.completion, options.AnalysisType),
//                     Metadata = metadata,
//                     ProcessingTimeMs = stopwatch.ElapsedMilliseconds,
//                     AnalysisTimestamp = DateTimeOffset.UtcNow,
//                     ModelUsed = MODEL_ID,
//                     TokensProcessed = CalculateTokenCount(extractedText)
//                 };
//             }
//             catch (Exception ex)
//             {
//                 stopwatch.Stop();
//                 return new AnalysisResult
//                 {
//                     Success = false,
//                     ErrorMessage = $"Analysis failed: {ex.Message}",
//                     ProcessingTimeMs = stopwatch.ElapsedMilliseconds,
//                     AnalysisTimestamp = DateTimeOffset.UtcNow,
//                     ModelUsed = MODEL_ID
//                 };
//             }
//         }

//         private async Task<(string Text, DocumentMetadata Metadata)> ExtractTextAndMetadataAsync(Stream pdfStream, bool includePageNumbers)
//         {
//             var memoryStream = new MemoryStream();
//             await pdfStream.CopyToAsync(memoryStream);
//             memoryStream.Position = 0;

//             var textBuilder = new StringBuilder();
//             var metadata = new DocumentMetadata();

//             using (var document = PdfDocument.Open(memoryStream))
//             {
//                 metadata.PageCount = document.NumberOfPages;
//                 metadata.DocumentSizeBytes = memoryStream.Length;

//                 if (document.Information.Title != null)
//                     metadata.Title = document.Information.Title;
//                 if (document.Information.Author != null)
//                     metadata.Author = document.Information.Author;


//                 for (int i = 0; i < document.NumberOfPages; i++)
//                 {
//                     var page = document.GetPage(i + 1);
//                     var pageText = ContentOrderTextExtractor.GetText(page);

//                     if (includePageNumbers)
//                         textBuilder.AppendLine($"[Page {i + 1}]");

//                     textBuilder.AppendLine(pageText);
//                     textBuilder.AppendLine();
//                 }
//             }

//             return (textBuilder.ToString(), metadata);
//         }

//         private string GetPromptForAnalysisType(string text, AnalysisType analysisType)
//         {
//             return analysisType switch
//             {
//                 AnalysisType.Summary =>
//                     $"Please provide a concise summary of the following document: {text}",

//                 AnalysisType.KeyPoints =>
//                     $"Please extract and list the key points from the following document: {text}",

//                 AnalysisType.FullAnalysis =>
//                     @$"Please provide a comprehensive analysis of the following document including:
//                     1. A detailed summary
//                     2. Key main points
//                     3. The primary language used
//                     4. Important topics and keywords
//                     5. Any notable findings or conclusions

//                     Document text: {text}",

//                 _ => throw new ArgumentException($"Unsupported analysis type: {analysisType}")
//             };
//         }

//         private string ExtractRelevantContent(string completion, AnalysisType analysisType)
//         {
//             // For Summary and KeyPoints, return the completion as is
//             if (analysisType != AnalysisType.FullAnalysis)
//                 return completion;

//             // For FullAnalysis, try to structure the response
//             try
//             {
//                 var sections = completion.Split('\n', StringSplitOptions.RemoveEmptyEntries);
//                 var structuredResponse = new StringBuilder();

//                 foreach (var section in sections)
//                 {
//                     if (section.StartsWith("1.") ||
//                         section.StartsWith("2.") ||
//                         section.StartsWith("3.") ||
//                         section.StartsWith("4.") ||
//                         section.StartsWith("5."))
//                     {
//                         structuredResponse.AppendLine();
//                         structuredResponse.AppendLine(section.Trim());
//                     }
//                     else
//                     {
//                         structuredResponse.AppendLine(section.Trim());
//                     }
//                 }

//                 return structuredResponse.ToString().Trim();
//             }
//             catch
//             {
//                 // If structured parsing fails, return the original completion
//                 return completion;
//             }
//         }

//         private int CalculateTokenCount(string text)
//         {
//             // Simple approximation: average English words are about 4 characters
//             // and most tokenizers create about 1.3 tokens per word
//             return (int)(text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length * 1.3);
//         }
//     }

//     internal class BedrockResponse
//     {
//         public string completion { get; set; }
//     }
// }
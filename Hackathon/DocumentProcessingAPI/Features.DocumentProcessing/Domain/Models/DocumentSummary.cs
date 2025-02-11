using System.Collections.Generic;

namespace Features.DocumentProcessing.Domain
{
    public class DocumentSummary
    {
        public string DocumentId { get; set; }
        public string Summary { get; set; }
        public TimeSpan ProcessingTime { get; set; }
        public DateTimeOffset AnalysisTimestamp { get; set; }
    }
}

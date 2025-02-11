namespace Features.DocumentProcessing.Domain
{
    public class DocumentDiff
    {
        public string DocumentId { get; set; }
        public string Analysis { get; set; }
        public TimeSpan ProcessingTime { get; set; }
        public DateTimeOffset AnalysisTimestamp { get; set; }
    }
}

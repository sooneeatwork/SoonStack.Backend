using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Features.DocumentProcessing.Domain.Models
{

    public class AnalysisOptions
    {
        public AnalysisType AnalysisType { get; set; }
    }

    public enum AnalysisType
    {
        Summary,
        KeyPoints,
        FullAnalysis
    }

    public class AnalysisResult
    {
        public bool Success { get; set; }
        public string Summary { get; set; }
        public string ErrorMessage { get; set; }
        public long ProcessingTimeMs { get; set; }
        public DateTimeOffset AnalysisTimestamp { get; set; }
        public string ModelUsed { get; set; }
        public int CharacterCount { get; set; }
        public AnalysisType AnalysisType { get; set; }
    }

}
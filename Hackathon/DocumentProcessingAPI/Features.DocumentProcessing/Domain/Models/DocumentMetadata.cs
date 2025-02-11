using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Features.DocumentProcessing.Domain.Models
{
    public class DocumentMetadata
    {
        /// <summary>
        /// Total number of pages in the document
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Document title if available
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Document author if available
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Creation date of the document if available
        /// </summary>
        public DateTimeOffset? CreationDate { get; set; }

        /// <summary>
        /// Size of the document in bytes
        /// </summary>
        public long DocumentSizeBytes { get; set; }

        /// <summary>
        /// The detected language of the document
        /// </summary>
        public string DetectedLanguage { get; set; }

        /// <summary>
        /// Document keywords or tags if available
        /// </summary>
        public List<string> Keywords { get; set; } = new List<string>();
    }
}
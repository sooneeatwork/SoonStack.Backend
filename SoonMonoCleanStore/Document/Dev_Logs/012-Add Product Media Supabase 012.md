---

## Log 012: Add Product Media Supabase 012

---
Development Notes:

Now, I will proceed to develop the product media upload module. Firstly, I need to determine a suitable storage solution for my media. I have three options:


**1. Storing in a Database as a Blob**
Pros:

Centralization: Media files are stored alongside your other data, which can simplify backup and recovery processes.
Security: Databases often offer robust security features, making it easier to control access to your media files.
Data Integrity: Storing files in the database can ensure data integrity and transactions, including media files, are atomic, consistent, isolated, and durable (ACID properties).

Cons:

Performance: Large blobs can significantly increase the size of your database, potentially degrading performance for both the database and your application.
Scalability: As the database grows, the cost and complexity of scaling (vertically or horizontally) can become prohibitive.
Backup and Recovery: Larger databases result in longer backup and recovery times, which could lead to longer downtimes in case of failure.
https://dba.stackexchange.com/questions/2445/should-binary-files-be-stored-in-the-database

**2. Storing on the Web Server's File System**

Pros:

Simplicity: Implementing file storage on a web server's file system is relatively straightforward and doesn't require much setup.
Performance: Accessing files from the filesystem is generally faster than retrieving them from a database blob, especially for large files.
Direct Access: Files can be served directly to clients without the need for database queries, reducing load on your database.

Cons:

Scalability: Requires manual scaling of storage and can be costly to expand disk space on existing servers.
Management Complexity: Managing file permissions, organizing files, and ensuring security can become complex as your application grows.
Reliability: Storing files on a single server or within a single data center can pose risks in terms of data loss and availability. Implementing redundancy requires additional infrastructure and complexity.

**3. Utilizing Cloud Services (e.g., Supabase,firebase)**
Pros:

Scalability: Cloud storage solutions are designed to scale easily, allowing you to store as much data as needed without worrying about physical hardware limitations.
Reliability and Availability: Cloud providers typically offer high durability and availability, with data replicated across multiple locations.
Cost-Effective: You pay only for the storage you use, and there's no need to invest in physical hardware. Additionally, many cloud providers offer a tiered pricing model that can be cost-effective for startups and scale with your usage.
Cons:

Dependency on Internet Connectivity: Access to your files depends on internet connectivity. Slow or unreliable internet connections can impact access times and availability.
Ongoing Costs: While cloud services can be cost-effective initially, ongoing costs can grow with your usage, especially if your application serves a large number of media files frequently.
Complexity: Using cloud storage introduces complexity in terms of API usage, understanding pricing models, and implementing security and access controls.



After careful consideration, I have chosen Supabase as my preferred option because for a side project, 
the best approach often balances ease of implementation, cost, scalability, and maintenance effort. 
Considering these factors, utilizing cloud services like Supabase for storing my media files is likely the most advantageous approach for several reasons:

Ease of Setup and Use: Cloud services are designed to be user-friendly, offering straightforward setup processes. Supabase, in particular, provides a simple and intuitive interface for storage operations, making it accessible even to developers who might not have extensive experience with cloud storage solutions.

Scalability: One of the significant advantages of cloud services is their scalability. As your side project grows, cloud services can easily accommodate increasing storage needs without requiring manual intervention or significant reconfiguration.

Cost-Effectiveness: For side projects, especially those in their initial stages, keeping costs low is often a priority. Cloud services like Supabase offer free tiers and pay-as-you-go pricing models that can be very cost-effective for small projects.

Maintenance and Security: Managing server infrastructure and ensuring data security can be challenging, especially for individuals or small teams working on side projects. By using a cloud service, i offload these responsibilities to the provider, who takes care of server maintenance, security patches, and data redundancy.

Accessibility: Cloud storage makes my media files accessible from anywhere, facilitating easier collaboration and testing across different locations and devices. This is particularly beneficial if i am working on a web or mobile application that requires frequent access to media content.

Focus on Development: With the storage concerns handled by your cloud provider, i can focus more on developing and refining my project's core functionality, rather than getting bogged down by infrastructure management tasks.

This feature introduces a direct interaction with Supabase Storage, abstracting file uploads behind our SupabaseStorageService.
We're utilizing an IStreamProcessor for stream-to-byte conversion, ensuring flexibility in handling different stream sources.
Exception handling is built into the process, aiming to provide clear error messaging and facilitate troubleshooting.
The integration demonstrates a practical application of external services within our infrastructure layer, aligning with our clean architecture principles. 
So that we can switch to different provider without affecting the core business logic layer


**Use Case Code**
```csharp
using Infrastructure.StreamLibrary;


namespace Infrastructure.MediaStorage
{
    public class SupabaseStorageService : IMediaStorageServices
    {

        private readonly Supabase.Client _supabaseClient;
        private readonly IStreamProcessor _streamProcessor;

        public SupabaseStorageService(Supabase.Client supabaseClient, IStreamProcessor streamProcessor)
        {
            _supabaseClient = supabaseClient;
            _streamProcessor = streamProcessor;
        }

        public async Task<string> UploadFileAsync(string bucketName,
                                                  string fileName, 
                                                  Stream fileContent,
                                                  string contentType)
        {
            if (string.IsNullOrWhiteSpace(bucketName)) throw new ArgumentException("Bucket name must be provided.", nameof(bucketName));
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("File name must be provided.", nameof(fileName));
            if (fileContent == null) throw new ArgumentNullException(nameof(fileContent));
            if (string.IsNullOrWhiteSpace(contentType)) throw new ArgumentException("Content type must be provided.", nameof(contentType));
            string uploadResult = string.Empty;

            try
            {
                var storage = _supabaseClient.Storage.From(bucketName);
                var fileContentByte = await _streamProcessor.ConvertStreamToByteArray(fileContent);

                // Pseudo-code: Replace with actual method signature and parameters based on the library documentation.
                uploadResult = await storage.Upload(fileContentByte,
                                                       fileName,
                                                       new Supabase.Storage.FileOptions { ContentType = contentType })
                                                   .ConfigureAwait(false);
            }
            catch (Exception){ throw; }

            return uploadResult;
        }

     

    }

}

```
Next Steps:

Further testing with different file sizes and formats to ensure robustness and efficiency.
Explore implementing additional features like file deletion, retrieval, and metadata management in the SupabaseStorageService.
Consider security enhancements, particularly around access control and encryption, to bolster our file storage strategy.
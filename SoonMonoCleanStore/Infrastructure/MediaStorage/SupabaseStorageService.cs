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

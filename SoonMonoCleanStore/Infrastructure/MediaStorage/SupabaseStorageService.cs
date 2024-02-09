using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MediaStorage
{
    public class SupabaseStorageService : IMediaStorageServices
    {
        private readonly Supabase.Client _supabaseClient;

        public SupabaseStorageService(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public async Task<string> UploadFileAsync(string bucketName, string fileName, Stream fileContent, string contentType)
        {
            var storage = _supabaseClient.Storage.From(bucketName);
            var uploadResult = await storage.UploadAsync(fileName, fileContent, contentType);

            if (uploadResult.Error != null)
            {
                throw new Exception(uploadResult.Error.Message);
            }

            return uploadResult.Data.PublicUrl;
        }
    }

}

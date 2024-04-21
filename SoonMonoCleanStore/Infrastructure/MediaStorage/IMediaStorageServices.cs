using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MediaStorage
{
    public interface IMediaStorageServices
    {
        Task<string> UploadFileAsync(string bucketName, string fileName, Stream fileContent, string contentType);
    }

   

}

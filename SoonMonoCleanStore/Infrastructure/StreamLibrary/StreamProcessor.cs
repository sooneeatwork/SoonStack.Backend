using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.StreamLibrary
{
    public class StreamProcessor : IStreamProcessor
    {

        public async Task<byte[]> ConvertStreamToByteArray(Stream stream) 
        {
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();

        }
    }
}

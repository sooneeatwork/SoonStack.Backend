using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.StreamLibrary
{
    public interface IStreamProcessor
    {
        Task<byte[]> ConvertStreamToByteArray(Stream stream);
    }
}

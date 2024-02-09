using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgmtSlices.Domain
{
    public class ProductMedia
    {
        public long ProductId { get; private set; }
        public string MediaUrl { get; private set; } = string.Empty;
        public string MediaType { get; private set; } = string.Empty;

        internal static ProductMedia CreateProductMedia(long productId,
                                                        string mediaUrl,
                                                        string contentType)
        {
            ProductMedia media = new ProductMedia();
            media.ProductId = productId;
            media.MediaUrl = mediaUrl;
            media.MediaType = contentType;
            return media;
        }
    }
}

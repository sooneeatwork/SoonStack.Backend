using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core
{
    public class BaseEntity
    {
        public long CreatedBy { get; set; } = 1;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public long ModifiedBy { get; set; } = 1;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}

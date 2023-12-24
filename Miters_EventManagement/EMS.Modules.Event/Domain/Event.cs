
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace EMS.Modules.EventMgmt.Domain
{
    public class Event
    {
       public long Id { get; set; }

       public DateTime EventDate { get; set; }
    }
}

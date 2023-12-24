using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.UseCases.Application.TicketModule.Query.GetTicketDiscount
{
    public record TicketDiscountResponse
    (
        long Id = 0,
        decimal BasePrice = 0M,
        decimal FinalAmount = 0M,
        decimal DiscountAmount = 0M
    );

}

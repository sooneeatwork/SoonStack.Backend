using AutoMapper;
using EMS.Core.TicketMgmt;
using EMS.UseCases.Application.TicketModule.Query.GetTicketDiscount;

namespace EMS.UseCases.TicketMgmt.Application.TicketModule.MappingProfile
{


    public class TicketDiscountMappingProfile : Profile
    {
        public TicketDiscountMappingProfile()
        {
            CreateMap<TicketDiscount, TicketDiscountResponse>().ReverseMap();
            // Add other mappings as needed...
        }
    }

}

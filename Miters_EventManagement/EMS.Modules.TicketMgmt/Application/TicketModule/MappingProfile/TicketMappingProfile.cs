using AutoMapper;
using EMS.Core.TicketMgmt;

namespace EMS.UseCases.TicketMgmt.Application.TicketModule.MappingProfile
{


    public class TicketMappingProfile : Profile
    {
        public TicketMappingProfile()
        {
            CreateMap<Ticket, TicketResponse>().ReverseMap();
            // Add other mappings as needed...
        }
    }

}

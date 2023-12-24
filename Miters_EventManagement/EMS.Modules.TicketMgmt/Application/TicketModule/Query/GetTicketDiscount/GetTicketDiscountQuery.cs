using AutoMapper;
using EMS.Core.TicketMgmt;
using EMS.Shared.Application.Wrapper;
using EMS.UseCases.TicketMgmt.Application.TicketModule.RepositoryInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.UseCases.Application.TicketModule.Query.GetTicketDiscount
{
    public class GetTicketDiscountQuery : IRequest<Result<TicketDiscountResponse>>
    {
        public long TicketDiscountId { get; set; }
    }


    public class GetTicketDiscountQueryHandler : IRequestHandler<GetTicketDiscountQuery, Result<TicketDiscountResponse>>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public GetTicketDiscountQueryHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<Result<TicketDiscountResponse>> Handle(GetTicketDiscountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var ticket = await _ticketRepository.GetByIdAsync<TicketDiscount>(request.TicketDiscountId);

                if (ticket == null)
                    throw new InvalidOperationException("Ticket discount not found.");

                var ticketDiscountResponse = _mapper.Map<TicketDiscountResponse>(ticket);
                return Result<TicketDiscountResponse>.Success(ticketDiscountResponse);
            }
            catch (Exception ex)
            {
                return Result<TicketDiscountResponse>.Fail(ex.Message);
            }
        }
    }

}

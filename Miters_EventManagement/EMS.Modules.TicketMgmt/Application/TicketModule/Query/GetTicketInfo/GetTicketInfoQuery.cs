using AutoMapper;
using EMS.Core.TicketMgmt;
using EMS.UseCases.TicketMgmt.Application.TicketModule.RepositoryInterfaces;
using EMS.Shared.Application.Wrapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.UseCases.TicketMgmt.Application.TicketModule.Query.GetTicketInfo
{
    public class GetTicketInfoQuery : IRequest<Result<TicketResponse>>
    {
        public string TicketCode { get; set; } = string.Empty;
    }

    public class GetTicketInfoQueryHandler : IRequestHandler<GetTicketInfoQuery, Result<TicketResponse>>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public GetTicketInfoQueryHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<Result<TicketResponse>> Handle(GetTicketInfoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var ticket = await _ticketRepository.GetByCodeAsync<Ticket>(request.TicketCode);

                if (ticket == null)
                    throw new InvalidOperationException("Ticket not found.");

                var ticketResponse = _mapper.Map<TicketResponse>(ticket);
                return Result<TicketResponse>.Success(ticketResponse);
            }
            catch (Exception ex)
            {
                return Result<TicketResponse>.Fail(ex.Message);
            }
        }
    }
}

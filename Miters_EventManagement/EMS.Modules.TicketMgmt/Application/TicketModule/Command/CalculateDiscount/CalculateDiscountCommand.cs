using AutoMapper;
using EMS.Core.TicketMgmt;
using EMS.Core.TicketMgmt.DomainServices;
using EMS.UseCases.TicketMgmt.Application.TicketModule.RepositoryInterfaces;
using EMS.Shared.Application.Wrapper;

using EMS.Shared.Persistance;
using EMS.Shared.Promotion;
using MediatR;


namespace EMS.UseCases.TicketMgmt.Application.TicketModule.Command.CalculateDiscount
{
    public class CalculateDiscountCommand : IRequest<Result<long>>
    {
        public long CustomerId { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public int TicketQuantity { get; set; }
    }
    public class CalculateDiscountCommandHandler : IRequestHandler<CalculateDiscountCommand,
                                                                    Result<long>>
    {

        private readonly ITicketPurchaseHistoryRepository _ticketPurchaseRepository;
        private readonly IDiscountService _discountService;
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CalculateDiscountCommandHandler(IBaseRepository baseRepo,
                                               ITicketPurchaseHistoryRepository ticketPurchaseRepository,
                                               IDiscountService discountService,
                                               IUnitOfWork unitOfWork,
                                               IMapper mapper)
        {
            _ticketPurchaseRepository = ticketPurchaseRepository;
            _baseRepository = baseRepo;
            _discountService = discountService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<long>> Handle(CalculateDiscountCommand request,
                                               CancellationToken cancellationToken)
        {
            var result = Result<long>.Fail();
            try
            {
                var ticketInfo = await _baseRepository.GetByCodeAsync<Ticket>(request.TicketCode);
                if (ticketInfo == null)
                    throw new InvalidOperationException("Ticket not found.");

                var discountPercentage = await CalculateDiscountAmt(request, ticketInfo.BasePrice);
                await InsertDiscountInfo(ticketInfo, discountPercentage);
                var insertedId = await _baseRepository.SelectMaxIDAsync<TicketDiscount>();


                result =  Result<long>.Success(insertedId);
            }
            catch (ArgumentNullException ex)
            {
                result =  Result<long>.Fail(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                result = Result<long>.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                result = Result<long>.Fail(ex.Message);
            }

            return result;
        }

        private async Task<decimal> CalculateDiscountAmt(CalculateDiscountCommand request, decimal ticketBasePrice)
        {


            var customerPurchasedTicketCount = await _ticketPurchaseRepository.GetCustomerPurchasedTicketCount(request.CustomerId);

            var discountCriteria = DiscountCriteria.Create(customerPurchasedTicketCount,
                                                           request.TicketQuantity,
                                                           ticketBasePrice);

            return _discountService.CalculateTotalDiscountAmt(discountCriteria);
        }

        private async Task<long> InsertDiscountInfo(Ticket ticketInfo, decimal discountAmt)
        {


            long result = 0;
            var ticketDiscountInfo = TicketDiscount.ApplyDiscount(ticketInfo.Id,ticketInfo.BasePrice, discountAmt);

            using (var trans = _unitOfWork.BeginTransaction())
            {
                try
                {
                    result = await _baseRepository.InsertAsync(ticketDiscountInfo, trans);
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return result;
        }
    }
}




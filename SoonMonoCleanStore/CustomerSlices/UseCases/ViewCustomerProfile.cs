using Core.Domain.RepoInterface;
using Core.Domain.ValueObject;
using Core.UseCases.MapperInterface;
using Core.UseCases.Wrapper;

namespace CustomerSlices.UseCases
{
    public class ViewCustomerProfileQuery : IRequest<Result<CustomerProfileResponse>>
    {
        public int CustomerId { get; set; }
    }

    public class CustomerProfileResponse
    {
        public int Id { get; set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public DateTime DateOfBirth { get; private set; }
        public Address? BillingAddress { get; private set; }
    }

    public class ViewCustomerProfileHandler : IRequestHandler<ViewCustomerProfileQuery, Result<CustomerProfileResponse>>
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ViewCustomerProfileHandler(IGenericRepository genericRepository,
                                          IMapper mapper,
                                          ILogger logger)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<CustomerProfileResponse>> Handle(ViewCustomerProfileQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.CustomerId);
            Result<CustomerProfileResponse> result;

            try
            {
                var customer = await _genericRepository.GetByIdAsync<CustomersTable>(request.CustomerId);

                if (customer == null)
                    return Result<CustomerProfileResponse>.Failure($"Customer with ID {request.CustomerId} not found.");

                var response = _mapper.Map<CustomersTable, CustomerProfileResponse>(customer);
                result = Result<CustomerProfileResponse>.Success(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(ViewCustomerProfileQuery), ex);
                result =  Result<CustomerProfileResponse>.Failure("Failed to view customer profile");
            }

            return result;
        }
    }
}

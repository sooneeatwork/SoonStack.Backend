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
        public ViewCustomerProfileHandler(IGenericRepository genericRepository,
                                          IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Result<CustomerProfileResponse>> Handle(ViewCustomerProfileQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.CustomerId);

            var customer = await _genericRepository.GetByIdAsync<CustomersTable>(request.CustomerId);

            if (customer == null)
                return Result<CustomerProfileResponse>.Failure($"Customer with ID {request.CustomerId} not found.");

            var result = _mapper.Map<CustomersTable, CustomerProfileResponse>(customer);
            return Result<CustomerProfileResponse>.Success(result);
        }
    }
}

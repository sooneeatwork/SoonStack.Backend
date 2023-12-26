namespace CustomerSlices.UseCases
{
    public record EditCustomerProfileCommand(
        int CustomerId,
        string NewName,
        string NewEmail,
        DateTime NewDateOfBirth,
        Address NewBillingAddress) : IRequest<Result<int>>;



    public class EditCustomerProfileHandler : IRequestHandler<EditCustomerProfileCommand, Result<int>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerTableMappers _customerTableMapper;
        private readonly IMapper _mapper;

        public EditCustomerProfileHandler(ICustomerRepository customerRepository,
                                          IMapper mapper, 
                                          ICustomerTableMappers customerTableMappers)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _customerTableMapper = customerTableMappers;
        }

        public async Task<Result<int>> Handle(EditCustomerProfileCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.CustomerId);

            var customerData = await _customerRepository.GetByIdAsync<CustomersTable>(request.CustomerId);

            if (customerData == null)
                return Result<int>.Failure($"Customer with ID {request.CustomerId} not found.");

            var customer = _mapper.Map<CustomersTable, Customer>(customerData);
            // Update customer profile information based on the request
            customer.UpdateCustomerInfo(request.NewName,
                                        request.NewDateOfBirth,
                                        request.NewBillingAddress,
                                        request.NewEmail);

            // Save the updated customer information
           var (dataFileds, whereClause) =  _customerTableMapper.CreateMapForUpdate(customer, customerData);
            int rowsAffected = await _customerRepository.UpdateOneAsync<CustomersTable>(dataFileds, whereClause);

            return Result<int>.Success(rowsAffected);
        }
    }
}





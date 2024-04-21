﻿

using SharedKernel.Domain.DomainModel.CustomerModel;

namespace CustomerSlices.UseCases
{
    public record AddNewCustomerProfileCommand(
      string NewName,
      string NewEmail,
      DateTime NewDateOfBirth,
      Address NewBillingAddress) : IRequest<Result<long>>;



    public class AddNewCustomerProfileHandler : IRequestHandler<AddNewCustomerProfileCommand, Result<long>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerTableMappers _customerTableMapper;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AddNewCustomerProfileHandler(ICustomerRepository customerRepository,  
                                            IMapper mapper,
                                            ICustomerTableMappers customerTableMappers,
                                            ILogger logger)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _customerTableMapper = customerTableMappers;
            _logger = logger;
        }

        public async Task<Result<long>> Handle(AddNewCustomerProfileCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            long newId = 0;
            try 
            {
                var customerCount = await _customerRepository.GetCountByEmailAsync(request.NewEmail);

                if (Customer.IsCustomerExists(customerCount))
                    return Result<long>.Failure($"Customer with Email {request.NewEmail} existed.");

                // Update customer profile information based on the request
                var newCustomer = Customer.CreateCustomer(request.NewName,
                                                          request.NewEmail,
                                                          request.NewBillingAddress,
                                                          request.NewDateOfBirth);

                // Save the updated customer information
                var customerTableData = _customerTableMapper.CreateMapForInsert(newCustomer);
                 newId = await _customerRepository.InsertOneGetIdAsync<CustomersTable>(customerTableData);
            }
            catch(Exception ex)
            {
                _logger.LogError(nameof(AddNewCustomerProfileCommand), ex);
                throw;
            }

           

            return Result<long>.Success(newId);
        }
    }
}

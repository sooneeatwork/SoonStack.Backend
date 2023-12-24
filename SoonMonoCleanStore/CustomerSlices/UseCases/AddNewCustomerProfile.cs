using CustomerSlices.Repository.CustomerTableMapper;
using CustomerSlices.Repository.DatabaseModel;
using CustomerSlices.Repository.RepoInterfaces;
using SharedKernel.Domain.RepoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public AddNewCustomerProfileHandler(ICustomerRepository customerRepository,  
                                            IMapper mapper,
                                            ICustomerTableMappers customerTableMappers)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _customerTableMapper = customerTableMappers;
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
            catch
            {
                throw;
            }

           

            return Result<long>.Success(newId);
        }
    }
}

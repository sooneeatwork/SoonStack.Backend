using CustomerSlices.UseCases;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Domain.ValueObject;

namespace RestApi.Controllers.CustomerController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("AddNewCustomerProfile")]
        public async Task<IActionResult> AddNewCustomerProfile([FromBody] AddNewCustomerProfileCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if (result.IsSuccess)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, @$"An error occurred while processing your request.
                                          Exception message --> {ex.Message}
                                          Inner Exception --> {ex.InnerException}
                                          Stack Trace --> {ex.StackTrace}");
            }
        }

        [HttpPut]
        [Route("EditCustomerProfile")]
        public async Task<IActionResult> EditCustomerProfile([FromBody] EditCustomerProfileCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if (result.IsSuccess)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, @$"An error occurred while processing your request.
                                          Exception message --> {ex.Message}
                                          Inner Exception --> {ex.InnerException}
                                          Stack Trace --> {ex.StackTrace}");
            }
        }

        [HttpGet]
        [Route("ViewCustomerProfile/{customerId}")]
        public async Task<IActionResult> ViewCustomerProfile(int customerId)
        {
            try
            {
                var query = new ViewCustomerProfileQuery { CustomerId = customerId };
                var result = await _mediator.Send(query);

                if (result.IsSuccess)
                    return Ok(result);
                else
                    return NotFound(result);
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, @$"An error occurred while processing your request.
                                          Exception message --> {ex.Message}
                                          Inner Exception --> {ex.InnerException}
                                          Stack Trace --> {ex.StackTrace}");
            }
        }
    }

 
}

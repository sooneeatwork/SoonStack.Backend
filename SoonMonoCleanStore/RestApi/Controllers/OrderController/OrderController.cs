using OrderSlices.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.Ocsp;

namespace RestApi.Controllers.OrderController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderCommand command)
        {
            try
            {

                var result = await _mediator.Send(command);

                if (result.IsSuccess)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("GetOrderDetails/{orderId}")]
        public async Task<IActionResult> GetOrderDetails(long orderId)
        {
            try
            {
                var query = new GetOrderDetailsQuery(orderId);
                var result = await _mediator.Send(query);

                if (result.IsSuccess)
                    return Ok(result);
                else
                    return NotFound(result);

            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
            }
        }
    }


}

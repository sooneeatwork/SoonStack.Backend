using EMS.UseCases.Application.TicketModule.Query.GetTicketDiscount;
using EMS.UseCases.TicketMgmt.Application.TicketModule.Command.CalculateDiscount;
using EMS.UseCases.TicketMgmt.Application.TicketModule.Query.GetTicketInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    // TicketsController
    [ApiController]
    [Route("api/ticketsDiscountCalculator")]
    public class TicketsDiscountCalculatorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketsDiscountCalculatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Calculate_Discount")]
        public async Task<IActionResult> ApplyDiscount(CalculateDiscountCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)  // Assuming the result has an IsSuccess property
            {
                return Ok(result.Value);  // Assuming the successful result is in the Data property
            }
            //else if (result.)  // Assuming the result can have validation errors
            //{
            //    return BadRequest(result.ValidationErrors);
            //}
            else
            {
                return StatusCode(500, result.ErrorMessage);  // Assuming an Error property for unexpected issues
            }
        }

        [HttpGet("Check_Ticket")]
        public async Task<IActionResult> GetTicketInfo(string ticketCode)
        {
            var query = new GetTicketInfoQuery { TicketCode = ticketCode };
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return StatusCode(500, result.ErrorMessage);
            }
        }

        [HttpGet("Check_Ticket_Discount")]
        public async Task<IActionResult> GetTicketDiscountInfo(long ticketDiscountId)
        {
            var query = new GetTicketDiscountQuery { TicketDiscountId = ticketDiscountId };
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return StatusCode(500, result.ErrorMessage);
            }
        }


        // ... other ticket-related actions ...
    }
}

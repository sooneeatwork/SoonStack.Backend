using ProductSlices.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace RestApi.Controllers.ProductController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductCommand command)
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
        [Route("SearchAllProducts")]
        public async Task<IActionResult> SearchAllProducts()
        {
            try
            {
                var result = await _mediator.Send(new SearchAllProductQuery());

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

        [HttpGet("{id}")]
        public async Task<IActionResult> SearchProductById(long id)
        {
            try
            {
                var query = new SearchProductByIdQuery(id);
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

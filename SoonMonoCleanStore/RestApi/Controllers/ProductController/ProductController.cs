using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductMgmtSlices.UseCases;

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


        [HttpPut]
        [Route("EditProduct")]
        public async Task<IActionResult> EditProduct([FromBody] EditProductCommand command)
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
        [Route("SearchProducts")]
        public async Task<IActionResult> SearchAllProducts([FromQuery]SearchProductsQuery searchProductsQuery)
        {
            try
            {
                var result = await _mediator.Send(new SearchProductsQuery(searchProductsQuery.ProductName,
                                                                          searchProductsQuery.MinPrice,
                                                                          searchProductsQuery.MaxPrice,
                                                                          searchProductsQuery.PageNumber,
                                                                          searchProductsQuery.PageSize));

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

        //[HttpGet("{id}")]
        //public async Task<IActionResult> SearchProductById(long id)
        //{
        //    try
        //    {
        //        var query = new SearchProductByIdQuery(id);
        //        var result = await _mediator.Send(query);

        //        if (result.IsSuccess)
        //            return Ok(result);
        //        else
        //            return NotFound(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception details
        //        return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
        //    }
        //}
    }

}

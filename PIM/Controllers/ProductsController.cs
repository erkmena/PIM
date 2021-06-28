using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PIM.Model;
using PIM.Model.RequestModels;
using PIM.Model.Responses;
using PIM.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIM.Controllers
{
    [Route("api/brands/")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Route("product/Add")]
        [ProducesResponseType(typeof(AddProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(ProductAddRequestModel productRequestModel)
        {
            var response = await _productService.AddProductAsync(productRequestModel);

            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Data);
        }

        [HttpGet]
        [Route("product/{productId}")]
        [ProducesResponseType(typeof(GetProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromRoute] int productId)
        {
            var response = await _productService.GetProductAsync(productId);

            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }
            if (response.Data == null)
            {
                return NotFound();
            }

            return Ok(response.Data);
        }

        [HttpPut]
        [Route("product/Update")]
        [ProducesResponseType(typeof(AddProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(ProductUpdateRequestModel productRequest)
        {
            var response = await _productService.UpdateProductAsync(productRequest);

            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }
            if (!response.Data.IsSuccess)
            {
                return NotFound();
            }
            return Ok(response.Data);
        }
        [HttpDelete]
        [Route("product/{productId}")]
        [ProducesResponseType(typeof(AddProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Archive(int productId)
        {
            var response = await _productService.ArchiveProductAsync(productId);

            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }
            if(!response.Data.IsSuccess)
            {
                return NotFound();
            }

            return Ok(response.Data);
        }
    }
}

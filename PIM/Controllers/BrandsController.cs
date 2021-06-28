using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PIM.Model;
using PIM.Model.Responses;
using PIM.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIM.Controllers
{
    [Route("api/brands/")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet]
        [Route("brand/{brandId}/products")]
        [ProducesResponseType(typeof(GetBrandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBrand([FromRoute] int brandId)
        {
            var response = await _brandService.GetBrandAsync(brandId);

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
    }
}

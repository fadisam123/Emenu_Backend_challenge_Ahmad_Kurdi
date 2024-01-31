using Application.Services.Product;
using Application.Services.ProductVariant;
using DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace Emenu_Backend_challenge_Ahmad_Kurdi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariantsController : Controller
    {
        #region Constructor & Properies

        private readonly IProductVariantService _productVariantService;

        public ProductVariantsController(IProductVariantService productVariantService)
        {
            _productVariantService = productVariantService;
        }
        #endregion

        #region Get Methods

        [HttpGet("{parentProductId:guid}")]
        public async Task<IActionResult> GetVariants([FromQuery] PaginationInputDto pagination, String parentProductId)
        {
            Guid guidId;
            if (!Guid.TryParse(parentProductId, out guidId))
                return BadRequest();

            ProductDto? parent = await _productVariantService.GetParentAsync(guidId);
            if (parent == null)
                return NotFound();

            PaginationOutputDto<ProductVariantDto> result = await _productVariantService.GetAllVariantsAsync(pagination, guidId);
            if (!result.Items.Any())
                return NoContent();
            return Ok(result);
        }

        [HttpGet("variant/{id:guid}")]
        public async Task<IActionResult> GetVariant(string id)
        {
            // validate the id
            Guid guidId;
            if (!Guid.TryParse(id, out guidId))
                return BadRequest();

            ProductVariantDto? result = await _productVariantService.GetVariantAsync(guidId);
            if (result == null)
                return NotFound(); // No product with this id

            // product exist
            return Ok(result);
        }

        #endregion

        #region Post Methods

        [HttpPost]
        public async Task<IActionResult> PostVariant(ProductVariantDto productVariant)
        {
            // user input data validations goes here (for simplicity no validation right now)
            var newProductVariant = await _productVariantService.CreateVariantAsync(productVariant);
            return CreatedAtAction(nameof(GetVariant), new { id = newProductVariant.Id }, newProductVariant);
        }

        #endregion

        #region Put Methods

        [HttpPut]
        public async Task<IActionResult> PutProductVariant(ProductVariantDto productVariant)
        {
            // validate the id
            Guid guidId;
            if (!Guid.TryParse(productVariant.Id, out guidId))
                return BadRequest();

            var oldProductVariant = await _productVariantService.GetVariantAsync(guidId);
            if (oldProductVariant == null || oldProductVariant.Id != productVariant.Id)
                return NotFound();

            // other validations goes here

            await _productVariantService.UpdateVariantAsync(productVariant);
            return NoContent();
        }

        #endregion

        #region Delete Methods

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVariant(string id)
        {
            // validate the id
            Guid guidId;
            if (!Guid.TryParse(id, out guidId))
                return BadRequest();

            var oldVariant = await _productVariantService.GetVariantAsync(guidId);
            if (oldVariant == null || oldVariant.Id != id)
                return NotFound();

            // other validations goes here

            await _productVariantService.RemoveVariantAsync(guidId);
            return NoContent();
        }

        #endregion
    }
}

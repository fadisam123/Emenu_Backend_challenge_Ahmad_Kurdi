using Application.Services.Product;
using DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace Emenu_Backend_challenge_Ahmad_Kurdi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region Constructor & Properies

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region Get Methods

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] PaginationInputDto pagination)
        {
            PaginationOutputDto<ProductDto> result = await _productService.GetAllProductsAsync(pagination);

            if (!result.Items.Any())
                return NoContent();
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            // validate the id
            Guid guidId;
            if (!Guid.TryParse(id, out guidId))
                return BadRequest();

            ProductDto? result = await _productService.GetProductAsync(guidId);
            if (result == null)
                return NotFound(); // No product with this id

            // product exist
            return Ok(result);
        }

        #endregion

        #region Post Methods

        [HttpPost]
        public async Task<IActionResult> PostProduct(ProductDto product)
        {
            // user input data validations goes here (for simplicity no validation right now)
            var newProduct = await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
        }

        #endregion

        #region Put Methods

        [HttpPut]
        public async Task<IActionResult> PutProduct(ProductDto product)
        {
            // validate the id
            Guid guidId;
            if (!Guid.TryParse(product.Id, out guidId))
                return BadRequest();

            var oldProduct = await _productService.GetProductAsync(guidId);
            if (oldProduct == null || oldProduct.Id != product.Id)
                return NotFound();

            // other validations goes here

            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        #endregion

        #region Delete Methods

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            // validate the id
            Guid guidId;
            if (!Guid.TryParse(id, out guidId))
                return BadRequest();

            var oldProduct = await _productService.GetProductAsync(guidId);
            if (oldProduct == null || oldProduct.Id != id)
                return NotFound();

            // other validations goes here

            await _productService.RemoveProductAsync(guidId);
            return NoContent();
        }

        #endregion
    }
}

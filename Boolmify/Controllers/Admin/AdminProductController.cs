        using Boolmify.Dtos.Product;
        using Boolmify.Interfaces.ADminRepository;
        using Microsoft.AspNetCore.Authorization;
        using Microsoft.AspNetCore.Mvc;

        namespace Boolmify.Controllers;
        [ApiController]
        [Route("api/Admin/Product")]
        [Authorize(Roles = "Admin")]
        public class AdminProductController: ControllerBase
        {
            private readonly IAdminProductService _productService;

            public AdminProductController(IAdminProductService productService)
            {
                _productService = productService;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllAsync([FromQuery] string? search = null,
                int pageNumbe = 1, int pageSize = 10)
            {
                var   product = await _productService.GetAllAsync(search, pageNumbe, pageSize);
                return Ok(product);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<ProductDto>> GetByIdAsync(int id)
            {
                var product = await _productService.GetByIdAsync(id);
                if(product==null) return NotFound("Product not found");
                return Ok(product);
            }

            [HttpPost]
            public async Task<ActionResult<ProductDto>> CreateProductAsync([FromBody] CreateProductDto dto)
            {
                var product = await _productService.CreateAsync(dto);
                return CreatedAtRoute("GetById",new {id= product.ProductId} ,  product);
            }

            [HttpPut("{id}")]
            public async Task<ActionResult<ProductDto>> UpdateProductAsync(int id ,[FromBody] UpdateProductDto dto)
            {
                var update = await _productService.UpdateAsync(id,dto);
                if(update==null) return NotFound("Product not found");
                return Ok(update);
                
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteProductAsync(int id)
            {
                var delete = await _productService.DeleteAsync(id);
                if(!delete) return NotFound("Product not found");
                return NoContent();
            }

            [HttpPatch("toggle/Active/{id}")]
            public async Task<IActionResult> ToggleActiveAsync(int id , bool isActive)
            {
                var result = await _productService.ToggleActiveAsync(id, isActive);
                if(!result) return NotFound("Product not found");
                return Ok("Product status toggled");
            }

            [HttpPatch("Stock/Set/{id}")]
            public async Task<IActionResult> SetStockAsync(int id, int newquantity)
            {
                var result = await _productService.SetStockAsync(id, newquantity);
                if(!result) return NotFound("Product not found");
                return Ok("Product Stock set");
            }

            [HttpPatch("Stock/AddJust/{id}")]
            public async Task<IActionResult> AddjustStockAsync(int id,[FromQuery] int addquantity)
            {
                var result = await _productService.AddjustStockAsync(id, addquantity);
                if(!result) return NotFound("Product not found");
                return Ok("Product Stock added");
            }

            [HttpPatch("Price/{id}")]
            public async Task<IActionResult> SetPriceAsync(int id, Decimal newprice, decimal? discountprice = null)
            {
                var price = await _productService.UpdatePriceAsync(id, newprice, discountprice);
                if(!price) return NotFound("Product not found");
                return Ok("Product Price set");
            }
            
        }
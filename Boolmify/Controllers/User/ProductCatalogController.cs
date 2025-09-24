    using Boolmify.Dtos.Product;
    using Boolmify.Helper;
    using Boolmify.Interfaces.USerService;
    using Boolmify.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    namespace Boolmify.Controllers.User;
    [ApiController]
    [Route("Api/ProductCatalog")]
    public class ProductCatalogController: ControllerBase
    {
        private readonly IProductCatalogService _productService;

        public ProductCatalogController(IProductCatalogService productService)
        {
            _productService =  productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<QueryObject<ProductDto>>> GetProducts([FromQuery] string? search,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool isAscending = true)
        
        {
            var product = await _productService.GetAllAsync(search , pageNumber ,  pageSize, sortBy, isAscending);
            return Ok(product);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductDto>> GetProduct([FromRoute] int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound(new { message = "Product not found" });
            return Ok(product);
        }
        
        
        


    }
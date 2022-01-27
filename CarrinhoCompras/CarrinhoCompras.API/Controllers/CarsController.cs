using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarrinhoCompras.API.Models;
using CarrinhoCompras.API.Swagger;
using CarrinhoCompras.BLL.Contracts;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CarrinhoCompras.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/products")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductsService _productsService;

        /// <summary>
        /// products db api
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="productsService"></param>
        public ProductController(IMapper mapper, IProductsService productsService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _productsService = productsService ?? throw new ArgumentNullException(nameof(productsService));
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Returns created product</returns>           
        [HttpPost("")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ProductModelExample))]
        [SwaggerResponse((int)HttpStatusCode.Created, Type = typeof(Product), Description = "Returns created product")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> CreateproductAsync([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _productsService.CreateProductAsync(_mapper.Map<BLL.Models.Product>(product));
            return Created($"{result.Id}", _mapper.Map<Product>(result));
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">product Id</param>
        /// <returns>Returns finded product</returns>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Product), Description = "Returns finded product")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ProductModelExample))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid product id")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> GetproductAsync([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var result = await _productsService.GetProductAsync(id);
            return Ok(_mapper.Map<Product>(result));
        }

        /// <summary>
        /// Update existing product
        /// </summary>
        /// <param name="id">product id</param>
        /// <param name="product">product parameters</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [SwaggerRequestExample(typeof(Product), typeof(ProductModelExample))]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns 200")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid product object")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> UpdateproductAsync([FromRoute] Guid id, [FromBody] Product product)
        {
            if (id == Guid.Empty || !ModelState.IsValid)
            {
                return BadRequest();
            }

            product.Id = id;
            var result = await _productsService.UpdateProductAsync(_mapper.Map<BLL.Models.Product>(product));
            return Ok();
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns 200")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid product id")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> DeleteproductAsync([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var result = await _productsService.DeleteProductAsync(id);
            return Ok();
        }

        /// <summary>
        /// Get products list
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        [HttpGet("")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Product>), Description = "Returns finded products array")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid pageNumber or pageSize")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> GetproductsListAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
        {
            if (pageNumber == 0 || pageSize == 0)
            {
                return BadRequest();
            }

            var result = await _productsService.GetProductsListAsync(pageNumber, pageSize);
            return Ok(_mapper.Map<IEnumerable<Product>>(result));
        }
    }
}

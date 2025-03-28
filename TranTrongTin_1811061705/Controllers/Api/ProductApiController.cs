using Microsoft.AspNetCore.Mvc;
using TranTrongTin_1811061705.Models;

namespace TranTrongTin_1811061705.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductApiController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Lấy danh sách sản phẩm
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        // Lấy sản phẩm theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // Thêm sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _productRepository.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // Cập nhật sản phẩm
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("ID không khớp");
            }
            await _productRepository.UpdateAsync(product);
            return NoContent();
        }

        // Xóa sản phẩm
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

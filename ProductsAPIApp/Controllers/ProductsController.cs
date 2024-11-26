using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPIApp.DTO;
using ProductsAPIApp.Models;

namespace ProductsAPIApp.Controllers
{
    // localhost:5000/api/products
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly ProductsContext _context;

        public ProductsController(ProductsContext context)
        {
            _context = context;
        }        
        // localhost:5000/api/products =>GET
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {   
            var products = await _context
                            .Products
                            .Where(i=>i.IsActive)
                            .Select(p=> ProductToDTO(p))
                            .ToListAsync();

            if (products==null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        // localhost:5000/api/products/1 =>GET
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int? id)
        {
            var product = await _context
                        .Products
                        .Where(i=>i.ProductId==id)
                        .Select(p=> ProductToDTO(p))
                        .FirstOrDefaultAsync();

            if (id==null)
            {
                return NotFound();
            }
            

            if (product==null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product entity)
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById),new {id=entity.ProductId},entity);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id,Product entity)
        {
            if (id!=entity.ProductId)
            {
                return BadRequest();
            }

            var product = await _context.Products.FirstOrDefaultAsync(p=>p.ProductId==id);

            if (product==null)
            {
                return NotFound();
            }
            product.ProductName=entity.ProductName;
            product.Price=entity.Price;
            product.IsActive=entity.IsActive;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p=>p.ProductId==id);
            if (product==null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        private static ProductDTO ProductToDTO(Product p)
        {
            var entity = new ProductDTO();
            if (p!=null)
            {
                entity.ProductId = p.ProductId;
                entity.ProductName = p.ProductName;
                entity.Price = p.Price;
            }
            return entity;
        }
    }
}
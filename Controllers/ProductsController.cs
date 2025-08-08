using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly StoreContext _context;

    public ProductsController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        return Ok(_context.Products);
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();
        
        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> CreateProduct(Product product)
    {
        product.Id = _context.Products.Max(p => p.Id) + 1;
        _context.Products.Add(product);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, Product product)
    {
        var existingProduct = _context.Products.FirstOrDefault(p => p.Id == id);
        if (existingProduct == null)
            return NotFound();

        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.Stock = product.Stock;
        existingProduct.Category = product.Category;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();

        _context.Products.Remove(product);
        return NoContent();
    }
}

using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly StoreContext _context;

    public OrdersController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Order>> GetOrders()
    {
        return Ok(_context.Orders);
    }

    [HttpGet("{id}")]
    public ActionResult<Order> GetOrder(int id)
    {
        var order = _context.Orders.FirstOrDefault(o => o.Id == id);
        if (order == null)
            return NotFound();
        
        return Ok(order);
    }

    [HttpPost]
    public ActionResult<Order> CreateOrder(Order order)
    {
        order.Id = _context.Orders.Count + 1;
        
        // Calculate total amount
        order.TotalAmount = order.Items.Sum(i => i.Price * i.Quantity);
        
        // Update stock
        foreach (var item in order.Items)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
            if (product != null && product.Stock >= item.Quantity)
            {
                product.Stock -= item.Quantity;
            }
        }
        
        _context.Orders.Add(order);
        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }

    [HttpPut("{id}/status")]
    public IActionResult UpdateOrderStatus(int id, [FromBody] string status)
    {
        var order = _context.Orders.FirstOrDefault(o => o.Id == id);
        if (order == null)
            return NotFound();

        order.Status = status;
        return NoContent();
    }
}

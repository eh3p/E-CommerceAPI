using ECommerceAPI.Models;

namespace ECommerceAPI.Data;

public class StoreContext
{
    public List<Product> Products { get; set; } = new();
    public List<Order> Orders { get; set; } = new();

    public StoreContext()
    {
        // Seed some initial data
        Products.AddRange(new List<Product>
        {
            new() { Id = 1, Name = "Laptop", Description = "High performance laptop", Price = 999.99m, Stock = 10, Category = "Electronics" },
            new() { Id = 2, Name = "T-Shirt", Description = "Cotton t-shirt", Price = 19.99m, Stock = 50, Category = "Clothing" },
            new() { Id = 3, Name = "Book", Description = "Programming book", Price = 29.99m, Stock = 20, Category = "Books" }
        });
    }
}

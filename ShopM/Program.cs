using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopM.Data;
using ShopM.Models;

await RunAsync();

static async Task RunAsync()
{
    await using var dc = new DataContext();

    // Example usage (uncomment to run):
    // var user = await DbOperations.CreateUserAsync(dc, new User { Name = "Alice", Email = "alice@example.com" });
    // var users = await DbOperations.GetAllUsersAsync(dc);
}

/// <summary>
/// CRUD/helper operations for all domain entities.
/// Methods are async and accept a DataContext instance so you can control lifetime (recommended).
/// </summary>
internal static class DbOperations
{
    // ---------- User ----------
    public static async Task<User> CreateUserAsync(DataContext dc, User user)
    {
        await dc.Users.AddAsync(user);
        await dc.SaveChangesAsync();
        return user;
    }

    public static async Task<User?> GetUserAsync(DataContext dc, int id)
    {
        return await dc.Users
            .Include(u => u.Address)
            .Include(u => u.Orders)
                .ThenInclude(o => o.OrderItems)
            .Include(u => u.Reviews)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public static async Task<List<User>> GetAllUsersAsync(DataContext dc)
    {
        return await dc.Users
            .Include(u => u.Address)
            .ToListAsync();
    }

    public static async Task<User?> UpdateUserAsync(DataContext dc, User user)
    {
        dc.Users.Update(user);
        await dc.SaveChangesAsync();
        return user;
    }

    public static async Task<bool> DeleteUserAsync(DataContext dc, int id)
    {
        var entity = await dc.Users.FindAsync(id);
        if (entity == null) return false;
        dc.Users.Remove(entity);
        await dc.SaveChangesAsync();
        return true;
    }

    // ---------- Address ----------
    public static async Task<Address> CreateAddressAsync(DataContext dc, Address address)
    {
        await dc.Addresses.AddAsync(address);
        await dc.SaveChangesAsync();
        return address;
    }

    public static async Task<Address?> GetAddressAsync(DataContext dc, int id)
    {
        return await dc.Addresses
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public static async Task<List<Address>> GetAllAddressesAsync(DataContext dc)
    {
        return await dc.Addresses.Include(a => a.User).ToListAsync();
    }

    public static async Task<Address?> UpdateAddressAsync(DataContext dc, Address address)
    {
        dc.Addresses.Update(address);
        await dc.SaveChangesAsync();
        return address;
    }

    public static async Task<bool> DeleteAddressAsync(DataContext dc, int id)
    {
        var entity = await dc.Addresses.FindAsync(id);
        if (entity == null) return false;
        dc.Addresses.Remove(entity);
        await dc.SaveChangesAsync();
        return true;
    }

    // ---------- Category ----------
    public static async Task<Category> CreateCategoryAsync(DataContext dc, Category category)
    {
        await dc.Categories.AddAsync(category);
        await dc.SaveChangesAsync();
        return category;
    }

    public static async Task<Category?> GetCategoryAsync(DataContext dc, int id)
    {
        return await dc.Categories
            .Include(c => c.ProductCategories)
                .ThenInclude(pc => pc.Product)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public static async Task<List<Category>> GetAllCategoriesAsync(DataContext dc)
    {
        return await dc.Categories.ToListAsync();
    }

    public static async Task<Category?> UpdateCategoryAsync(DataContext dc, Category category)
    {
        dc.Categories.Update(category);
        await dc.SaveChangesAsync();
        return category;
    }

    public static async Task<bool> DeleteCategoryAsync(DataContext dc, int id)
    {
        var entity = await dc.Categories.FindAsync(id);
        if (entity == null) return false;
        dc.Categories.Remove(entity);
        await dc.SaveChangesAsync();
        return true;
    }

    // ---------- Product ----------
    public static async Task<Product> CreateProductAsync(DataContext dc, Product product)
    {
        await dc.Products.AddAsync(product);
        await dc.SaveChangesAsync();
        return product;
    }

    public static async Task<Product?> GetProductAsync(DataContext dc, int id)
    {
        return await dc.Products
            .Include(p => p.Categories)
                .ThenInclude(pc => pc.Category)
            .Include(p => p.Reviews)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public static async Task<List<Product>> GetAllProductsAsync(DataContext dc)
    {
        return await dc.Products
            .Include(p => p.Categories)
                .ThenInclude(pc => pc.Category)
            .ToListAsync();
    }

    public static async Task<Product?> UpdateProductAsync(DataContext dc, Product product)
    {
        dc.Products.Update(product);
        await dc.SaveChangesAsync();
        return product;
    }

    public static async Task<bool> DeleteProductAsync(DataContext dc, int id)
    {
        var entity = await dc.Products.FindAsync(id);
        if (entity == null) return false;
        dc.Products.Remove(entity);
        await dc.SaveChangesAsync();
        return true;
    }

    // ---------- ProductCategory ----------
    public static async Task<ProductCategory> CreateProductCategoryAsync(DataContext dc, ProductCategory pc)
    {
        await dc.ProductCategories.AddAsync(pc);
        await dc.SaveChangesAsync();
        return pc;
    }

    public static async Task<ProductCategory?> GetProductCategoryAsync(DataContext dc, int id)
    {
        return await dc.ProductCategories
            .Include(x => x.Product)
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public static async Task<List<ProductCategory>> GetAllProductCategoriesAsync(DataContext dc)
    {
        return await dc.ProductCategories
            .Include(x => x.Product)
            .Include(x => x.Category)
            .ToListAsync();
    }

    public static async Task<ProductCategory?> UpdateProductCategoryAsync(DataContext dc, ProductCategory pc)
    {
        dc.ProductCategories.Update(pc);
        await dc.SaveChangesAsync();
        return pc;
    }

    public static async Task<bool> DeleteProductCategoryAsync(DataContext dc, int id)
    {
        var entity = await dc.ProductCategories.FindAsync(id);
        if (entity == null) return false;
        dc.ProductCategories.Remove(entity);
        await dc.SaveChangesAsync();
        return true;
    }

    // ---------- Order ----------
    public static async Task<Order> CreateOrderAsync(DataContext dc, Order order)
    {
        await dc.Orders.AddAsync(order);
        await dc.SaveChangesAsync();
        return order;
    }

    public static async Task<Order?> GetOrderAsync(DataContext dc, int id)
    {
        return await dc.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.Payment)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public static async Task<List<Order>> GetAllOrdersAsync(DataContext dc)
    {
        return await dc.Orders
            .Include(o => o.OrderItems)
            .ToListAsync();
    }

    public static async Task<Order?> UpdateOrderAsync(DataContext dc, Order order)
    {
        dc.Orders.Update(order);
        await dc.SaveChangesAsync();
        return order;
    }

    public static async Task<bool> DeleteOrderAsync(DataContext dc, int id)
    {
        var entity = await dc.Orders.FindAsync(id);
        if (entity == null) return false;
        dc.Orders.Remove(entity);
        await dc.SaveChangesAsync();
        return true;
    }

    // ---------- OrderItem ----------
    public static async Task<OrderItem> CreateOrderItemAsync(DataContext dc, OrderItem orderItem)
    {
        await dc.OrderItems.AddAsync(orderItem);
        await dc.SaveChangesAsync();
        return orderItem;
    }

    public static async Task<OrderItem?> GetOrderItemAsync(DataContext dc, int id)
    {
        return await dc.OrderItems
            .Include(oi => oi.Product)
            .Include(oi => oi.Order)
            .FirstOrDefaultAsync(oi => oi.Id == id);
    }

    public static async Task<List<OrderItem>> GetAllOrderItemsAsync(DataContext dc)
    {
        return await dc.OrderItems
            .Include(oi => oi.Product)
            .ToListAsync();
    }

    public static async Task<OrderItem?> UpdateOrderItemAsync(DataContext dc, OrderItem orderItem)
    {
        dc.OrderItems.Update(orderItem);
        await dc.SaveChangesAsync();
        return orderItem;
    }

    public static async Task<bool> DeleteOrderItemAsync(DataContext dc, int id)
    {
        var entity = await dc.OrderItems.FindAsync(id);
        if (entity == null) return false;
        dc.OrderItems.Remove(entity);
        await dc.SaveChangesAsync();
        return true;
    }

    // ---------- Payment ----------
    public static async Task<Payment> CreatePaymentAsync(DataContext dc, Payment payment)
    {
        await dc.Payments.AddAsync(payment);
        await dc.SaveChangesAsync();
        return payment;
    }

    public static async Task<Payment?> GetPaymentAsync(DataContext dc, int id)
    {
        return await dc.Payments
            .Include(p => p.Order)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public static async Task<List<Payment>> GetAllPaymentsAsync(DataContext dc)
    {
        return await dc.Payments.Include(p => p.Order).ToListAsync();
    }

    public static async Task<Payment?> UpdatePaymentAsync(DataContext dc, Payment payment)
    {
        dc.Payments.Update(payment);
        await dc.SaveChangesAsync();
        return payment;
    }

    public static async Task<bool> DeletePaymentAsync(DataContext dc, int id)
    {
        var entity = await dc.Payments.FindAsync(id);
        if (entity == null) return false;
        dc.Payments.Remove(entity);
        await dc.SaveChangesAsync();
        return true;
    }

    // ---------- Review ----------
    public static async Task<Review> CreateReviewAsync(DataContext dc, Review review)
    {
        await dc.Reviews.AddAsync(review);
        await dc.SaveChangesAsync();
        return review;
    }

    public static async Task<Review?> GetReviewAsync(DataContext dc, int id)
    {
        return await dc.Reviews
            .Include(r => r.Product)
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public static async Task<List<Review>> GetAllReviewsAsync(DataContext dc)
    {
        return await dc.Reviews.Include(r => r.Product).Include(r => r.User).ToListAsync();
    }

    public static async Task<Review?> UpdateReviewAsync(DataContext dc, Review review)
    {
        dc.Reviews.Update(review);
        await dc.SaveChangesAsync();
        return review;
    }

    public static async Task<bool> DeleteReviewAsync(DataContext dc, int id)
    {
        var entity = await dc.Reviews.FindAsync(id);
        if (entity == null) return false;
        dc.Reviews.Remove(entity);
        await dc.SaveChangesAsync();
        return true;
    }
}
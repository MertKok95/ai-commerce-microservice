using Microsoft.EntityFrameworkCore;
using Payment.API.Models;

namespace Payment.API.Data;

public class PaymentDbContext : DbContext
{
    public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options) { }
    public DbSet<OrderTransaction> Transactions => Set<OrderTransaction>();
}
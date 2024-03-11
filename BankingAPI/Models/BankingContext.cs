using Microsoft.EntityFrameworkCore;

namespace BankingApi.Models;

public class BankingContext : DbContext
{
    public BankingContext(DbContextOptions<BankingContext> options)
        : base(options) { }

    public DbSet<User> User { get; set; } = null!;
    public DbSet<Account> Account { get; set; } = null!;
    public DbSet<Transaction> Transaction { get; set; } = null!;
}

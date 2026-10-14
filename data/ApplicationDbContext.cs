using CarQuotationSystem.models;

using Microsoft.EntityFrameworkCore;

namespace CarQuotationSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Accessory> Accessories { get; set; }

        public DbSet<Dealer> Dealers { get; set; }

        public DbSet<PurchaseRequest> PurchaseRequests { get; set; }

        public DbSet<PurchaseAccessory> PurchaseAccessories { get; set; }

        public DbSet<Quotation> Quotations { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
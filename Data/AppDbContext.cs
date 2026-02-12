using Microsoft.EntityFrameworkCore;

namespace Credit_Card_Fraud_Detection.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<TxnTable> TxnTables { get; set; }
        public DbSet<FraudAlert> FraudAlerts { get; set; }
        public DbSet<DeviceHistory> DeviceHistory { get; set; }
    }
}


using API.Quotes.Limit.Models;
using Microsoft.EntityFrameworkCore;
using EFCoreDbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace API.Quotes.Limit.Data
{
    public class QuotesContext : EFCoreDbContext, IContext
    {
        public QuotesContext(DbContextOptions<QuotesContext> options)
            : base(options)
        {
        }

        public DbSet<StockQuote> StockQuotes { get; set; } = null!;

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var time = new DateTime(2025, 1, 1);

            modelBuilder.Entity<StockQuote>().HasData(
               new StockQuote  { Id = 1, Symbol = "AAPL", CompanyName = "Apple Inc", Price = 150.25m, Timestamp = time, MarketCap = 250000000000 },
                new StockQuote { Id = 2, Symbol = "MSFT", CompanyName = "Microsoft corporation", Price = 150.25m, Timestamp = time, MarketCap = 220000000000 },
                new StockQuote { Id = 3, Symbol = "GOOGL", CompanyName = "Alphabet Inc", Price = 150.25m, Timestamp = time, MarketCap = 180000000000 },
                new StockQuote { Id = 4, Symbol = "TSLA", CompanyName = "Tesla Inc", Price = 150.25m, Timestamp = time, MarketCap = 170000000000 },
                new StockQuote { Id = 5, Symbol = "AMZN", CompanyName = "Amazon.com Inc", Price = 145.80m, Timestamp = time, MarketCap = 160000000000 },
                new StockQuote { Id = 6, Symbol = "META", CompanyName = "Meta Platforms Inc", Price = 310.45m, Timestamp = time, MarketCap = 90000000000 },
                new StockQuote { Id = 7, Symbol = "NFLX", CompanyName = "Netflix Inc", Price = 420.75m, Timestamp = time, MarketCap = 80000000000 },
                new StockQuote { Id = 8, Symbol = "NVDA", CompanyName = "NVIDIA Corporation", Price = 480.20m, Timestamp = time, MarketCap = 150000000000 },
                new StockQuote { Id = 9, Symbol = "AMD", CompanyName = "Advanced Micro Devices Inc", Price = 110.60m, Timestamp = time, MarketCap = 70000000000 },
                new StockQuote { Id = 10, Symbol = "INTC", CompanyName = "Intel Corporation", Price = 35.90m, Timestamp = time, MarketCap = 60000000000 },
                new StockQuote { Id = 11, Symbol = "ORCL", CompanyName = "Oracle Corporation", Price = 88.15m, Timestamp = time, MarketCap = 75000000000 },
                new StockQuote { Id = 12, Symbol = "SAP", CompanyName = "SAP SE", Price = 135.45m, Timestamp = time, MarketCap = 65000000000 },
                new StockQuote { Id = 13, Symbol = "UBER", CompanyName = "Uber Technologies Inc", Price = 42.75m, Timestamp = time, MarketCap = 55000000000 },
                new StockQuote { Id = 14, Symbol = "LYFT", CompanyName = "Lyft Inc", Price = 11.20m, Timestamp = time, MarketCap = 10000000000 }
            );
        }
    }
}

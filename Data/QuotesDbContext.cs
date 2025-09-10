using API.Quotes.Limit.Models;
using Microsoft.EntityFrameworkCore;
using EFCoreDbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace API.Quotes.Limit.Data
{
    public class QuotesDbContext : EFCoreDbContext, IContext
    {
        public QuotesDbContext(DbContextOptions<QuotesDbContext> options)
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

            var fixedDate = new DateTime(2025, 1, 1);

            modelBuilder.Entity<StockQuote>().HasData(
                new StockQuote { Id = 1, Symbol = "AAPL", CompanyName = "Apple Inc.", Price = 150.25m, Timestamp = fixedDate, MarketCap = 2500000000000L },
                new StockQuote { Id = 2, Symbol = "MSFT", CompanyName = "Microsoft Corporation", Price = 280.50m, Timestamp = fixedDate, MarketCap = 2200000000000L },
                new StockQuote { Id = 3, Symbol = "GOOGL", CompanyName = "Alphabet Inc.", Price = 2700.75m, Timestamp = fixedDate, MarketCap = 1800000000000L },
                new StockQuote { Id = 4, Symbol = "AMZN", CompanyName = "Amazon.com, Inc.", Price = 3300.10m, Timestamp = fixedDate, MarketCap = 1700000000000L },
                new StockQuote { Id = 5, Symbol = "TSLA", CompanyName = "Tesla, Inc.", Price = 700.30m, Timestamp = fixedDate, MarketCap = 800000000000L },
                new StockQuote { Id = 6, Symbol = "NVDA", CompanyName = "NVIDIA Corporation", Price = 400.75m, Timestamp = fixedDate, MarketCap = 900000000000L },
                new StockQuote { Id = 7, Symbol = "FB", CompanyName = "Meta Platforms, Inc.", Price = 350.60m, Timestamp = fixedDate, MarketCap = 950000000000L },
                new StockQuote { Id = 8, Symbol = "NFLX", CompanyName = "Netflix, Inc.", Price = 500.20m, Timestamp = fixedDate, MarketCap = 250000000000L },
                new StockQuote { Id = 9, Symbol = "BABA", CompanyName = "Alibaba Group Holding Limited", Price = 200.15m, Timestamp = fixedDate, MarketCap = 500000000000L },
                new StockQuote { Id = 10, Symbol = "V", CompanyName = "Visa Inc.", Price = 220.40m, Timestamp = fixedDate, MarketCap = 480000000000L }
            );
        }
    }
}

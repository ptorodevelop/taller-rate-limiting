using API.Quotes.Limit.Data;
using API.Quotes.Limit.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace API.Quotes.Limit.DbContext
{
    public class DbSeeder(IContext context)
    {

        private readonly IContext _context = context;
        public 
            async Task SeedAsync()
        {
            if (await _context.StockQuotes.AnyAsync()) return;
            
            var now = new DateTime(2024, 01, 01);
            var stockQuotes = new List<StockQuote>
            {
                new StockQuote { Symbol = "AAPL", CompanyName = "Apple Inc.", Price = 150.25m, Timestamp = now, MarketCap = 2500000000000L },
                new StockQuote { Symbol = "MSFT", CompanyName = "Microsoft Corporation", Price = 280.50m, Timestamp = now, MarketCap = 2200000000000L },
                new StockQuote { Symbol = "GOOGL", CompanyName = "Alphabet Inc.", Price = 2700.75m, Timestamp = now, MarketCap = 1800000000000L },
                new StockQuote { Symbol = "AMZN", CompanyName = "Amazon.com, Inc.", Price = 3300.10m, Timestamp = now, MarketCap = 1700000000000L },
                new StockQuote { Symbol = "TSLA", CompanyName = "Tesla, Inc.", Price = 700.30m, Timestamp = now, MarketCap = 800000000000L },
                new StockQuote { Symbol = "NVDA", CompanyName = "NVIDIA Corporation", Price = 400.75m, Timestamp = now, MarketCap = 900000000000L },
                new StockQuote { Symbol = "FB", CompanyName = "Meta Platforms, Inc.", Price = 350.60m, Timestamp = now, MarketCap = 950000000000L },
                new StockQuote { Symbol = "NFLX", CompanyName = "Netflix, Inc.", Price = 500.20m, Timestamp = now, MarketCap = 250000000000L },
                new StockQuote { Symbol = "BABA", CompanyName = "Alibaba Group Holding Limited", Price = 200.15m, Timestamp = now, MarketCap = 500000000000L },
                new StockQuote { Symbol = "V", CompanyName = "Visa Inc.", Price = 220.40m, Timestamp = now, MarketCap = 480000000000L }
            };

            _context.StockQuotes.AddRange(stockQuotes);
            await _context.SaveChangesAsync();
        }
    }
}

using API.Quotes.Limit.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Quotes.Limit.Data
{
    public interface IContext
    {

        Task<int> SaveChangesAsync();
        DbSet<StockQuote> StockQuotes { get; set; }
    }
}

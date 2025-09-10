using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Quotes.Limit.Models
{
    public class StockQuote
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)] 
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)] 
        public string CompanyName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")] 
        public decimal Price { get; set; }

        public DateTime Timestamp { get; set; }

        public long MarketCap { get; set; }
    }
}

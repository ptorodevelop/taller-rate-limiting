using API.Quotes.Limit.Data;
using API.Quotes.Limit.Models;
using API.Quotes.Limit.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Quotes.Limit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly ILogger<QuotesController> _logger;
        private readonly IRateLimiterService _rateLimiterService;
        private readonly IDatabase _redisDb;
        private readonly QuotesContext _context;

        public QuotesController(
            IRateLimiterService rateLimiterService,
            IConnectionMultiplexer redisDb,
            ILogger<QuotesController> logger,
            QuotesContext context)
        {
            _rateLimiterService = rateLimiterService;
            _redisDb = redisDb.GetDatabase();
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuotes()
        {
            _logger.LogInformation("Fetching all stock quotes");
            var quotes = await _context.StockQuotes.ToListAsync();
            return Ok(quotes);
        }

        [HttpGet("{symbol}")]
        public async Task<IActionResult> GetQuoteBySymbol(string symbol)
        {
            if (!(await _rateLimiterService.AcquireLimit(_rateLimiterService._symbolLimiter)))
            {
                Response.Headers["Retry-After"] = "30";
                return StatusCode(
                    StatusCodes.Status429TooManyRequests,
                    "Symbol rate limit exceeded. Try again later.");
            }

            _logger.LogInformation($"Fetching stock quote for symbol: {symbol}");
            var quote = await _context.StockQuotes.FirstOrDefaultAsync(x => x.Symbol == symbol);
            if (quote == null)
            {
                var message = $"Quote for symbol '{symbol}' not found.";
                _logger.LogError(message);
                return NotFound(message);
            }

            return Ok(quote);
        }

        [HttpGet("top/{top}")]
        public async Task<IActionResult> GetTopQuotes(int top)
        {
            if (!(await _rateLimiterService.AcquireLimit(_rateLimiterService._topLimiter)))
            {
                Response.Headers["Retry-After"] = "60";
                return StatusCode(
                    StatusCodes.Status429TooManyRequests,
                    "Top rate limit exceeded. Try again later.");
            }

            _logger.LogInformation($"Fetching top {top} stock quotes by market cap");

            var count = await _context.StockQuotes.CountAsync();
            if (top <= 0 || top > count)
            {
                return BadRequest();
            }

            var quotesTop = await _context.StockQuotes
                .OrderByDescending(x => x.MarketCap)
                .Take(top)
                .ToListAsync();

            return Ok(quotesTop);
        }

        [HttpGet("/bySymbol/{symbol}")]
        public async Task<IActionResult> GetQuoteBySymbolRedis(string symbol)
        {
            var token = "token_bucket:symbol";
            var tokenLimit = 5;
            var refillInterval = TimeSpan.FromSeconds(30);

            var cacheTokenResult = await _redisDb.StringGetWithExpiryAsync(token);
            var currentTokens = (int?)(cacheTokenResult.Value) ?? tokenLimit;
            var ttl = cacheTokenResult.Expiry;

            if (!ttl.HasValue || ttl.Value.TotalSeconds <= 0)
            {
                currentTokens = tokenLimit;
                await _redisDb.StringSetAsync(token, currentTokens, refillInterval);
            }

            if (currentTokens <= 0)
            {
                Response.Headers["Retry-After"] = ttl?.TotalSeconds.ToString() ?? "30";
                return StatusCode(
                    StatusCodes.Status429TooManyRequests,
                    "Symbol rate limit exceeded. Try again later.");
            }

            await _redisDb.StringSetAsync(token, currentTokens - 1, refillInterval);

            _logger.LogInformation($"Fetching stock quote for symbol: {symbol}");
            var quote = await _context.StockQuotes.FirstOrDefaultAsync(x => x.Symbol == symbol);
            if (quote == null)
            {
                var message = $"Quote for symbol '{symbol}' not found.";
                _logger.LogError(message);
                return NotFound(message);
            }

            return Ok(quote);
        }
    }
}

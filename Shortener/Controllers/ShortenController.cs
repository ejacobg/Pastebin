using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shortener.Models;

namespace Shortener.Controllers
{
    [ApiController]
    public class ShortenController : ControllerBase
    {
        private readonly IShortenerService _shortenerService;
        private readonly ILogger<ShortenController> _logger;

        public ShortenController(IShortenerService shortenerService, ILogger<ShortenController> logger)
        {
            _shortenerService = shortenerService;
            _logger = logger;
        }

        [HttpPost("/shorten")]
        public async Task<ShortenResult> Shorten([FromBody] PasteInput input)
        {
            var result = new ShortenResult(
                await _shortenerService.Shorten(HttpContext, input.ToPaste())
            );

            _logger.LogInformation(
                "Created shortlink {@shortlink}",
                result.Shortlink);

            return result;
        }

        public class PasteInput
        {
            [Required]
            public string Content { get; set; }

            public int? Expires { get; set; }

            public Paste ToPaste()
            {
                var expires = Expires ?? 60;
                return new Paste
                {
                    Content = Content,
                    Expires = expires
                };
            }
        }

        public record ShortenResult(string Shortlink);
    }
}
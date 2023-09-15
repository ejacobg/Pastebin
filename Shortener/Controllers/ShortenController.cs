using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shortener.Data;
using Shortener.Models;

namespace Shortener.Controllers
{
    public class ShortenController : ControllerBase
    {
        private readonly IShortenerService _shortenerService;

        public ShortenController(IShortenerService shortenerService)
        {
            _shortenerService = shortenerService;
        }

        [HttpPost("/shorten")]
        public async Task<ShortenResult> Shorten([FromBody] PasteInput input)
        {
            return new ShortenResult(
                await _shortenerService.Shorten(HttpContext, input.ToPaste())
            );
        }

        public class PasteInput
        {
            [Required] public string Content { get; set; }
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
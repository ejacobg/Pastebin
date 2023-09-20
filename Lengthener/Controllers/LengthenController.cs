using System.Threading.Tasks;
using Lengthener.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lengthener.Controllers
{
    [ApiController]
    public class LengthenController : ControllerBase
    {
        private readonly IPasteStore _pasteStore;

        public LengthenController(IPasteStore pasteStore)
        {
            _pasteStore = pasteStore;
        }

        [HttpGet("/lengthen/{shortlink}")]
        public async Task<ActionResult<LengthenResult>> Lengthen(string shortlink)
        {
            var paste = await _pasteStore.GetByShortlink(shortlink);
            if (paste is null)
            {
                return NotFound();
            }

            return new LengthenResult(
                paste.Content,
                paste.Created.ToString("yyyy-MM-dd hh:mm:ss"),
                paste.Expires
            );
        }

        public record LengthenResult(string Content, string Created, int Expires);
    }
}
using System;
using System.Threading.Tasks;
using Lengthener.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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

            // If the current time is after the Created + Expires date, then return an "expired" response.
            // Expired pastes will no longer be tracked by the counter.
            if (paste.Created.AddMinutes(paste.Expires) < DateTime.Now)
            {
                return StatusCode(StatusCodes.Status410Gone);
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
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Database.Models;
using Microsoft.AspNetCore.Http;

namespace Shortener.Models
{
    /// <summary>
    /// IShortenerService is implemented by types that handle paste shortening requests.
    /// </summary>
    public interface IShortenerService
    {
        /// <summary>
        /// Shorten performs a shortening operation based off the given request.
        /// </summary>
        /// <param name="context">the original paste request.</param>
        /// <param name="paste">the parsed paste from the request.</param>
        /// <returns>A string representing the shortened content.</returns>
        public Task<string> Shorten(HttpContext context, Paste paste);
    }

    /// <summary>
    /// TextShortener handles plaintext paste shortening requests, where the paste content is in the request body.
    /// </summary>
    public class TextShortener : IShortenerService
    {
        private readonly IGenerator _generator;
        private readonly IPasteStore _pasteStore;

        public TextShortener(IGenerator generator, IPasteStore pasteStore)
        {
            _generator = generator;
            _pasteStore = pasteStore;
        }

        /// <summary>
        /// Shorten performs a shortening operation based off the given request.
        /// </summary>
        /// <param name="context">the request containing the paste to be shortened.</param>
        /// <param name="paste">the parsed paste from the request.</param>
        /// <returns>A string representing the shortened content.</returns>
        public async Task<string> Shorten(HttpContext context, Paste paste)
        {
            bool saved;
            do
            {
                paste.Shortlink = _generator.Generate(context, paste);
                saved = await _pasteStore.Save(paste);
            } while (!saved);

            return paste.Shortlink;
        }
    }
}
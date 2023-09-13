using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Shortener.Models
{
    /// <summary>
    /// IGenerator is implemented by types that can generate keys for incoming paste requests.
    /// </summary>
    public interface IGenerator
    {
        /// <summary>
        /// Generate creates a new key based on the incoming request data (not necessarily the paste itself).
        /// Generate is not guaranteed to create unique keys for all requests.
        /// Multiple calls to Generate should produce different results(?)
        /// </summary>
        /// <param name="context">the request containing the paste to be shortened.</param>
        /// <returns>A base62-encoded string to be used as a key for the given paste.</returns>
        public string Generate(HttpContext context);
    }

    /// <summary>
    /// IPGenerator creates keys using the algorithm outlined in the
    /// <a href="https://github.com/donnemartin/system-design-primer/blob/master/solutions/system_design/pastebin/README.md#use-case-user-enters-a-block-of-text-and-gets-a-randomly-generated-link">
    /// System Design Primer
    /// </a>.
    /// Note that this class requires access to the client IP in order to function properly. Use appropriate middleware when using this in front of a proxy or gateway.
    /// </summary>
    public class IpGenerator : IGenerator
    {
        public string Generate(HttpContext context)
        {
            // Get the IP address of the request.
            // May need middleware to actually get the client's IP into the context, especially if you're running this behind a proxy or API gateway.
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            // Get the current timestamp.
            var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds(); // This line makes it hard to test. One solution might be to use some middleware that attaches timestamp data to the request.

            // Take the MD5 hash of the IP address + timestamp.
            var preimage = $"{ipAddress}{timestamp}";
            using var md5 = MD5.Create();
            var input = Encoding.ASCII.GetBytes(preimage);
            var hash = md5.ComputeHash(input);

            // Base62-encode the MD5 hash.
            var encoded = Base62.Encode(hash);

            // Return the first 7 digits of the encoded hash.
            return encoded[..7];
        }
    }

    public static class Base62
    {
        public static string Encode(byte[] bytes)
        {
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var result = new StringBuilder();
            var value = new BigInteger(bytes.Reverse().ToArray());

            while (value > 0)
            {
                value = BigInteger.DivRem(value, 62, out var remainder);
                result.Insert(0, chars[(int)remainder]);
            }

            return result.ToString();
        }
    }
}
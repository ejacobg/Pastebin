using System;
using System.Text;
using Shortener.Models;
using Xunit;

namespace Shortener.Tests
{
    public class GeneratorTest
    {
        // These tests are for the original Base62 implementation. They are not compatible with the currently-used one.
        [Theory]
        [InlineData("", "")]
        [InlineData("::11694622294", "1Ylie8QCzPXZOk7456")] // An example IPv6 address and Unix timestamp.
        [InlineData("test", "289lyu")]
        [InlineData("127.0.0.11695074824", "1hOwLMkOsW0YLQL6P5CYE0uQeS")]
        [InlineData("9cd2536f0c0f9c8a93cc24d82e605eb2", "")]
        public void TestBase62Encode(string input, string expected)
        {
            var bytes = Encoding.ASCII.GetBytes(input);
            var actual = Base62.Encode(bytes);

            Assert.Equal(expected, actual);
        }
    }
}
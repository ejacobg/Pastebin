using System;
using System.Text;
using Shortener.Models;
using Xunit;

namespace Shortener.Tests
{
    public class GeneratorTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("::11694622294", "1Ylie8QCzPXZOk7456")] // An example IPv6 address and Unix timestamp.
        [InlineData("test", "289lyu")]
        public void TestBase62Encode(string input, string expected)
        {
            var bytes = Encoding.ASCII.GetBytes(input);
            var actual = Base62.Encode(bytes);

            Assert.Equal(expected, actual);
        }
    }
}

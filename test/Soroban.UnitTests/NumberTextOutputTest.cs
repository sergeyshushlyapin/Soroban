using System;
using System.IO;
using Soroban.App;
using Xunit;

namespace Soroban.UnitTests
{
    public class NumberTextOutputTest
    {
        [Fact]
        public void InstantiateWithNullThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new NumberTextOutput(null));
        }

        [Fact]
        public void WriteWithNullThrows()
        {
            var sut = new NumberTextOutput(new StubOutput());
            Assert.Throws<ArgumentNullException>(() =>
                sut.Write(null));
        }

        [Theory]
        [InlineData(-1, "-1")]
        [InlineData(0, "0")]
        [InlineData(1, "+1")]
        public void WriteCallsInnerOutput(int line, string expected)
        {
            object actual = null;
            var sut = new NumberTextOutput(
                new DeletagingOutput(
                    l => actual = l));
            sut.Write(line);
            Assert.Equal(expected, actual);
        }
    }
}
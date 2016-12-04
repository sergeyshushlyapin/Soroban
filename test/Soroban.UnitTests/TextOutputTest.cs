using System;
using System.IO;
using Soroban.App;
using Xunit;

namespace Soroban.UnitTests
{
    public class TextOutputTest
    {
        [Fact]
        public void InstantiateWithNullThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new TextOutput(null));
        }

        [Fact]
        public void WriteWithNullThrows()
        {
            var sut = new TextOutput(new StringWriter());
            Assert.Throws<ArgumentNullException>(() =>
                sut.Write(null));
        }

        [Fact]
        public void WriteCallsInnerOutput()
        {
            var writer = new StringWriter();
            var sut = new TextOutput(writer);
            sut.Write("line");
            Assert.Equal("line", writer.ToString());
        }
    }
}
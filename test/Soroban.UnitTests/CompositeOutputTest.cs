using System;
using Soroban.App;
using Xunit;

namespace Soroban.UnitTests
{
    public class CompositeOutputTest
    {
        [Fact]
        public void InstantiateWithNullThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new CompositeOutput(null));
        }

        [Fact]
        public void WriteWithNullThrows()
        {
            var sut = new CompositeOutput(new StubOutput());
            Assert.Throws<ArgumentNullException>(() =>
                sut.Write(null));
        }

        [Fact]
        public void WriteCallsInnerOutput()
        {
            var actual = string.Empty;
            var output1 = new DeletagingOutput(l => actual += "1");
            var output2 = new DeletagingOutput(l => actual += "2");
            var sut = new CompositeOutput(output1, output2);

            sut.Write("line");

            Assert.Equal("12", actual);
        }
    }

    public class DeletagingOutput : IOutput
    {
        private readonly Action<object> _action;

        public DeletagingOutput(Action<object> action)
        {
            _action = action;
        }

        public void Write(object line)
        {
            _action(line);
        }
    }
}
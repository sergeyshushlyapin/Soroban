using System;
using System.IO;
using System.Linq;
using FsCheck.Xunit;
using Soroban.App;
using Xunit;

namespace Soroban.UnitTests
{
    public class RoundTest
    {
        [Fact]
        public void InstantiateWithNullNumbersThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new Round(null, new StubOutput()));
        }

        [Fact]
        public void InstantiateWithNullOutputThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new Round(new int[0], null));
        }

        [Fact]
        public void NumberResultWriterReturnsDefaultValue()
        {
            var sut = new Round(new int[0], new StubOutput());
            Assert.NotNull(sut.NumberResultWriter);
        }

        [Fact]
        public void SuccessResultWriterReturnsDefaultValue()
        {
            var sut = new Round(new int[0], new StubOutput());
            Assert.NotNull(sut.SuccessResultWriter);
        }

        [Fact]
        public void FailureResultWriterReturnsDefaultValue()
        {
            var sut = new Round(new int[0], new StubOutput());
            Assert.NotNull(sut.FailureResultWriter);
        }

        [Fact]
        public void SetNumberResultWriterWithNullOutputThrows()
        {
            var sut = new Round(new int[0], new StubOutput());
            Assert.Throws<ArgumentNullException>(() =>
                sut.NumberResultWriter = null);
        }

        [Fact]
        public void SetSuccessResultWriterWithNullOutputThrows()
        {
            var sut = new Round(new int[0], new StubOutput());
            Assert.Throws<ArgumentNullException>(() =>
                sut.SuccessResultWriter = null);
        }

        [Fact]
        public void SetFailureResultWriterWithNullOutputThrows()
        {
            var sut = new Round(new int[] { }, new StubOutput());
            Assert.Throws<ArgumentNullException>(() =>
                sut.FailureResultWriter = null);
        }

        [Fact]
        public void SetNumberResultWriterSetsCorrectValue()
        {
            var expected = new StubOutput();
            var sut = new Round(new int[0], new StubOutput())
            {
                NumberResultWriter = expected
            };

            Assert.Same(expected, sut.NumberResultWriter);
        }

        [Fact]
        public void SetSuccessResultWriterSetsCorrectValue()
        {
            var expected = new StubOutput();
            var sut = new Round(new int[0], new StubOutput())
            {
                SuccessResultWriter = expected
            };

            Assert.Same(expected, sut.SuccessResultWriter);
        }

        [Fact]
        public void SetFailureResultWriterSetsCorrectValue()
        {
            var expected = new StubOutput();
            var sut = new Round(new int[0], new StubOutput())
            {
                FailureResultWriter = expected
            };

            Assert.Same(expected, sut.FailureResultWriter);
        }

        [Property]
        public void PrintNumbersWritesToNumberResultWriter(int[] numbers)
        {
            var expected = string.Join(string.Empty, numbers);
            var output = new TextOutput(new StringWriter());
            var sut = new Round(numbers, new StubOutput())
            {
                NumberResultWriter = output
            };

            sut.ReciteNumbers();

            Assert.Equal(expected, output.ToString());
        }

        [Property]
        public void PrintNumbersWritesReturnsRoundInstance(int[] numbers)
        {
            var sut = new Round(numbers, new StubOutput())
            {
                NumberResultWriter = new StubOutput()
            };
            var actual = sut.ReciteNumbers();
            Assert.Same(sut, actual);
        }

        [Fact]
        public void VerifyResultWithNullAnswerThrows()
        {
            var sut = new Round(new int[0], new StubOutput());
            Assert.Throws<ArgumentNullException>(() =>
                sut.VerifyResult(null));
        }

        [Property(Skip = "Unclear how to ignore null.")]
        public void VerifyResultWithInvalidAnswerThrows(string answer)
        {
            var sut = new Round(new int[0], new StubOutput());
            Assert.Throws<ArgumentException>(() =>
                sut.VerifyResult(answer));
        }

        [Property]
        public void VerifyResultWithCorrectAnswerPrintsSuccessMessage(
            int[] numbers)
        {
            var expected = numbers.Sum().ToString();
            var output = new TextOutput(new StringWriter());
            var sut = new Round(numbers, new StubOutput())
            {
                NumberResultWriter = new StubOutput(),
                SuccessResultWriter = output
            };

            sut.ReciteNumbers()
                .VerifyResult(expected);

            Assert.Equal(expected, output.ToString());
        }

        [Property]
        public void VerifyResultWithIncorrectAnswerPrintsFailureMessage(
            int[] numbers)
        {
            var expected = (numbers.Sum() - 1).ToString();
            var output = new TextOutput(new StringWriter());
            var sut = new Round(numbers, new StubOutput())
            {
                NumberResultWriter = new StubOutput(),
                FailureResultWriter = output
            };

            sut.ReciteNumbers()
                .VerifyResult(expected);

            Assert.Equal(expected, output.ToString());
        }
    }

    public class StubOutput : IOutput
    {
        public void Write(object line)
        {
        }
    }
}
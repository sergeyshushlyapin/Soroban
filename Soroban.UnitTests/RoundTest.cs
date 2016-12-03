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
                new Round(null));
        }

        [Fact]
        public void PrintNumbersToWithNullWriterThrows()
        {
            var sut = new Round(new int[0]);
            Assert.Throws<ArgumentNullException>(() =>
                sut.PrintNumbersTo(null));
        }

        [Property]
        public void PrintNumbersToOutput(int[] numbers)
        {
            var expected = string.Join(
                string.Empty,
                numbers.Select(n => n + Environment.NewLine));
            var output = new StringWriter();
            var sut = new Round(numbers);

            sut.PrintNumbersTo(output);

            Assert.Equal(expected, output.ToString());
        }

        [Fact]
        public void VerifyResultWithNullAnswerThrows()
        {
            var sut = new Round(new int[0]);
            Assert.Throws<ArgumentNullException>(() =>
                sut.VerifyResult(null));
        }

        [Property(Skip = "Unclear how to ignore null.")]
        public void VerifyResultWithInvalidAnswerThrows(string answer)
        {
            var sut = new Round(new int[0]);
            Assert.Throws<ArgumentException>(() =>
                sut.VerifyResult(answer));
        }

        [Property]
        public void VerifyResultWithCorrectAnswerPrintsSuccessMessage(
            int[] numbers)
        {
            var validAnswer = numbers.Sum().ToString();
            var expected = "Correct!" + Environment.NewLine;
            var output = new StringWriter();
            var sut = new Round(numbers);

            sut.PrintNumbersTo(output)
                .VerifyResult(validAnswer);

            Assert.EndsWith(expected, output.ToString());
        }

        [Property]
        public void VerifyResultWithIncorrectAnswerPrintsFailureMessage(
            int[] numbers)
        {
            var invalidAnswer = (numbers.Sum() - 1).ToString();
            var expected = "Nope!" + Environment.NewLine;
            var output = new StringWriter();
            var sut = new Round(numbers);

            sut.PrintNumbersTo(output)
                .VerifyResult(invalidAnswer);

            Assert.EndsWith(expected, output.ToString());
        }
    }
}
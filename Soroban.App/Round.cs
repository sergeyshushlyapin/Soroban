using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace Soroban.App
{
    public class Round
    {
        private readonly int[] nums;
        private readonly IEnumerator enumerator;
        private TextWriter textWriter;

        public Round(int[] nums)
        {
            if (nums == null)
                throw new ArgumentNullException(nameof(nums));

            this.nums = nums;
            this.enumerator = nums.GetEnumerator();
        }

        public Round PrintNumbersTo(TextWriter @out)
        {
            if (@out == null)
                throw new ArgumentNullException(nameof(@out));

            this.textWriter = @out;

            while (this.enumerator.MoveNext())
            {
                @out.WriteLine(this.enumerator.Current);
                //Thread.Sleep(1000);
            }

            return this;
        }

        public void VerifyResult(string answer)
        {
            if (answer == null)
                throw new ArgumentNullException(nameof(answer));

            int result;
            if (!int.TryParse(answer, out result))
                throw new ArgumentException();

            textWriter.WriteLine(
                this.nums.Sum() == result
                    ? "Correct!"
                    : "Nope!");
        }
    }
}
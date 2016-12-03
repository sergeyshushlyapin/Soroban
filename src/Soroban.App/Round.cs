using System;
using System.Collections;
using System.Linq;

namespace Soroban.App
{
    public class Round
    {
        private readonly int[] nums;
        private readonly IEnumerator enumerator;
        private IOutput _numberResultWriter;
        private IOutput _successResultWriter;
        private IOutput _failureResultWriter;

        public Round(int[] nums, IOutput output)
        {
            if (nums == null)
                throw new ArgumentNullException(nameof(nums));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            this.nums = nums;
            this.enumerator = nums.GetEnumerator();

            _numberResultWriter = new CompositeOutput(
               new NumberTextOutput(output),
                new SpeakerOutput(),
                new DelayedOutput(500),
                new CleaningOutput(),
                new DelayedOutput(100));

            _successResultWriter = new CompositeOutput(
                new CleaningOutput(),
                new ColoredTextOutput(
                    ConsoleColor.Green,
                    output));

            _failureResultWriter = new CompositeOutput(
                new CleaningOutput(),
                new ColoredTextOutput(
                    ConsoleColor.Red,
                    output));
        }

        public IOutput NumberResultWriter
        {
            get { return _numberResultWriter; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                _numberResultWriter = value;
            }
        }

        public IOutput SuccessResultWriter
        {
            get { return _successResultWriter; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                _successResultWriter = value;
            }
        }

        public IOutput FailureResultWriter
        {
            get { return _failureResultWriter; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                _failureResultWriter = value;
            }
        }

        public Round PrintNumbers()
        {
            while (this.enumerator.MoveNext())
            {
                NumberResultWriter.Write(this.enumerator.Current);
            }

            return this;
        }

        public void VerifyResult(string answer)
        {
            if (answer == null)
                throw new ArgumentNullException(nameof(answer));

            int result;
            if (!int.TryParse(answer, out result))
            {
                FailureResultWriter.Write(answer);
                return;
            }

            if (this.nums.Sum() != result)
            {
                FailureResultWriter.Write(answer);
                return;
            }

            SuccessResultWriter.Write(answer);
        }
    }
}
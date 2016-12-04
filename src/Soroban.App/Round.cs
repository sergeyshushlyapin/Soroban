using System;
using System.Collections;
using System.Globalization;
using System.Linq;

namespace Soroban.App
{
    public class Round
    {
        private readonly int[] _nums;
        private readonly IEnumerator _enumerator;
        private IOutput _numberResultWriter;
        private IOutput _successResultWriter;
        private IOutput _failureResultWriter;

        public Round(int[] nums, IOutput output)
        {
            if (nums == null)
                throw new ArgumentNullException(nameof(nums));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            this._nums = nums;
            this._enumerator = nums.GetEnumerator();

            var culture = CultureInfo.GetCultureInfo("uk-UA");

            _numberResultWriter = new CompositeOutput(
                new NumberTextOutput(output),
                new NumberTextOutput(
                    new Megaphone(
                        culture)),
                new OutputDelay(500),
                new CleaningOutput(),
                new OutputDelay(100));

            _successResultWriter = new CompositeOutput(
                new CleaningOutput(),
                new ColoredConsoleOutput(
                    ConsoleColor.Green,
                    output),
                new SpeechRecord("Ну ты крутяк! Так держать!", culture));

            _failureResultWriter = new CompositeOutput(
                new CleaningOutput(),
                new ColoredConsoleOutput(
                    ConsoleColor.Red,
                    output),
                new SpeechRecord("Неа. Чуть чуть не правильно.", culture));
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

        public Round ReciteNumbers()
        {
            while (this._enumerator.MoveNext())
            {
                NumberResultWriter.Write(this._enumerator.Current);
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

            if (this._nums.Sum() != result)
            {
                FailureResultWriter.Write(answer);
                return;
            }

            SuccessResultWriter.Write(answer);
        }
    }
}
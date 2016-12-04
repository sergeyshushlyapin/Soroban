using System;

namespace Soroban.App
{
    public class NumberTextOutput : IOutput
    {
        private readonly IOutput _output;

        public NumberTextOutput(IOutput output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));
            _output = output;
        }

        public void Write(object line)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            _output.Write(((int)line).ToString("+#;-#;0"));
        }
    }
}
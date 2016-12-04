using System;
using System.IO;

namespace Soroban.App
{
    public class TextOutput : IOutput
    {
        private readonly TextWriter _writer;

        public TextOutput(TextWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            _writer = writer;
        }

        public void Write(object line)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));
            _writer.Write(line);
        }

        public override string ToString()
        {
            return _writer.ToString();
        }
    }
}
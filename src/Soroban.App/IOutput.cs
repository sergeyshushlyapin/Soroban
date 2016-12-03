using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Soroban.App
{
    public interface IOutput
    {
        void Write(object line);
    }

    public class CompositeOutput : IOutput
    {
        public IEnumerable<IOutput> Outputs { get; }

        public CompositeOutput(params IOutput[] outputs)
        {
            Outputs = outputs;
        }

        public void Write(object line)
        {
            foreach (var output in Outputs)
            {
                output.Write(line);
            }
        }
    }

    public class TextOutput : IOutput
    {
        public TextWriter Writer { get; }

        public TextOutput(TextWriter writer)
        {
            Writer = writer;
        }

        public void Write(object line)
        {
            Writer.Write(line);
        }

        public override string ToString()
        {
            return this.Writer.ToString();
        }
    }

    public class NumberTextOutput : IOutput
    {
        private readonly IOutput _output;

        public NumberTextOutput(IOutput output)
        {
            _output = output;
        }

        public void Write(object line)
        {
            _output.Write(((int)line).ToString("+#;-#;0"));
        }
    }

    public class DelayedOutput : IOutput
    {
        public int Delay { get; }

        public DelayedOutput(int delay)
        {
            Delay = delay;
        }

        public void Write(object line)
        {
            Thread.Sleep(Delay);
        }
    }

    public class SpeakerOutput : IOutput
    {
        public void Write(object line)
        {
            Console.Beep();
        }
    }

    public class CleaningOutput : IOutput
    {
        public void Write(object line)
        {
            Console.Clear();
        }
    }

    public class ColoredTextOutput : IOutput
    {
        private readonly ConsoleColor _color;
        private readonly IOutput _output;

        public ColoredTextOutput(ConsoleColor color, IOutput output)
        {
            _color = color;
            _output = output;
        }

        public void Write(object line)
        {
            Console.ForegroundColor = _color;
            _output.Write(line);
            Console.ResetColor();
        }
    }
}
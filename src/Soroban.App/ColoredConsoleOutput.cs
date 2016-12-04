using System;

namespace Soroban.App
{
    public class ColoredConsoleOutput : IOutput
    {
        private readonly ConsoleColor _color;
        private readonly IOutput _output;

        public ColoredConsoleOutput(ConsoleColor color, IOutput output)
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
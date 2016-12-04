using System;

namespace Soroban.App
{
    public class Beeper : IOutput
    {
        public void Write(object line)
        {
            Console.Beep();
        }
    }
}
using System;

namespace Soroban.App
{
    public class CleaningOutput : IOutput
    {
        public void Write(object line)
        {
            Console.Clear();
        }
    }
}
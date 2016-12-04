using System.Threading;

namespace Soroban.App
{
    public class OutputDelay : IOutput
    {
        private readonly int _delay;

        public OutputDelay(int delay)
        {
            _delay = delay;
        }

        public void Write(object line)
        {
            Thread.Sleep(_delay);
        }
    }
}
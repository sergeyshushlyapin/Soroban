using System;

namespace Soroban.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var nums = new RandomNumberSequence(-3, 5);
            var output = new TextOutput(Console.Out);
            var round = new Round(nums, output);
            round.PrintNumbers()
                .VerifyResult(
                    Console.ReadLine());

            Console.ReadKey();
        }
    }
}

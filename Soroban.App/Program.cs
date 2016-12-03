using System;

namespace Soroban.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var nums = new RandomNumberSequence();
            new Round(nums)
                .PrintNumbersTo(Console.Out)
                .VerifyResult(
                    Console.ReadLine());

            Console.ReadKey();
        }
    }
}

using System;

namespace Soroban.App
{
    public class RandomNumberSequence
    {
        public RandomNumberSequence(int min, int max)
        {
            var random = new Random();
            this.Nums = new[]
            {
                random.Next(min, max),
                random.Next(min, max),
                random.Next(min, max),
                random.Next(min, max),
            };
        }

        public int[] Nums { get; }

        public static implicit operator int[] (RandomNumberSequence random)
        {
            return random.Nums;
        }
    }
}
using System;

namespace Soroban.App
{
    public class RandomNumberSequence
    {
        public RandomNumberSequence()
        {
            var random = new Random();
            this.Nums = new[]
            {
                random.Next(0, 5),
                random.Next(0, 5),
                random.Next(0, 5),
                random.Next(0, 5),
            };
        }

        public int[] Nums { get; }

        public static implicit operator int[] (RandomNumberSequence random)
        {
            return random.Nums;
        }
    }
}
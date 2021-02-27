using System;

namespace APIPlaygroundBusiness
{
    public class Randomizer : IRandomizer
    {
        private readonly Random _random;

        public Randomizer()
        {
            _random = new Random();
        }

        public int Next()
        {
            return _random.Next();
        }
    }
}

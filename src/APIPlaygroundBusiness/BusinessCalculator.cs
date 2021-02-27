namespace APIPlaygroundBusiness
{
    public class BusinessCalculator : IBusinessCalculator
    {

        private readonly IRandomizer _randomizer;
        private const string Stonks = "Stonks!";
        private const string NotStonks = "Not stonks...";


        public BusinessCalculator(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public string CalculateBusiness()
        {
            if (_randomizer.Next() % 2 == 0)
            {
                return Stonks;
            }

            return NotStonks;
        }
    }
}

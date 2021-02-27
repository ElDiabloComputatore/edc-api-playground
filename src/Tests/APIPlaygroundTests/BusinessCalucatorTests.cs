using NUnit.Framework;
using NSubstitute;
using APIPlaygroundBusiness;

namespace APIPlaygroundBusinessTests
{
    [TestFixture]
    public class BusinessCalucatorTests
    {
        private const string Stonks = "Stonks!";
        private const string NotStonks = "Not stonks...";

        [Test]
        public void CalculateBusiness_OddNumber_NotStonksReturned()
        {
            // arrange
            var randomizerMock = Substitute.For<IRandomizer>();
            randomizerMock.Next().Returns(3);

            var businessCalculator = new BusinessCalculator(randomizerMock);

            // act 
            var result = businessCalculator.CalculateBusiness();

            // assert
            Assert.AreEqual(NotStonks, result);
        }

        [Test]
        public void CalculateBusiness_EvenNumber_StonksReturned()
        {
            // arrange
            var randomizerMock = Substitute.For<IRandomizer>();
            randomizerMock.Next().Returns(6);

            var businessCalculator = new BusinessCalculator(randomizerMock);

            // act 
            var result = businessCalculator.CalculateBusiness();

            // assert
            Assert.AreEqual(Stonks, result);
        }
    }
}

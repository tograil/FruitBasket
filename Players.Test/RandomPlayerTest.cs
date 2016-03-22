using NUnit.Framework;
using Players.Players;
using Rhino.Mocks;

namespace Players.Test
{
    [TestFixture]
    public class RandomPlayerTest
    {

        [Test]
        public void PlayerShouldGuessRandomNumbersBetweenSpecifiedRange()
        {
            //given
            var player = new RandomPlayer();

            //when
            var newNumber = player.GuessNumber();

            //then
            Assert.That(newNumber, Is.GreaterThan(40));
            Assert.That(newNumber, Is.LessThan(140));

            //and when
            newNumber = player.GuessNumber();

            //then
            Assert.That(newNumber, Is.GreaterThan(40));
            Assert.That(newNumber, Is.LessThan(140));
        }

        [Test]
        public void PlayerDontRememberPreviousNumbers()
        {
            //given
            var knowNumbersStub = MockRepository.GenerateStub<IKnowNumbers>();
            knowNumbersStub.Stub(x => x.RememberNumbers(0));

            var player = new RandomPlayer(knowNumbersStub);

            //when
            player.NewNumberGuessed(45);

            //then
            knowNumbersStub.AssertWasCalled(x => x.RememberNumbers(0));
        }
    }
}

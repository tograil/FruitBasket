
using NUnit.Framework;
using Players.Container;
using Players.Players;
using Rhino.Mocks;

namespace Players.Test
{
    [TestFixture]
    public class CheaterPlayerTest
    {
        [Test]
        public void PlayerShouldGuessRandomNumbersBetweenSpecifiedRange()
        {
            //given
            var player = new CheaterPlayer();

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
        public void PlayerRememberOtherPlayersGuessing()
        {
            //given
            var knowNumbersStub = MockRepository.GenerateStub<IKnowNumbers>();
            knowNumbersStub.Stub(x => x.RememberNumbers(1));

            var player = new CheaterPlayer(knowNumbersStub);

            //when
            player.NewNumberGuessed(45);

            //then
            knowNumbersStub.AssertWasCalled(x => x.RememberNumbers(1));
        }

        [Test]
        public void PlayerDontGenerateAlreadyGuessedNumber()
        {
            //given
            var knowNumbersStub = MockRepository.GenerateStub<IKnowNumbers>();
            knowNumbersStub.Stub(x => x.RememberNumbers(1));

            var player = new CheaterPlayer(knowNumbersStub);

            //when
            player.NewNumberGuessed(41);

            //then
            knowNumbersStub.AssertWasCalled(x => x.RememberNumbers(1));

            //and when
            var newNumber = player.GuessNumber();

            //then
            //for this test I generate numbers in order
            Assert.That(newNumber, Is.EqualTo(42));
        }

        [Test]
        public void PlayerDontRememberOwnGuess()
        {
            //given
            var knowNumbersStub = MockRepository.GenerateStub<IKnowNumbers>();
            knowNumbersStub.Stub(x => x.RememberNumbers(1));

            var player = new CheaterPlayer(knowNumbersStub);
            
            player.GuessNumber();
            player.NewNumberGuessed(41);

            //then
            knowNumbersStub.AssertWasCalled(x => x.RememberNumbers(1));

            var newNumber = player.GuessNumber();

            //then
            //for this test I generate numbers in order
            Assert.That(newNumber, Is.EqualTo(42));
        }
    }
}

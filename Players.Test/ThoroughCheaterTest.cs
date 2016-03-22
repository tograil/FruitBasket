using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Players.Players;
using Rhino.Mocks;

namespace Players.Test
{
    [TestFixture]
    public class ThoroughCheaterTest
    {
        [Test]
        public void PlayerGeneratesOrderedNumbers()
        {
            //given
            var player = new ThoroughCheaterPlayer();

            //when
            var newNumber = player.GuessNumber();

            //then
            Assert.That(newNumber, Is.EqualTo(41));

            //and when
            newNumber = player.GuessNumber();

            //then
            Assert.That(newNumber, Is.EqualTo(42));
        }

        [Test]
        public void PlayerDontGenerateAlreadyGuessedNumber()
        {
            //given
            var knowNumbersStub = MockRepository.GenerateStub<IKnowNumbers>();
            knowNumbersStub.Stub(x => x.RememberNumbers(1));

            var player = new ThoroughCheaterPlayer(knowNumbersStub);

            //when
            player.NewNumberGuessed(41);

            //then
            knowNumbersStub.AssertWasCalled(x => x.RememberNumbers(1));

            //and when
            var newNumber = player.GuessNumber();

            //then
            Assert.That(newNumber, Is.EqualTo(42));
        }
    }
}

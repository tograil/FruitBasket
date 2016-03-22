using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Players.Container;
using Players.Players;
using Rhino.Mocks;

namespace Players.Test
{
    [TestFixture]
    public class ThoroughPlayerTest
    {
        [Test]
        public void PlayerGeneratesOrderedNumbers()
        {
            //given
            var player = new ThoroughPlayer();

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

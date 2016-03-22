using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players.Players
{
    public class CheaterPlayer : BasePlayer
    {
        private readonly Random _random = new Random(TotalRandomizer.GetNext());

        public CheaterPlayer() : base(null)
        {
            Randomizer = () => _random.Next(40, 140);
        }

        public CheaterPlayer(IKnowNumbers knowNumbers) : base(knowNumbers)
        {
            var val = 40;

            Randomizer = () => ++val;
        }

        public override void NewNumberGuessed(int number)
        {
            AlreadyGuessedNumbers.Add(number);
            ReportMemoryNumber();
        }

        public override int GuessNumber()
        {
            var newNumber = Randomizer();

            while (AlreadyGuessedNumbers.Contains(newNumber))
            {
                newNumber = Randomizer();
            }

            return newNumber;
        }

        public Func<int> Randomizer { get; }
    }
}

using System;

namespace Players.Players
{
    public class MemoryPlayer : BasePlayer
    {
        private readonly Random _random = new Random(TotalRandomizer.GetNext());

        public MemoryPlayer() : base(null)
        {
            
        }

        public MemoryPlayer(IKnowNumbers knowNumbers) : base(knowNumbers)
        {
        }

        public override void NewNumberGuessed(int number)
        {
            ReportMemoryNumber();
        }

        public override int GuessNumber()
        {

            var newNumber = _random.Next(40, 140);

            while (AlreadyGuessedNumbers.Contains(newNumber))
            {
                newNumber = _random.Next(40, 140);
            }

            AlreadyGuessedNumbers.Add(newNumber);

            return newNumber;
        }
    }
}

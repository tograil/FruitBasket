using System;

namespace Players.Players
{
    public class RandomPlayer : BasePlayer
    {
        private readonly Random _random = new Random(TotalRandomizer.GetNext());

        public RandomPlayer() : base(null)
        {

        }

        public RandomPlayer(IKnowNumbers knowNumbers) : base(knowNumbers)
        {

        }

        public override void NewNumberGuessed(int number)
        {
            ReportMemoryNumber();
        }

        public override int GuessNumber()
        {
            return _random.Next(41, 139);
        }

        
    }
}

using System;
using System.Collections.Generic;

namespace Players.Players
{
    public abstract class BasePlayer
    {
        protected readonly IKnowNumbers KnowNumbers;
        protected List<int> AlreadyGuessedNumbers = new List<int>();


        protected BasePlayer(IKnowNumbers knowNumbers)
        {
            KnowNumbers = knowNumbers;
        }

        protected void ReportMemoryNumber()
        {
            KnowNumbers?.RememberNumbers(AlreadyGuessedNumbers.Count);
        }
        
        public abstract void NewNumberGuessed(int number);

        public abstract int GuessNumber();
    }
}

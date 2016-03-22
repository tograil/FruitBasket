namespace Players.Players
{
    public class ThoroughCheaterPlayer : BasePlayer
    {
        private int _baseNumber = 40;

        public ThoroughCheaterPlayer() : base(null)
        {
            
        }

        public ThoroughCheaterPlayer(IKnowNumbers knowNumbers) : base(knowNumbers)
        {
        }

        public override void NewNumberGuessed(int number)
        {
            AlreadyGuessedNumbers.Add(number);
            ReportMemoryNumber();
        }

        public override int GuessNumber()
        {
            _baseNumber++;

            while (AlreadyGuessedNumbers.Contains(_baseNumber))
                _baseNumber++;

            return _baseNumber;
        }
    }
}

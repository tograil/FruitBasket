namespace Players.Players
{
    public class ThoroughPlayer : BasePlayer
    {
        private int _baseNumber = 40;

        public ThoroughPlayer() : base(null)
        {
            
        }

        public ThoroughPlayer(IKnowNumbers knowNumbers) : base(knowNumbers)
        {
        }

        public override void NewNumberGuessed(int number)
        {
            ReportMemoryNumber();
        }

        public override int GuessNumber()
        {
            return ++_baseNumber;
        }
    }
}

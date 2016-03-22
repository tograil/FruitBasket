using Players.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Players.Container
{
    public class PlayerContainer<T> where T : BasePlayer
    {
        const int TotalAttempts = 1000;
        const int TotalPenalty = 1500;

        private int _attempts = 1;
        private int _penalty = 0;
        private bool _guessingFinished = false;

        public readonly string Name;
        private readonly T _player;

        private readonly object _reportingObject = new object();

        public PlayerContainer(string name, T player)
        {
            Name = name;
            _player = player;
        }

        public event Action<string, int> Guessed;
        public event Action<string, int> GuessedIncorrectNumber;
        public event Action<string> TotalPenaltyReached;
        public event Action<string> TotalAttemptsReached;

        public void StartGuessing(int numberToGuess)
        {
            int delta;
            do
            {
                int guessingNumber;
                lock (_reportingObject)
                {
                    guessingNumber = _player.GuessNumber();
                }

                delta = Math.Abs(numberToGuess - guessingNumber);

                if (delta > 0)
                {
                    GuessedIncorrectNumber?.Invoke(Name, guessingNumber);
                    Console.WriteLine($"{Name} has a penalty {delta} miliseconds");
                    Thread.Sleep(delta);
                    _attempts++;
                    _penalty += delta;
                }

                if (_penalty >= TotalPenalty)
                {
                    TotalPenaltyReached?.Invoke(Name);
                    return;
                }

                if (_attempts >= TotalAttempts)
                {
                    TotalAttemptsReached?.Invoke(Name);
                    return;
                }

                if(_guessingFinished)
                    return;

            } while (delta != 0);

            Guessed?.Invoke(Name, _attempts);
        }

        public void NewNumberGuessed(int number)
        {
            lock (_reportingObject)
            {
                _player.NewNumberGuessed(number);
            }
            
        }

        public void GuessingFinished()
        {
            _guessingFinished = true;
        }

        public string TypeOfPlayer()
        {
            lock (_reportingObject)
            {
                return _player.ToString();
            }
        }
    }
}

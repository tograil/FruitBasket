using Players.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Players.Container;

namespace Players.Hub
{
    public enum PlayerType
    {
        Random,
        Memory,
        Thorough,
        Cheater,
        CheaterThorough
    };

    

    public class GuessingHub
    {
        private readonly Dictionary<PlayerType, Func<BasePlayer>> _playersCreators = new Dictionary
            <PlayerType, Func<BasePlayer>>
        {
            { PlayerType.Random, () => new RandomPlayer() },
            { PlayerType.Memory, () => new MemoryPlayer() },
            { PlayerType.Thorough, () => new ThoroughPlayer() },
            { PlayerType.Cheater, () => new CheaterPlayer() },
            { PlayerType.CheaterThorough, () => new ThoroughCheaterPlayer() }

        };

        private List<PlayerContainer<BasePlayer>> ParticipatingPlayers { get; }

        public GuessingHub(Dictionary<string, PlayerType> initialPlayers)
        {
            ParticipatingPlayers = initialPlayers.Select(x => new PlayerContainer<BasePlayer> (x.Key, _playersCreators[x.Value]())).ToList();
        }

        public void Guessing()
        {
            var numberToGuess = new Random().Next(41, 139);

            var closedName = string.Empty;
            var closedValue = 100;

            var finalName = string.Empty;
            var finalAttempts = 0;

            var numberGuessed = false;

            var closedLock = new object();

            Console.WriteLine($"We are guessing number {numberToGuess}");

            Console.WriteLine("Participants:");

            foreach (var participatingPlayer in ParticipatingPlayers)
            {
                Console.WriteLine($"{participatingPlayer.Name} is {participatingPlayer.TypeOfPlayer()}");

                participatingPlayer.Guessed += (name, number) => ParticipatingPlayers.ForEach(player =>
                {
                    if (player.Name != name)
                        player.GuessingFinished();

                    finalName = name;
                    finalAttempts = number;
                    numberGuessed = true;
                });

                participatingPlayer.GuessedIncorrectNumber += (name, number) =>
                {
                    ParticipatingPlayers.ForEach(player =>
                    {
                        if (player.Name != name)
                            player.NewNumberGuessed(number);

                        lock (closedLock)
                        {
                            if (closedValue <= Math.Abs(numberToGuess - number)) return;
                            closedValue = number;
                            closedName = name;
                        }


                    });

                    Console.WriteLine($"player {name} tried to guess number {number}");
                };
            
                participatingPlayer.TotalAttemptsReached += name =>
                {
                    Console.WriteLine($"player {name} reached total attempts");
                };

                participatingPlayer.TotalPenaltyReached += name =>
                {
                    Console.WriteLine($"player {name} reached total penalty");
                };
               
            }

            Task.WaitAll(ParticipatingPlayers.Select(participatingPlayer => Task.Factory.StartNew(() => participatingPlayer.StartGuessing(numberToGuess))).ToArray());

            Console.WriteLine(!numberGuessed
                ? $"{closedName} has much closest to guess"
                : $"player {finalName} guessed number in {finalAttempts} attempts");
        }
    }
}

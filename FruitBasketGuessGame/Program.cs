using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Players;
using Players.Hub;

namespace FruitBasketGuessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var players = new string[]
            {
                "John Lennon",
                "Paul McCartney",
                "George Harrison",
                "Ringo Starr",
                "Freddy Mercury",
                "Brian May",
                "Angus Young",
                "Richi Blackmore",
                "Jon Lord"
            };

            var playersCount = new Random(TotalRandomizer.GetNext()).Next(2, 8);

            var random = new Random(TotalRandomizer.GetNext());

            var participants = new Dictionary<string, PlayerType>();
            for (var i = 0; i < playersCount; i++)
            {
                int playerNumber;
                do playerNumber = random.Next(0, 8);
                while (participants.ContainsKey(players[playerNumber]));

                var playerType = (PlayerType)random.Next(0, 4);

                participants.Add(players[playerNumber], playerType);
            }

            var hub = new GuessingHub(participants);

            hub.Guessing();

            Console.ReadLine();
        }
    }
}

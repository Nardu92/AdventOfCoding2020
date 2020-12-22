using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day22
    {
        public static long Solution1(string fileName = @".\..\..\..\Day22\Input.txt")
        {
            var decks = ReadInput(fileName);
            var game = new Game(decks.Item1, decks.Item2);
            return game.Play();
        }

        public static string Solution2(string fileName = @".\..\..\..\Day21\Input.txt")
        {
            var foods = ReadInput(fileName);
            string answer = "";
            return answer[..^1];
        }


        public static Tuple<Deck,Deck> ReadInput(string fileName)
        {
            List<string> cards1 = new List<string>();
            using StreamReader inputFile = new StreamReader(fileName);
            string line = inputFile.ReadLine();
            while (!string.IsNullOrEmpty(line = inputFile.ReadLine()))
            {
                cards1.Add(line);
            }
            var deck1 = new Deck(cards1);
            List<string> cards2 = new List<string>();
            line = inputFile.ReadLine();
            while (!string.IsNullOrEmpty(line = inputFile.ReadLine()))
            {
                cards2.Add(line);
            }
            var deck2 = new Deck(cards2);
            return new Tuple<Deck, Deck>(deck1,deck2);
        }
    }

    public class Deck
    {
        public List<int> Cards;
        public Deck(List<string> cards)
        {
            Cards = cards.Select(x => Convert.ToInt32(x)).ToList();

        }

        public void PutBottom(int winnerCard, int loserCard)
        {
            Cards.Add(winnerCard);
            Cards.Add(loserCard);
        }

        public int GetTop()
        {
            var topCard = Cards.ElementAt(0);
            Cards.RemoveAt(0);
            return topCard;
        }

        public bool GameOver()
        {
            if (!Cards.Any())
            {
                return true;
            }
            return false;
        }

        public long Score()
        {
            long total = 0;
            int multiplier = Cards.Count;
            foreach (var c in Cards)
            {
                total += c * multiplier;
                multiplier--;
            }
            return total;
        }
    }

    public class Game
    {
        public Deck Player1;
        public Deck Player2;

        public Game(Deck player1, Deck player2)
        {
            Player1 = player1;
            Player2 = player2;
        }

        public bool PlayRound()
        {
            var card1 = Player1.GetTop();
            var card2 = Player2.GetTop();

            if (card1 > card2)
            {
                Player1.PutBottom(card1, card2);
            }
            else
            {
                Player2.PutBottom(card2, card1);
            }

            if (Player1.GameOver() || Player2.GameOver())
            {
                return true;
            }
            return false;
        }

        public long Play()
        {
            while (!PlayRound())
            {

            }

            return Math.Max(Player1.Score(), Player2.Score());
        }
    }
}

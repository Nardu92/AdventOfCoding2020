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
            return game.Play().Item2;
        }

        public static long Solution2(string fileName = @".\..\..\..\Day22\Input.txt")
        {
            var decks = ReadInput(fileName);
            var game = new RecursiveCombatGame(decks.Item1, decks.Item2);
            return game.Play().Item2;
        }


        public static Tuple<Deck, Deck> ReadInput(string fileName)
        {
            List<string> cards1 = new List<string>();
            using StreamReader inputFile = new StreamReader(fileName);
            string line = inputFile.ReadLine();
            while (!string.IsNullOrEmpty(line = inputFile.ReadLine()))
            {
                cards1.Add(line);
            }
            var deck1 = new Deck(PlayerId.P1, cards1);
            List<string> cards2 = new List<string>();
            line = inputFile.ReadLine();
            while (!string.IsNullOrEmpty(line = inputFile.ReadLine()))
            {
                cards2.Add(line);
            }
            var deck2 = new Deck(PlayerId.P2, cards2);
            return new Tuple<Deck, Deck>(deck1, deck2);
        }
    }

    public enum PlayerId
    {
        P1 = 1,
        P2 = 2
    }

    public class Deck
    {
        public PlayerId PlayerId;
        public List<int> Cards;
        public Deck(PlayerId playerId, List<string> cards)
        {
            Cards = cards.Select(x => Convert.ToInt32(x)).ToList();
            PlayerId = playerId;
        }
        public Deck(PlayerId playerId, List<int> cards)
        {
            Cards = cards;
            PlayerId = playerId;
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

        public override string ToString()
        {
            return $"Player {PlayerId}'s deck: {string.Join(',', Cards)}";
        }
    }

    public interface ICardCombatGame
    {
        public bool PlayRound();
        public Tuple<PlayerId, long> Play();

    }
    public class Game : ICardCombatGame
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

        public Tuple<PlayerId, long> Play()
        {
            while (!PlayRound())
            {

            }
            var p1 = Player1.Score();
            var p2 = Player2.Score();
            if (p1 > p2)
            {
                return new Tuple<PlayerId, long>(PlayerId.P1, p1);
            }
            else
            {
                return new Tuple<PlayerId, long>(PlayerId.P2, p2);
            }
        }
    }

    public class RecursiveCombatGame : ICardCombatGame
    {
        public int GameId;
        public int Round;

        public Deck Player1;
        public Deck Player2;

        public PlayerId Winner;

        private static int totalGames = 0;
        public RecursiveCombatGame(Deck player1, Deck player2)
        {
            Player1 = player1;
            Player2 = player2;
            totalGames++;
            GameId = totalGames;
            Round = 0;
        }

        private void PrintRoundInfo()
        {
            Console.WriteLine($"=== Game {GameId} ===");
            Console.WriteLine($"--Round {Round}(Game {GameId})--");
            
            Console.WriteLine(Player1);
            Console.WriteLine(Player2);
            Console.WriteLine($"Player 1 plays: {Player1.Cards.First()}");
            Console.WriteLine($"Player 2 plays: {Player2.Cards.First()}");
        }
        public bool PlayRound()
        {
            Round++;
            //PrintRoundInfo();
            if (CheckForRepetition())
            {
                Winner = PlayerId.P1;
                return true;
            }

            var card1 = Player1.GetTop();
            var card2 = Player2.GetTop();
            
            bool p1Wins = false;
            

            if (!p1Wins)
            {
                if (card1 <= Player1.Cards.Count && card2 <= Player2.Cards.Count)
                {
                    //recursive game 
                    var player1Deck = new Deck(PlayerId.P1, Player1.Cards.Take(card1).ToList());
                    var player2Deck = new Deck(PlayerId.P2, Player2.Cards.Take(card2).ToList());

                    var rcg = new RecursiveCombatGame(player1Deck, player2Deck);
                    var winner = rcg.Play();
                    if (winner.Item1 == PlayerId.P1)
                    {
                        Player1.PutBottom(card1, card2);
                        //Console.WriteLine($"Player 1 wins round {Round} of game {GameId}!");
                    }
                    else
                    {
                        Player2.PutBottom(card2, card1);
                        //Console.WriteLine($"Player 2 wins round {Round} of game {GameId}!");
                    }
                }
                else
                {
                    //regular win condition
                    if (card1 > card2)
                    {
                        //Console.WriteLine($"Player 1 wins round {Round} of game {GameId}!");
                        Player1.PutBottom(card1, card2);
                    }
                    else
                    {
                        //Console.WriteLine($"Player 2 wins round {Round} of game {GameId}!");
                        Player2.PutBottom(card2, card1);
                    }
                }
            }

            if (Player1.GameOver())
            {
                Winner = PlayerId.P2;
                //Console.WriteLine($"The winner of game {GameId} is player {Winner}!");
                return true;
            }
            else if (Player2.GameOver())
            {
                //Console.WriteLine($"The winner of game {GameId} is player {Winner}!");
                Winner = PlayerId.P1;
                return true;
            }
            else
            {
                return false;
            }
        }
        HashSet<long> PreviousRoundsScores = new HashSet<long>();
        public bool CheckForRepetition()
        {
            //this can be the hash of the decks snapshots
            var score = Player1.Score() * Player2.Score();
            if(PreviousRoundsScores.Contains(score))
            {
                return true;
            }
            else
            {
                PreviousRoundsScores.Add(score);
                return false;
            }
        }
        public Tuple<PlayerId, long> Play()
        {
            while (!PlayRound())
            {

            }

            if (Winner == PlayerId.P1)
            {
                return new Tuple<PlayerId, long>(PlayerId.P1, Player1.Score());
            }
            else
            {
                return new Tuple<PlayerId, long>(PlayerId.P2, Player2.Score());
            }
        }
    }
}

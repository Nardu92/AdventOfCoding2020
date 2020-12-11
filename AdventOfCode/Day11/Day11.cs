using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day11
    {
        public static long Solution1()
        {
            var room = ReadInput();

            Console.WriteLine(room.ToString());

            while (room.CalculateRound())
            {
                Console.WriteLine(room.ToString());
            }

            return room.GetOccupiedSeats();
        }

        public static long DaySolution2()
        {
            return 0;
        }


        private static Room ReadInput()
        {
            using StreamReader inputFile = new StreamReader(@".\..\..\..\Day11\Input.txt");
            string line;
            List<string> lines = new List<string>();
            while ((line = inputFile.ReadLine()) != null)
            {
                lines.Add(line);
            }
            return new Room(lines.ToArray());
        }
    }

    public class Room
    {
        Seat[][] Seats;

        public Room(string[] roomLayout)
        {
            var roomHeight = roomLayout.Length;
            var roomWidth = roomLayout.First().Length;
            Seats = new Seat[roomHeight][];
            for (int y = 0; y < roomHeight; y++)
            {
                Seats[y] = new Seat[roomWidth];
                for (int x = 0; x < roomWidth; x++)
                {
                    Seats[y][x] = new Seat(roomLayout[y][x]);
                }
            }

            for (int y = 0; y < roomHeight; y++)
            {
                for (int x = 0; x < roomWidth; x++)
                {
                    var currentSeat = Seats[y][x];
                    var adjecents = new List<Seat>();
                    if (y > 0 && x < roomWidth - 1)
                    {
                        Seat upRight = Seats[y - 1][x + 1];
                        if (upRight.Type != SeatType.Floor)
                            adjecents.Add(upRight);
                    }
                    if (x < roomWidth - 1)
                    {
                        Seat right = Seats[y][x + 1];
                        if (right.Type != SeatType.Floor)
                            adjecents.Add(right);
                    }
                    if (y < roomHeight - 1 && x < roomWidth - 1)
                    {
                        Seat downRight = Seats[y + 1][x + 1];
                        if (downRight.Type != SeatType.Floor)
                            adjecents.Add(downRight);
                    }
                    if (y < roomHeight - 1)
                    {
                        Seat down = Seats[y + 1][x];
                        if (down.Type != SeatType.Floor)
                            adjecents.Add(down);
                    }
                    if (y < roomHeight - 1 && x > 0)
                    {
                        Seat downLeft = Seats[y + 1][x - 1];
                        if (downLeft.Type != SeatType.Floor)
                            adjecents.Add(downLeft);
                    }
                    if (x > 0)
                    {
                        Seat left = Seats[y][x - 1];
                        if (left.Type != SeatType.Floor)
                            adjecents.Add(left);
                    }
                    if (y > 0 && x > 0)
                    {
                        Seat upLeft = Seats[y - 1][x - 1];
                        if (upLeft.Type != SeatType.Floor)
                            adjecents.Add(upLeft);
                    }
                    if (y > 0)
                    {
                        Seat up = Seats[y - 1][x];
                        if (up.Type != SeatType.Floor)
                            adjecents.Add(up);
                    }
                    currentSeat.AdjecentSeats = adjecents;
                }
            }

        }

        public bool CalculateRound()
        {
            foreach (var row in Seats)
            {
                foreach (var seat in row)
                {
                    seat.CalculateRound();
                }
            }
            bool somethingChanged = false;
            foreach (var row in Seats)
            {
                foreach (var seat in row)
                {
                    somethingChanged = seat.ApplyRound() || somethingChanged;
                }
            }
            return somethingChanged;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var row in Seats)
            {
                var line = "";
                foreach (var seat in row)
                {
                    line += seat.ToString();
                }
                sb.AppendLine(line);
            }
            return sb.ToString();

        }

        public int GetOccupiedSeats()
        {
            return  Seats.SelectMany(x => x).Count(s => s.Occupied);
        }
    }


    public class Seat
    {
        public List<Seat> AdjecentSeats { get; set; }

        public bool Occupied { get; private set; }

        public SeatType Type { get; private set; }
        public Seat(char seat)
        {
            if (seat == '.')
            {
                this.Type = SeatType.Floor;
            }
            else if (seat == 'L')
            {
                this.Type = SeatType.Seat;
                //an empty seat(L)
            }
            this.AdjecentSeats = new List<Seat>();
            Occupied = false;
        }

        bool nextState = false;
        public void CalculateRound()
        {
            if (this.Type == SeatType.Seat)
            {
                //If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
                if (!Occupied && AdjecentSeats.All(x => !x.Occupied))
                {
                    nextState = true;
                    return;
                }
                //If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
                if (Occupied && AdjecentSeats.Count(x => x.Occupied) >= 4)
                {
                    nextState = false;
                    return;
                }
                nextState = Occupied;
                return;
            }
        }

        public bool ApplyRound()
        {
            if (Occupied != nextState)
            {
                Occupied = nextState;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return Type == SeatType.Floor ? "." : Occupied ? "#" : "L";
        }
    }

    public enum SeatType
    {
        Seat,
        Floor
    }
}

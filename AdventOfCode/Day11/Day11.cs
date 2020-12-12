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
            var room = ReadInput(true);


            while (room.CalculateRound())
            {
            }

            return room.GetOccupiedSeats();
        }

        public static long Solution2()
        {
            var room = ReadInput(false);
            while (room.CalculateRound())
            {
            }
            return room.GetOccupiedSeats();
        }


        private static Room ReadInput(bool rule1)
        {
            using StreamReader inputFile = new StreamReader(@".\..\..\..\Day11\Input.txt");
            string line;
            List<string> lines = new List<string>();
            while ((line = inputFile.ReadLine()) != null)
            {
                lines.Add(line);
            }
            return new Room(lines.ToArray(), rule1);
        }
    }

    public class Room
    {
        Seat[][] Seats;
        bool Rule1;
        public Room(string[] roomLayout, bool rule1)
        {
            var roomHeight = roomLayout.Length;
            var roomWidth = roomLayout.First().Length;
            Seats = new Seat[roomHeight][];
            Rule1 = rule1;
            for (int y = 0; y < roomHeight; y++)
            {
                Seats[y] = new Seat[roomWidth];
                for (int x = 0; x < roomWidth; x++)
                {
                    Seats[y][x] = new Seat(roomLayout[y][x]);
                }
            }
            BuildAdjecents(rule1);
        }

        private void BuildAdjecents(bool rule1)
        {
            var roomHeight = Seats.Length;
            var roomWidth = Seats.First().Length;
            for (int y = 0; y < roomHeight; y++)
            {
                for (int x = 0; x < roomWidth; x++)
                {
                    var currentSeat = Seats[y][x];
                    if(currentSeat.Type == SeatType.Floor)
                    {
                        continue;
                    }
                    var adjecents = new List<Seat>();
                    //upRight
                    CalculateAdjecent(roomHeight, roomWidth, adjecents, y, x, 1, -1);

                    //Right
                    CalculateAdjecent(roomHeight, roomWidth, adjecents, y, x, 1, 0);

                    //Down Right
                    CalculateAdjecent(roomHeight, roomWidth, adjecents, y, x, 1, 1);

                    //Down
                    CalculateAdjecent(roomHeight, roomWidth, adjecents, y, x, 0, 1);

                    //Down Left
                    CalculateAdjecent(roomHeight, roomWidth, adjecents, y, x, -1, 1);

                    //Left
                    CalculateAdjecent(roomHeight, roomWidth, adjecents, y, x, -1, 0);

                    //Up Left
                    CalculateAdjecent(roomHeight, roomWidth, adjecents, y, x, -1, -1);

                    //Up
                    CalculateAdjecent(roomHeight, roomWidth, adjecents, y, x, 0, -1);

                    currentSeat.AdjecentSeats = adjecents;
                }
            }
        }

        private void CalculateAdjecent(int roomHeight, int roomWidth, List<Seat> adjecents, int initialY, int initialX, int xDirection, int yDirection)
        {
            var repeat = false;
            do
            {
                initialX += xDirection;
                initialY += yDirection;
                if (initialY >= 0 && initialY <= roomHeight - 1 && initialX >= 0 && initialX <= roomWidth - 1)
                {
                    Seat upRight = Seats[initialY][initialX];
                    if (upRight.Type != SeatType.Floor)
                    {
                        repeat = false;
                        adjecents.Add(upRight);
                    }
                    else if (!Rule1)
                    {
                        repeat = true;
                    }
                }
                else
                {
                    repeat = false;
                }
            } while (repeat);
        }

        public bool CalculateRound()
        {
            foreach (var row in Seats)
            {
                foreach (var seat in row)
                {
                    seat.CalculateRound(Rule1 ? 4: 5);
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
            return Seats.SelectMany(x => x).Count(s => s.Occupied);
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
        public void CalculateRound(int limit)
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
                if (Occupied && AdjecentSeats.Count(x => x.Occupied) >= limit)
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

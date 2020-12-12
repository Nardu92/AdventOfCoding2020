using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day12
    {
        public static long Solution1()
        {
            var route = ReadInput(false);

            return Math.Abs(route.Destination.X) + Math.Abs(route.Destination.Y);
        }

        public static long Solution2()
        {
            return 0;
        }


        private static Route ReadInput(bool rule2)
        {
            using StreamReader inputFile = new StreamReader(@".\..\..\..\Day12\Input.txt");
            string line;
            List<string> lines = new List<string>();
            while ((line = inputFile.ReadLine()) != null)
            {
                lines.Add(line);
            }
            return new Route(lines);
        }
    }


    public class Route
    {
        public Segment[] Segments { get; private set; }

        public Route(List<string> input)
        {
            var l = new List<Segment>();
            l.Add(new Segment(OrientationEnum.E));
            foreach (string segmentString in input)
            {
                l.Add(new Segment(segmentString, l.LastOrDefault()));
            }
            
            Segments = l.ToArray();
        }

        public Point Destination => Segments.Last().End;

        public IEnumerable<Segment> OrizontalSegments => Segments.Where(x => x.Direction == Direction.Orizontal);
        public IEnumerable<Segment> VerticalSegments => Segments.Where(x => x.Direction == Direction.Vertical);
    }

    public class Segment
    {
        public OrientationEnum Orientation { get; private set; }

        public OrientationEnum MovingOrientation { get; private set; }

        public Direction Direction => Orientation == OrientationEnum.E || Orientation == OrientationEnum.W ? Direction.Orizontal : Direction.Vertical;
        public int Lenght { get; private set; }

        public Point Start { get; private set; }
        public Point End { get; private set; }

        public Segment PreviousSegment { get; private set; }

        public Segment(OrientationEnum movingOrientation)
        {
            this.Orientation = movingOrientation;
            this.Lenght = 0;
            this.MovingOrientation = movingOrientation;
            Start = new Point(0, 0);
            End = new Point(0, 0);

        }
        public Segment(string segment, Segment previousSegment)
        {
            Orientation = (OrientationEnum)Enum.Parse(typeof(OrientationEnum), segment[0].ToString());
            Lenght = Convert.ToInt32(segment[1..]);
            PreviousSegment = previousSegment;
            if (previousSegment != null)
            {
                Start = previousSegment.End;
            }
            else
            {
                Start = new Point(0, 0);
            }
            FixOrientation();
            End = CalculateEndPoint(Start, Orientation, Lenght);
        }

        private void FixOrientation()
        {
            switch (this.Orientation)
            {
                case OrientationEnum.N:
                case OrientationEnum.E:
                case OrientationEnum.S:
                case OrientationEnum.W:
                    //Orientation is fine already, continue on previous orientation
                    MovingOrientation = PreviousSegment.MovingOrientation;
                    break;
                case OrientationEnum.L:
                    MovingOrientation = PreviousSegment.MovingOrientation;
                    PerformTurn(Lenght / 90);
                    Lenght = 0;
                    Orientation = MovingOrientation;
                    break;
                case OrientationEnum.R:
                    MovingOrientation = PreviousSegment.MovingOrientation;
                    PerformTurn(4 - Lenght / 90);
                    Lenght = 0;
                    Orientation = MovingOrientation;
                    break;
                case OrientationEnum.F:
                    MovingOrientation = PreviousSegment.MovingOrientation;
                    Orientation = MovingOrientation;
                    break;
                default:
                    break;
            }
        }

        private void PerformTurn(int leftTurns)
        {
            for (int i = 0; i < leftTurns; i++)
            {
                if (MovingOrientation == OrientationEnum.E)
                {
                    MovingOrientation = OrientationEnum.N;
                }
                else if (MovingOrientation == OrientationEnum.N)
                {
                    MovingOrientation = OrientationEnum.W;
                }
                else if (MovingOrientation == OrientationEnum.W)
                {
                    MovingOrientation = OrientationEnum.S;
                }
                else if (MovingOrientation == OrientationEnum.S)
                {
                    MovingOrientation = OrientationEnum.E;
                }
            }
        }


        private static Point CalculateEndPoint(Point start, OrientationEnum d, int lenght)
        {
            return d switch
            {
                OrientationEnum.N => new Point(start.X, start.Y + lenght),
                OrientationEnum.E => new Point(start.X + lenght, start.Y),
                OrientationEnum.S => new Point(start.X, start.Y - lenght),
                OrientationEnum.W => new Point(start.X - lenght, start.Y),
                _ => throw new Exception("Orientation not calculated correctly"),
            };
        }
    }

    public enum OrientationEnum
    {
        N,
        E,
        S,
        W,
        L,
        R,
        F
    }

    public enum Direction
    {
        Vertical,
        Orizontal,
    }
}

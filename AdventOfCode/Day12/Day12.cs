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
            var lines = ReadInput();
            var route = new Route(lines);
            return Math.Abs(route.Destination.X) + Math.Abs(route.Destination.Y);
        }

        public static long Solution2()
        {
            var lines = ReadInput();
            var route = new RouteWithWaypoint(lines);
            return Math.Abs(route.Destination.X) + Math.Abs(route.Destination.Y);
        }


        private static List<string> ReadInput()
        {
            using StreamReader inputFile = new StreamReader(@".\..\..\..\Day12\Input.txt");
            string line;
            List<string> lines = new List<string>();
            while ((line = inputFile.ReadLine()) != null)
            {
                lines.Add(line);
            }
            return lines;
        }
    }
    public class RouteWithWaypoint
    {
        private Point Waypoint;

        private Point Position;

        public RouteWithWaypoint(List<string> input)
        {
            Waypoint = new Point(10, 1);
            Position = new Point(0, 0);
            foreach (string command in input)
            {
                OrientationEnum orientation = (OrientationEnum)Enum.Parse(typeof(OrientationEnum), command[0].ToString());
                int length = Convert.ToInt32(command[1..]);
                ChangeWaypoint(orientation, length);

                if (orientation == OrientationEnum.F)
                {
                    for (int i = 0; i < length; i++)
                    {
                        Position.X += Waypoint.X;
                        Position.Y += Waypoint.Y;
                    }
                }
            }
        }

        private void ChangeWaypoint(OrientationEnum orientation, int length)
        {
            switch (orientation)
            {
                case OrientationEnum.N:
                    Waypoint.Y = Waypoint.Y + length;
                    break;
                case OrientationEnum.E:
                    Waypoint.X = Waypoint.X + length;
                    break;
                case OrientationEnum.S:
                    Waypoint.Y = Waypoint.Y - length;
                    break;
                case OrientationEnum.W:
                    Waypoint.X = Waypoint.X - length;
                    break;
                case OrientationEnum.L:
                    RotateWaypoint(length);
                    break;
                case OrientationEnum.R:
                    RotateWaypoint(-length);
                    break;
                case OrientationEnum.F:
                default:
                    break;
            }
        }

        private void RotateWaypoint(int angleDegrees)
        {
            double angle = (double)angleDegrees / 180 * Math.PI;
            var sin = (int)Math.Sin(angle);
            var cos = (int)Math.Cos(angle);

            var xnew = Waypoint.X * cos - Waypoint.Y * sin;
            var ynew = Waypoint.X * sin + Waypoint.Y * cos;

            Waypoint.X = xnew;
            Waypoint.Y = ynew;
        }
        public Point Destination => Position;
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
    }

    public class Segment
    {
        public OrientationEnum Orientation { get; private set; }

        public OrientationEnum MovingOrientation { get; private set; }

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

        public Segment(Point waypoint, Segment previousSegment)
        {

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
}

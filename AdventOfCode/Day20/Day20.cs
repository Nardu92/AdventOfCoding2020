using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day20
    {
        public static long Solution1(string fileName = @".\..\..\..\Day20\Input.txt")
        {
            var input = ReadInput(fileName);
            long total = 0;
            return total;

        }



        public static long Solution2(string fileName = @".\..\..\..\Day20\Input.txt")
        {
            var input = ReadInput(fileName);
            long total = 0;
            return total;


        }


        public static Dictionary<int, Picture> ReadInput(string fileName)
        {
            List<Picture> picturesList = new List<Picture>();
            using StreamReader inputFile = new StreamReader(fileName);
            List<string> pictures = new List<string>();
            string title = inputFile.ReadLine();
            do
            {
                string line;
                while (!string.IsNullOrEmpty(line = inputFile.ReadLine()))
                {
                    pictures.Add(line);
                }

                picturesList.Add(new Picture(title, pictures));
            } while (!string.IsNullOrEmpty(title = inputFile.ReadLine()));

            return picturesList.ToDictionary(x => x.Id, x => x);
        }
    }

    public class Picture
    {
        public int Id { get; private set; }

        public char[][] Pixels { get; private set; }

        int Height = 10;
        int Width = 10;

        public Picture(string id, List<string> pic)
        {
            //id = Tile 2473:
            var indexOfSpace = id.IndexOf(' ') + 1;
            var indexOfSC = id.IndexOf(':');
            Id = Convert.ToInt32(id[indexOfSpace..indexOfSC]);

            Pixels = new char[Height][];

            for (int i = 0; i < Height; i++)
            {
                Pixels[i] = new char[Width];
                var line = pic.ElementAt(i);
                for (int j = 0; j < Width; j++)
                {
                    Pixels[i][j] = line[j];
                }
            }
        }

        public int GetTopBorderId()
        {
            string border = "";
            foreach (char p in Pixels[0])
            {
                border += p;
            }
            return GetBorderId(border);
        }

        public int GetBottomBorderId()
        {
            string border = "";
            foreach (char p in Pixels[Height - 1])
            {
                border += p;
            }
            return GetBorderId(border);
        }

        public int GetRightBorderId()
        {
            string border = "";
            foreach (var item in Pixels)
            {
                border += item.Last();
            }
            return GetBorderId(border);
        }

        public int GetLeftBorderId()
        {
            string border = "";
            foreach (var item in Pixels)
            {
                border += item.First();
            }
            return GetBorderId(border);
        }

        private static int GetBorderId(string border)
        {
            return Convert.ToInt32(border.Replace('.', '0').Replace('#', '1'), 2);
        }

        public bool TopBorderMatchesBottom(Picture picture)
        {
            var topId = this.GetTopBorderId();
            var bottomId = picture.GetBottomBorderId();
            if (topId == bottomId)
            {
                return true;
            }
            if (topId == picture.GetTopBorderId())
            {
                picture.MirrorVertically();
                return true;
            }

            //var mirroredBottomId = Convert.ToInt32(new string(Convert.ToString(rigId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);

            return false;
        }


        public int GetReverseId(int id)
        {
            return Convert.ToInt32(new string(Convert.ToString(id, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
        }

        public int GetReverseId(string id)
        {
            return Convert.ToInt32(new string(id.Replace('.', '0').Replace('#', '1').Reverse().ToArray()), 2);
        }

        public void MirrorVertically()
        {
            char[][] tempPixels = new char[Height][];

            for (int i = 0; i < Height; i++)
            {
                tempPixels[i] = new char[Width];
                for (int j = 0; j < Width; j++)
                {
                    tempPixels[i][j] = Pixels[i][Width - 1 - j];
                }
            }

            Pixels = tempPixels;
        }


        public void MirrorHorizzontally()
        {
            char[][] tempPixels = new char[Height][];

            for (int i = 0; i < Height; i++)
            {
                tempPixels[i] = new char[Width];
                for (int j = 0; j < Width; j++)
                {
                    tempPixels[i][j] = Pixels[Height - 1 - i][j];
                }
            }

            Pixels = tempPixels;
        }

        public void RotateClockwise()
        {
            char[][] tempPixels = new char[Height][];

            for (int i = 0; i < Height; i++)
            {
                tempPixels[i] = new char[Width];
            }

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    tempPixels[j][Height - i - 1] = Pixels[i][j];
                }
            }

            Pixels = tempPixels;
        }

    }
}

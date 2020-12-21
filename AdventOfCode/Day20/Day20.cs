using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day20
    {
        public static long Solution1(string fileName = @".\..\..\..\Day20\Input.txt", int startingID = 3257)
        {
            var input = ReadInput(fileName);
            var picturesByBorders = new Dictionary<int, List<Picture>>();
            foreach (var kvp in input)
            {
                foreach (var id in kvp.Value.AllPossibleIds)
                {
                    if(!picturesByBorders.TryGetValue(id, out var picList))
                    {
                        picList = new List<Picture>();
                        picturesByBorders[id] = picList;
                    }
                    picList.Add(kvp.Value);
                }
            }

            Dictionary<int, int> numberOfCommonBordersByPictureId = new Dictionary<int, int>();

            List<Picture> edges = new List<Picture>();
            foreach(Picture p in input.Values)
            {
                var count = 0;
                foreach( var id in p.AllPossibleIds)
                {
                    if(picturesByBorders[id].Count == 1)
                    {
                        count++;
                    }
                }
                numberOfCommonBordersByPictureId[p.Id] = 4 - count;
            }

            var l = numberOfCommonBordersByPictureId.Where(x => x.Value == 0).Select(x => x.Key).ToList();

            long total = 1;
            l.ForEach(x => total *= x);
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
            string title = inputFile.ReadLine();
            do
            {
                List<string> pictures = new List<string>();
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

        public Picture Top { get; set; }
        public Picture Bottom { get; set; }
        public Picture Left { get; set; }
        public Picture Right { get; set; }

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

        private List<int> allPossibleIds; 
        public List<int> AllPossibleIds { 
            
            get {
                /*if(allPossibleIds == null)
                {
                    allPossibleIds = GetAllPossibleIds();
                }
                */
                return GetAllPossibleIds();
            }
        }

        public List<int> GetAllPossibleIds()
        {
            var ids = new List<int>();
            
            var t = GetTopBorderId();
            ids.Add(t);
            ids.Add(GetReverseId(t));
            
            var b = GetBottomBorderId();
            ids.Add(b);
            ids.Add(GetReverseId(b));

            var r = GetRightBorderId();
            ids.Add(r);
            ids.Add(GetReverseId(r));

            var l = GetLeftBorderId();
            ids.Add(l);
            ids.Add(GetReverseId(l));

            return ids;
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

        public override string ToString()
        {
            return $"Pic '{Id}'";
            return base.ToString();
        }
    }
}

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
            Dictionary<int, List<Picture>> picturesByBorders = GetPicturesByBorders(input);

            long total = 1;
            CornersIds(input, picturesByBorders).ForEach(x => total *= x);
            return total;

        }

        private static List<int> CornersIds(Dictionary<int, Picture> input, Dictionary<int, List<Picture>> picturesByBorders)
        {
            Dictionary<int, int> numberOfCommonBordersByPictureId = new Dictionary<int, int>();

            foreach (Picture p in input.Values)
            {
                var count = 0;
                foreach (var id in p.AllPossibleIds)
                {
                    if (picturesByBorders[id].Count == 1)
                    {
                        count++;
                    }
                }
                numberOfCommonBordersByPictureId[p.Id] = 4 - count;
            }

            var l = numberOfCommonBordersByPictureId.Where(x => x.Value == 0).Select(x => x.Key).ToList();
            return l;
        }

        private static Dictionary<int, List<Picture>> GetPicturesByBorders(Dictionary<int, Picture> input)
        {
            var picturesByBorders = new Dictionary<int, List<Picture>>();
            foreach (var kvp in input)
            {
                foreach (var id in kvp.Value.AllPossibleIds)
                {
                    if (!picturesByBorders.TryGetValue(id, out var picList))
                    {
                        picList = new List<Picture>();
                        picturesByBorders[id] = picList;
                    }
                    picList.Add(kvp.Value);
                }
            }

            return picturesByBorders;
        }

        public static long Solution2(string fileName = @".\..\..\..\Day20\Input.txt")
        {

            var picturesById = ReadInput(fileName);
            Dictionary<int, List<Picture>> picturesByBorders = GetPicturesByBorders(picturesById);
            var corners = CornersIds(picturesById, picturesByBorders);

            var topLeftCornerId = corners.First();

            var mosaic = BuildImage(picturesById, picturesByBorders, topLeftCornerId);

            long total = 1;
            return total;

        }

        private static Picture BuildImage(Dictionary<int, Picture> picturesById, Dictionary<int, List<Picture>> picturesByBorders, int topLeftCornerId)
        {
            var firstOfRow = picturesById[topLeftCornerId];
            //pick on one of the corners and rotate it to be the top left one
            EnsureChosenCornerIsTopLeft(picturesById, picturesByBorders, firstOfRow);
            var width = Math.Sqrt(picturesById.Count);
            for (int i = 0; i < width - 1; i++)
            {
                SetRow(picturesById, picturesByBorders, firstOfRow);
                FindBottomImage(picturesById, picturesByBorders, firstOfRow);
                firstOfRow = firstOfRow.Bottom;
            }
            SetRow(picturesById, picturesByBorders, firstOfRow);

            var mosaic = new Picture(0, picturesById, topLeftCornerId);
            return mosaic;
        }

        private static void SetRow(Dictionary<int, Picture> picturesById, Dictionary<int, List<Picture>> picturesByBorders, Picture firstOfTheRow)
        {
            var current = firstOfTheRow;
            var width = Math.Sqrt(picturesById.Count);
            for (int i = 0; i < width - 1; i++)
            {
                FindRightImage(picturesById, picturesByBorders, current);
                current = current.Right;
            }
        }

        private static void FindRightImage(Dictionary<int, Picture> picturesById, Dictionary<int, List<Picture>> picturesByBorders, Picture picture)
        {
            int matchingId = picturesByBorders[picture.GetRightBorderId()].Select(x => x.Id).Where(x => x != picture.Id).Single();
            var rightBorderId = picture.GetRightBorderId();
            for (int i = 0; i < 4; i++)
            {
                var righPicture = picturesById[matchingId];
                if (righPicture.GetLeftBorderId() == rightBorderId)
                {
                    picture.Right = righPicture;
                    righPicture.Left = picture;
                    if (picture.Top != null)
                    {
                        righPicture.Top = picture.Top.Right;
                        picture.Top.Right.Bottom = righPicture;
                    }

                    break;
                }
                if (righPicture.GetReverseId(righPicture.GetLeftBorderId()) == rightBorderId)
                {
                    picture.Right = righPicture;
                    righPicture.Left = picture;
                    righPicture.MirrorHorizzontally();
                    if (picture.Top != null)
                    {
                        righPicture.Top = picture.Top.Right;
                        picture.Top.Right.Bottom = righPicture;
                    }
                    break;
                }
                righPicture.RotateClockwise();
            }
        }

        private static void FindBottomImage(Dictionary<int, Picture> picturesById, Dictionary<int, List<Picture>> picturesByBorders, Picture picture)
        {
            int matchingId = picturesByBorders[picture.GetBottomBorderId()].Select(x => x.Id).Where(x => x != picture.Id).Single();
            var bottomId = picture.GetBottomBorderId();
            for (int i = 0; i < 4; i++)
            {
                var bottomPicture = picturesById[matchingId];
                if (bottomPicture.GetTopBorderId() == bottomId)
                {
                    picture.Bottom = bottomPicture;
                    bottomPicture.Top = picture;
                    break;
                }
                if (bottomPicture.GetReverseId(bottomPicture.GetTopBorderId()) == bottomId)
                {
                    picture.Bottom = bottomPicture;
                    bottomPicture.Top = picture;
                    bottomPicture.MirrorVertically();
                    break;
                }
                bottomPicture.RotateClockwise();
            }
        }

        private static void EnsureChosenCornerIsTopLeft(Dictionary<int, Picture> picturesById, Dictionary<int, List<Picture>> picturesByBorders, Picture current)
        {
            for (int i = 0; i < 2; i++)
            {
                if (picturesByBorders[current.GetTopBorderId()].Count == 2)
                {
                    current.RotateClockwise();
                }
            }
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

        public int Height { get; private set; }
        public int Width { get; private set; }

        public Picture(string id, List<string> pic)
        {
            //id = Tile 2473:
            var indexOfSpace = id.IndexOf(' ') + 1;
            var indexOfSC = id.IndexOf(':');
            Id = Convert.ToInt32(id[indexOfSpace..indexOfSC]);
            Height = pic.Count;
            Width = pic.First().Length;
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

        //Constructor for mosaic
        public Picture(int id, Dictionary<int, Picture> picturesById, int idOfTopLeftCorner)
        {
            Id = id;
            var firstOfRow = picturesById[idOfTopLeftCorner];
            int picturesPerSide = (int)Math.Sqrt(picturesById.Count);
            int sideLength = picturesPerSide * (firstOfRow.Width - 2);
            Pixels = new char[sideLength][];
            for (int i = 0; i < sideLength; i++)
            {
                Pixels[i] = new char[sideLength];
            }

            Height = sideLength;
            Width = sideLength;
            int x;
            int y = 0;
            var currentPicture = firstOfRow;
            int xIncrement = currentPicture.Width - 2;
            int yIncrement = currentPicture.Height - 2;
            for (int i = 0; i < picturesPerSide; i++)
            {
                x = 0;
                for (int k = 0; k < picturesPerSide; k++)
                {
                    CopyInnerPictureWithoutBorders(currentPicture, x, y);
                    x += xIncrement;
                    currentPicture = currentPicture.Right;
                }

                y += yIncrement;
                currentPicture = firstOfRow.Bottom;
                firstOfRow = firstOfRow.Bottom;
            }

        }

        private void CopyInnerPictureWithoutBorders(Picture picture, int x, int y)
        {
            for (int k = 1; k < picture.Height - 1; k++)
            {
                for (int l = 1; l < picture.Width - 1; l++)
                {
                    Pixels[y + k - 1][x + l - 1] = picture.Pixels[k][l];
                }
            }
        }

        private List<int> allPossibleIds;
        public List<int> AllPossibleIds
        {

            get
            {
                if (allPossibleIds == null)
                {
                    allPossibleIds = GetAllPossibleIds();
                }

                return allPossibleIds;
            }
        }

        public int GetHashNumber(int borderWidth)
        {
            int total = 0;
            for (int i = borderWidth; i < (Height - borderWidth); i++)
            {
                for (int j = borderWidth; j < Width - borderWidth; j++)
                {
                    if (Pixels[i][j] == '#')
                    {
                        total++;
                    }
                }
            }
            return total;
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
        }
    }
}

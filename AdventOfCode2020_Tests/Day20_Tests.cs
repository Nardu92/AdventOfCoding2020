using AdventOfCode;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace AdventOfCode2020_Tests
{
    public class Day20_Tests
    {
        [Fact]
        private static void Test_BorderId()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Test1.txt";
            var pictures = Day20.ReadInput(fileName);
            /*
            Tile 2311:
            0011010010
            ..##.#..#. 0001011001
            ##..#.....
            #...##..#.
            ####.#...#
            ##.##.###.
            ##...#.###
            .#.#.#..##
            ..#....#..
            ###...#.#.
            ..###..###
            
            0011100111

            left: 0111110010
             */
            var p = pictures[2311];
            var topId = 210;
            var botId = 231;
            var lefId = 498;
            var rigId = 89;
            Assert.Equal(topId, p.GetTopBorderId());
            Assert.Equal(rigId, p.GetRightBorderId());
            Assert.Equal(botId, p.GetBottomBorderId());
            Assert.Equal(lefId, p.GetLeftBorderId());
        }

        [Fact]
        private static void Test_MirrorHorizzontally()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Test1.txt";
            var pictures = Day20.ReadInput(fileName);
            /*
            Tile 2311:
            ..##.#..#.
            ##..#.....
            #...##..#.
            ####.#...#
            ##.##.###.
            ##...#.###
            .#.#.#..##
            ..#....#..
            ###...#.#.
            ..###..###
             */
            var p = pictures[2311];
            p.MirrorHorizzontally();

            /*
            Tile 2311:
            10 ..###..### 1001101000
            9 ###...#.#.
            8 ..#....#..
            7 .#.#.#..##
            6 ##...#.###
            5 ##.##.###.
            4 ####.#...#
            3 #...##..#.
            2 ##..#.....
            1 ..##.#..#.
             */

            var topId = 210;
            var botId = 231;
            var lefId = 498;
            var rigId = 89;

            var mirroredRigId = Convert.ToInt32(new string(Convert.ToString(rigId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
            var mirroredLefId = Convert.ToInt32(new string(Convert.ToString(lefId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
            Assert.Equal(botId, p.GetTopBorderId());
            Assert.Equal(mirroredRigId, p.GetRightBorderId());
            Assert.Equal(topId, p.GetBottomBorderId());
            Assert.Equal(mirroredLefId, p.GetLeftBorderId());
        }

        [Fact]
        private static void Test_MirrorHorizzontallyTwice()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Test1.txt";
            var pictures = Day20.ReadInput(fileName);
            /*
            Tile 2311:
            ..##.#..#.
            ##..#.....
            #...##..#.
            ####.#...#
            ##.##.###.
            ##...#.###
            .#.#.#..##
            ..#....#..
            ###...#.#.
            ..###..###
             */
            var p = pictures[2311];
            p.MirrorHorizzontally();
            p.MirrorHorizzontally();

            var topId = 210;
            var botId = 231;
            var lefId = 498;
            var rigId = 89;
            Assert.Equal(topId, p.GetTopBorderId());
            Assert.Equal(rigId, p.GetRightBorderId());
            Assert.Equal(botId, p.GetBottomBorderId());
            Assert.Equal(lefId, p.GetLeftBorderId());
        }

        [Fact]
        private static void Test_MirrorVertically()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Test1.txt";
            var pictures = Day20.ReadInput(fileName);
            /*
            Tile 2311:
            ..##.#..#.
            ##..#.....
            #...##..#.
            ####.#...#
            ##.##.###.
            ##...#.###
            .#.#.#..##
            ..#....#..
            ###...#.#.
            ..###..###
             */
            var p = pictures[2311];
            p.MirrorVertically();

            var topId = 210;
            var botId = 231;
            var lefId = 498;
            var rigId = 89;

            var mirroredTopId = Convert.ToInt32(new string(Convert.ToString(topId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
            var mirroredBottomId = Convert.ToInt32(new string(Convert.ToString(botId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
            Assert.Equal(mirroredTopId, p.GetTopBorderId());
            Assert.Equal(lefId, p.GetRightBorderId());
            Assert.Equal(mirroredBottomId, p.GetBottomBorderId());
            Assert.Equal(rigId, p.GetLeftBorderId());
        }

        [Fact]
        private static void Test_MirrorVerticallyTwice()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Test1.txt";
            var pictures = Day20.ReadInput(fileName);
            /*
            Tile 2311:
            ..##.#..#.
            ##..#.....
            #...##..#.
            ####.#...#
            ##.##.###.
            ##...#.###
            .#.#.#..##
            ..#....#..
            ###...#.#.
            ..###..###
             */
            var p = pictures[2311];
            p.MirrorVertically();
            p.MirrorVertically();

            var topId = 210;
            var botId = 231;
            var lefId = 498;
            var rigId = 89;
            Assert.Equal(topId, p.GetTopBorderId());
            Assert.Equal(rigId, p.GetRightBorderId());
            Assert.Equal(botId, p.GetBottomBorderId());
            Assert.Equal(lefId, p.GetLeftBorderId());
        }

        [Fact]
        private static void Test_RotateOnce()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Test1.txt";
            var pictures = Day20.ReadInput(fileName);

            /*
            Tile 2311:
            ..##.#..#.
            ##..#.....
            #...##..#.
            ####.#...#
            ##.##.###.
            ##...#.###
            .#.#.#..##
            ..#....#..
            ###...#.#.
            ..###..###
             */

            var p = pictures[2311];

            var topId = 210;
            var botId = 231;
            var lefId = 498;
            var rigId = 89;

            var mirroredRigId = Convert.ToInt32(new string(Convert.ToString(rigId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
            var mirroredLefId = Convert.ToInt32(new string(Convert.ToString(lefId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);

            p.RotateClockwise();

            Assert.Equal(mirroredLefId, p.GetTopBorderId());
            Assert.Equal(topId, p.GetRightBorderId());
            Assert.Equal(mirroredRigId, p.GetBottomBorderId());
            Assert.Equal(botId, p.GetLeftBorderId());
        }

        [Fact]
        private static void Test_Rotate4()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Test1.txt";
            var pictures = Day20.ReadInput(fileName);

            /*
            Tile 2311:
            ..##.#..#.
            ##..#.....
            #...##..#.
            ####.#...#
            ##.##.###.
            ##...#.###
            .#.#.#..##
            ..#....#..
            ###...#.#.
            ..###..###
             */

            var p = pictures[2311];

            var topId = 210;
            var botId = 231;
            var lefId = 498;
            var rigId = 89;

            var mirroredRigId = Convert.ToInt32(new string(Convert.ToString(rigId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
            var mirroredLefId = Convert.ToInt32(new string(Convert.ToString(lefId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);

            p.RotateClockwise();
            p.RotateClockwise();
            p.RotateClockwise();
            p.RotateClockwise();

            Assert.Equal(topId, p.GetTopBorderId());
            Assert.Equal(rigId, p.GetRightBorderId());
            Assert.Equal(botId, p.GetBottomBorderId());
            Assert.Equal(lefId, p.GetLeftBorderId());
        }

        [Fact]
        private static void Test_AllPossibleIds()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Test1.txt";
            var pictures = Day20.ReadInput(fileName);

            /*
            Tile 2311:
            ..##.#..#.
            ##..#.....
            #...##..#.
            ####.#...#
            ##.##.###.
            ##...#.###
            .#.#.#..##
            ..#....#..
            ###...#.#.
            ..###..###
             */

            var p = pictures[2311];

            var topId = 210;
            var botId = 231;
            var lefId = 498;
            var rigId = 89;

            var mirroredTopId = Convert.ToInt32(new string(Convert.ToString(topId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
            var mirroredBottomId = Convert.ToInt32(new string(Convert.ToString(botId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
            var mirroredRigId = Convert.ToInt32(new string(Convert.ToString(rigId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
            var mirroredLefId = Convert.ToInt32(new string(Convert.ToString(lefId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);

            var ids = p.GetAllPossibleIds();

            Assert.Equal(8, ids.Count);
            Assert.Contains(topId, ids);
            Assert.Contains(botId, ids);
            Assert.Contains(lefId, ids);
            Assert.Contains(rigId, ids);
            Assert.Contains(mirroredTopId, ids);
            Assert.Contains(mirroredBottomId, ids);
            Assert.Contains(mirroredRigId, ids);
            Assert.Contains(mirroredLefId, ids);
        }

        [Fact]
        private static void Test_AllPossibleIdsAfterRotation()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Test1.txt";
            var pictures = Day20.ReadInput(fileName);

            /*
            Tile 2311:
            ..##.#..#.
            ##..#.....
            #...##..#.
            ####.#...#
            ##.##.###.
            ##...#.###
            .#.#.#..##
            ..#....#..
            ###...#.#.
            ..###..###
             */

            var p = pictures[2311];

            var topId = 210;
            var botId = 231;
            var lefId = 498;
            var rigId = 89;

            var mirroredTopId = Convert.ToInt32(new string(Convert.ToString(topId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
            var mirroredBottomId = Convert.ToInt32(new string(Convert.ToString(botId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
            var mirroredRigId = Convert.ToInt32(new string(Convert.ToString(rigId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
            var mirroredLefId = Convert.ToInt32(new string(Convert.ToString(lefId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);

            p.RotateClockwise();
            var ids = p.GetAllPossibleIds();

            Assert.Equal(8, ids.Count);
            Assert.Contains(topId, ids);
            Assert.Contains(botId, ids);
            Assert.Contains(lefId, ids);
            Assert.Contains(rigId, ids);
            Assert.Contains(mirroredTopId, ids);
            Assert.Contains(mirroredBottomId, ids);
            Assert.Contains(mirroredRigId, ids);
            Assert.Contains(mirroredLefId, ids);
        }

        [Fact]
        private static void Test_AllPossibleIdsAfterHorMirror()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Test1.txt";
            var pictures = Day20.ReadInput(fileName);

            /*
            Tile 2311:
            ..##.#..#.
            ##..#.....
            #...##..#.
            ####.#...#
            ##.##.###.
            ##...#.###
            .#.#.#..##
            ..#....#..
            ###...#.#.
            ..###..###
             */

            var p = pictures[2311];

            var topId = 210;
            var botId = 231;
            var lefId = 498;
            var rigId = 89;

            var mirroredTopId = Convert.ToInt32(new string(Convert.ToString(topId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
            var mirroredBottomId = Convert.ToInt32(new string(Convert.ToString(botId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
            var mirroredRigId = Convert.ToInt32(new string(Convert.ToString(rigId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
            var mirroredLefId = Convert.ToInt32(new string(Convert.ToString(lefId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);

            p.MirrorHorizzontally();
            var ids = p.GetAllPossibleIds();

            Assert.Equal(8, ids.Count);
            Assert.Contains(topId, ids);
            Assert.Contains(botId, ids);
            Assert.Contains(lefId, ids);
            Assert.Contains(rigId, ids);
            Assert.Contains(mirroredTopId, ids);
            Assert.Contains(mirroredBottomId, ids);
            Assert.Contains(mirroredRigId, ids);
            Assert.Contains(mirroredLefId, ids);
        }

        [Fact]
        private static void Test_AllPossibleIdsAfterVertMirror()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Test1.txt";
            var pictures = Day20.ReadInput(fileName);

            /*
            Tile 2311:
            ..##.#..#.
            ##..#.....
            #...##..#.
            ####.#...#
            ##.##.###.
            ##...#.###
            .#.#.#..##
            ..#....#..
            ###...#.#.
            ..###..###
             */

            var p = pictures[2311];

            var topId = 210;
            var botId = 231;
            var lefId = 498;
            var rigId = 89;

            var mirroredTopId = Convert.ToInt32(new string(Convert.ToString(topId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
            var mirroredBottomId = Convert.ToInt32(new string(Convert.ToString(botId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
            var mirroredRigId = Convert.ToInt32(new string(Convert.ToString(rigId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);
            var mirroredLefId = Convert.ToInt32(new string(Convert.ToString(lefId, 2).PadLeft(10, '0').Reverse().ToArray()), 2);

            p.MirrorVertically();
            var ids = p.GetAllPossibleIds();

            Assert.Equal(8, ids.Count);
            Assert.Contains(topId, ids);
            Assert.Contains(botId, ids);
            Assert.Contains(lefId, ids);
            Assert.Contains(rigId, ids);
            Assert.Contains(mirroredTopId, ids);
            Assert.Contains(mirroredBottomId, ids);
            Assert.Contains(mirroredRigId, ids);
            Assert.Contains(mirroredLefId, ids);
        }

        [Fact]
        private static void Test_Sol1()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Input.txt";
            var actual = Day20.Solution1(fileName);
            var expected = 14986175499719;
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void Test_Test1_Sol1()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Test1.txt";
            var actual = Day20.Solution1(fileName);
            var expected = 20899048083289;
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void Test_Sol2()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Input.txt";
            var actual = Day20.Solution2(fileName);
            var expected = 2506 - 15 *23;
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void Test_Test1_Sol2()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Test1.txt";
            var actual = Day20.Solution2(fileName);
            var expected = 273;
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void Test_GetHashNum()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Test1.txt";
            var pictures = Day20.ReadInput(fileName);
            /*
            Tile 2311:
            0011010010
            ..##.#..#. 0001011001
            ##..#.....
            #...##..#.
            ####.#...#
            ##.##.###.
            ##...#.###
            .#.#.#..##
            ..#....#..
            ###...#.#.
            ..###..###
            
            0011100111

            left: 0111110010
             */
            var p = pictures[2311];
            Assert.Equal(29, p.GetHashNumber(1));
        }

        [Fact]
        private static void Test_TotalGetHashNum()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Test1.txt";
            var pictures = Day20.ReadInput(fileName);
            /*
            Tile 2311:
            0011010010
            ..##.#..#. 0001011001
            ##..#.....
            #...##..#.
            ####.#...#
            ##.##.###.
            ##...#.###
            .#.#.#..##
            ..#....#..
            ###...#.#.
            ..###..###
            
            0011100111

            left: 0111110010
             */
            var actual = 0;
            foreach (var p in pictures)
            {
                actual += p.Value.GetHashNumber(1);
            }
            var expected = 273 + 15 * 2;
            Assert.Equal(expected, actual);

        }

        [Fact]
        private static void Test_TotalGetHashNumForInput()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Input.txt";
            var pictures = Day20.ReadInput(fileName);
            /*
            Tile 2311:
            0011010010
            ..##.#..#. 0001011001
            ##..#.....
            #...##..#.
            ####.#...#
            ##.##.###.
            ##...#.###
            .#.#.#..##
            ..#....#..
            ###...#.#.
            ..###..###
            
            0011100111

            left: 0111110010
             */
            var actual = 0;
            foreach (var p in pictures)
            {
                actual += p.Value.GetHashNumber(1);
            }
            var expected = 2506;
            Assert.Equal(expected, actual);

        }

        [Fact]
        private static void Test_SeaMonster_HashNum()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\SeaMonster.txt";
            var pictures = Day20.ReadInput(fileName);
            var seaMonster = pictures[1];
            /*
            Tile 1:
                              # 
            #    ##    ##    ###
             #  #  #  #  #  #   
             */

            var actual = seaMonster.GetHashNumber(0);
            var expected = 15;
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void Test_SeaMonsterWidthAndHeight()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\SeaMonster.txt";
            var pictures = Day20.ReadInput(fileName);
            var seaMonster = pictures[1];
            /*
            Tile 1:
                              # 
            #    ##    ##    ###
             #  #  #  #  #  #   
             */

            var actual = seaMonster.Width;
            var expected = 20;
            Assert.Equal(expected, actual);

            actual = seaMonster.Height;
            expected = 3;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void Test_BuildMosaicWithTest1()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Test1.txt";
            var picturesById = Day20.ReadInput(fileName);
            Dictionary<int, List<Picture>> picturesByBorders = Day20.GetPicturesByBorders(picturesById);

            var topLeftCornerId = 1951;

            var tlc = picturesById[topLeftCornerId];
            tlc.MirrorVertically();

            var mosaic = Day20.BuildMosaicImage(picturesById, picturesByBorders, topLeftCornerId);
            Assert.Equal(2311, tlc.Right.Id);
            Assert.Equal(3079, tlc.Right.Right.Id);
            Assert.Equal(2729, tlc.Bottom.Id);
            Assert.Equal(1427, tlc.Bottom.Right.Id);
            Assert.Equal(2473, tlc.Bottom.Right.Right.Id);
            Assert.Equal(2971, tlc.Bottom.Bottom.Id);
            Assert.Equal(1489, tlc.Bottom.Bottom.Right.Id);
            Assert.Equal(1171, tlc.Bottom.Bottom.Right.Right.Id);

            var actual = mosaic.GetHashNumber(0);
            var expected = 303;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void Test_BuildMosaic()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Input.txt";
            var picturesById = Day20.ReadInput(fileName);
            Dictionary<int, List<Picture>> picturesByBorders = Day20.GetPicturesByBorders(picturesById);

            var topLeftCornerId = Day20.CornersIds(picturesById, picturesByBorders).First();

            var mosaic = Day20.BuildMosaicImage(picturesById, picturesByBorders, topLeftCornerId);

            var actual = mosaic.GetHashNumber(0);
            var expected = 2506;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void Test_RotateNonSquareImage()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\SeaMonster.txt";
            var pictures = Day20.ReadInput(fileName);
            var seaMonster = pictures[1];
            /*
            Tile 1:
                              # 
            #    ##    ##    ###
             #  #  #  #  #  #   
             */
            seaMonster.RotateClockwise();

            var actual = seaMonster.Height;
            var expected = 20;
            Assert.Equal(expected, actual);

            actual = seaMonster.Width;
            expected = 3;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void Test_SearchForPictureAtPosition()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\SeaMonster.txt";
            var pictures = Day20.ReadInput(fileName);
            var seaMonster = pictures[1];
            var innerPic = new Picture("tile 2:", new List<string>() { "###" });
            var actual = seaMonster.SearchForPictureAtPosition(17, 1, innerPic);

            /*
            Tile 1:
                              # 
            #    ##    ##    ###
             #  #  #  #  #  #   
             */
            var expected = true;
            Assert.Equal(expected, actual);

        }

        [Fact]
        public static void Test_SearchForPictureAtPosition_Negative()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\SeaMonster.txt";
            var pictures = Day20.ReadInput(fileName);
            var seaMonster = pictures[1];
            var innerPic = new Picture("tile 2:", new List<string>() { "###" });
            var actual = seaMonster.SearchForPictureAtPosition(17, 0, innerPic);

            /*
            Tile 1:
                              # 
            #    ##    ##    ###
             #  #  #  #  #  #   
             */
            var expected = false;
            Assert.Equal(expected, actual);

        }

        [Fact]
        public static void Test_SearchForPictureAtPosition_OutofBounds()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\SeaMonster.txt";
            var pictures = Day20.ReadInput(fileName);
            /*
               Tile 1:
                                 # 
               #    ##    ##    ###
                #  #  #  #  #  #   
            */
            var seaMonster = pictures[1];

            var innerPic = new Picture("tile 2:", new List<string>() { "###" });
            var actual = seaMonster.SearchForPictureAtPosition(25, 13, innerPic);

            var expected = false;
            Assert.Equal(expected, actual);

        }

        [Fact]
        public static void Test_SearchForPictureAtPosition_Reversed()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\SeaMonster.txt";
            var pictures = Day20.ReadInput(fileName);
            /*
               Tile 1:
                                 # 
               #    ##    ##    ###
                #  #  #  #  #  #   
            */
            var seaMonster = pictures[1];

            var innerPic = new Picture("tile 2:", new List<string>() { "###" });
            var actual = innerPic.SearchForPictureAtPosition(25, 13, seaMonster);

            var expected = false;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void Test_SearchForPicture()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\SeaMonster.txt";
            var pictures = Day20.ReadInput(fileName);
            var seaMonster = pictures[1];
            var innerPic = new Picture("tile 2:", new List<string>() { "###" });
            var actual = seaMonster.SearchForPicture(innerPic);

            /*
            Tile 1:
                              # 
            #    ##    ##    ###
             #  #  #  #  #  #   
             */
            var expected = 1;
            Assert.Equal(expected, actual);

        }
        [Fact]
        public static void Test_SearchForPicture_Multimatch()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\SeaMonster.txt";
            var pictures = Day20.ReadInput(fileName);
            var seaMonster = pictures[1];
            var innerPic = new Picture("tile 2:", new List<string>() { "##" });
            var actual = seaMonster.SearchForPicture(innerPic);

            /*
            Tile 1:
                              # 
            #    ##    ##    ###
             #  #  #  #  #  #   
             */
            var expected = 4;
            Assert.Equal(expected, actual);

        }

        [Fact]
        public static void Test_SearchForPicture_2()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\SeaMonster.txt";
            var pictures = Day20.ReadInput(fileName);
            var seaMonster = pictures[1];
            var innerPic = new Picture("tile 2:", new List<string>() { " # ", "###" });
            var actual = seaMonster.SearchForPicture(innerPic);

            /*
            Tile 1:
                              # 
            #    ##    ##    ###
             #  #  #  #  #  #   
             */
            var expected = 1;
            Assert.Equal(expected, actual);

        }

        [Fact]
        public static void Test_SearchForPicture_2_Multimatch()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\SeaMonster.txt";
            var pictures = Day20.ReadInput(fileName);
            var seaMonster = pictures[1];
            var innerPic = new Picture("tile 2:", new List<string>() { " ## ","#  #" });
            var actual = seaMonster.SearchForPicture(innerPic);

            /*
            Tile 1:
                              # 
            #    ##    ##    ###
             #  #  #  #  #  #   
             */
            var expected = 2;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void Test_SearchForPictureWithAnyOrientation()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\SeaMonster.txt";
            var pictures = Day20.ReadInput(fileName);
            var seaMonster = pictures[1];
            var innerPic = new Picture("tile 2:", new List<string>() { "##" });
            var actual = seaMonster.SearchForPictureWithAnyOrientation(innerPic);

            /*
            Tile 1:
                              # 
            #    ##    ##    ###
             #  #  #  #  #  #   
             */
            var expected = 20;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void Test_SearchForSeaMonsterInMosaic_Test1()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Test1.txt";
            var picturesById = Day20.ReadInput(fileName);
            Dictionary<int, List<Picture>> picturesByBorders = Day20.GetPicturesByBorders(picturesById);
            var topLeftCornerId = 1951;

            var tlc = picturesById[topLeftCornerId];
            tlc.MirrorVertically();

            var mosaic = Day20.BuildMosaicImage(picturesById, picturesByBorders, topLeftCornerId);

            fileName = @".\..\..\..\..\AdventOfCode\Day20\SeaMonster.txt";
            var pictures = Day20.ReadInput(fileName);
            var seaMonster = pictures[1];
            var actual = mosaic.SearchForPictureWithAnyOrientation(seaMonster);

            /*
            Tile 1:
                              # 
            #    ##    ##    ###
             #  #  #  #  #  #   
             */
            var expected = 2;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void Test_SearchForSeaMonsterInMosaic()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day20\Input.txt";
            var picturesById = Day20.ReadInput(fileName);
            Dictionary<int, List<Picture>> picturesByBorders = Day20.GetPicturesByBorders(picturesById);

            var topLeftCornerId = Day20.CornersIds(picturesById, picturesByBorders).First();

            var mosaic = Day20.BuildMosaicImage(picturesById, picturesByBorders, topLeftCornerId);

            fileName = @".\..\..\..\..\AdventOfCode\Day20\SeaMonster.txt";
            var pictures = Day20.ReadInput(fileName);
            var seaMonster = pictures[1];
            var actual = mosaic.SearchForPictureWithAnyOrientation(seaMonster);

            /*
            Tile 1:
                              # 
            #    ##    ##    ###
             #  #  #  #  #  #   
             */
            var expected = 23;
            Assert.Equal(expected, actual);
        }
    }
}

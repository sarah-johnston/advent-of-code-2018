using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Days
{
    public class Day3
    {
        public static void Day3Solution()
        {
            var input = ParseFile();

            List<int[]> allCoordinates = new List<int[]>();

            //create the whole coordinate grid (with all values at 0)
            int gridSize = 2000;

            int[,] grid = new int[gridSize, gridSize];
            for (int i = 0; i < gridSize * gridSize; i++)
            {
                grid[i % gridSize, i / gridSize] = 0;
            }


            foreach (var coordinate in input)
            {
                List<int[]> coordinates = ParseCoordinates(coordinate);

                foreach (var coord in coordinates)
                {
                    // need to find if claimed and set to marker if double-claimed.
                    if (grid[coord[1], coord[2]] == 0)
                    {
                        grid[coord[1], coord[2]] = coord[0]; //ID here
                    }
                    else
                    {
                        grid[coord[1], coord[2]] = -1;
                    }

                }
            }

            foreach (var coordinate in input)
            {
                int ID = GetID(coordinate);
                List<int[]> coordinates = ParseCoordinates(coordinate);

                int matchingCoordinates = 0;

                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        int s = grid[i, j];
                        if (s.Equals(ID))
                        {
                            matchingCoordinates++;
                        }
                    }
                }

                if (matchingCoordinates == coordinates.Count)
                {
                    Console.WriteLine(ID);
                    Console.Read();
                }
            }


            //Console.WriteLine(duplicates);
            //Console.ReadLine();
        }

        public static int GetID(string input)
        {
            string[] pieces = input.Split('@', ':');
            return Int32.Parse(pieces[0].Trim('#', ' '));
        }

        public static List<int[]> ParseCoordinates(string input)
        {
            string[] pieces = input.Split('@', ':');

            int id = Int32.Parse(pieces[0].Trim('#', ' '));

            string[] coordinates = pieces[1].Split(',');
            string[] lengths = pieces[2].Split('x');

            int startingX = Int32.Parse(coordinates[0]) + 1;
            int startingY = Int32.Parse(coordinates[1]) + 1;
            int maxX = startingX + Int32.Parse(lengths[0]) - 1;
            int maxY = startingY + Int32.Parse(lengths[1]) - 1;

            List<int[]> coordinatesList = new List<int[]>();

            // add all co-ords to list
            for (int x = startingX; x <= maxX; x++)
            {
                for (int y = startingY; y <= maxY; y++)
                {
                    coordinatesList.Add(new[] {id, x, y});
                }
            }
            return coordinatesList;
        }

        static List<string> ParseFile()
        {
            List<string> output = new List<string>();
            string line;
            StreamReader file = new StreamReader("Input3.txt");

            while ((line = file.ReadLine()) != null)
            {
               output.Add(line);
            }
            file.Close();
            return output;
        }

    }
}

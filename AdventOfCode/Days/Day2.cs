using System;
using System.Collections.Generic;
namespace AdventOfCode.Days
{
    public class Day2
    {
        public static void Day2Solution()
        {
            var input = ParseFile();

            int numberOfTwos = 0;
            int numberOfThrees = 0;

            foreach (var line in input)
            {
                if (matchXTimes(line, 2))
                    numberOfTwos++;
                if (matchXTimes(line, 3))
                    numberOfThrees++;
            }

            var result = numberOfTwos * numberOfThrees;
            Console.WriteLine(result);
            Console.Read();

            findBoxes(input);
        }



        static void findBoxes(List<string>inputs)
        {
            foreach (var input in inputs)
            {
                int matchingChars;
                Dictionary<string, int> entries = new Dictionary<string, int>();
                //need to find a nearly-matching string
                foreach (var entry in inputs)
                {
                    matchingChars = 0;
                    if (entry != input)
                    {
                        for (int i = 0; i < entry.Length; i++)
                        {
                            if (entry[i] == input[i])
                                matchingChars++;
                        }
                        entries.Add(entry, matchingChars);
                    }
                }

                foreach (string key in entries.Keys)
                {
                    if (entries[key].Equals(input.Length - 1))
                    {
                        Console.WriteLine("Matching boxes are " + input + ", " + key);
                        Console.ReadLine();
                    }
                }
            }
        }


        static bool matchXTimes(string input, int numberOfTimes)
        {
            // go through each letter in the input
            foreach (char x in input)
            {
                // and see how many other letters it matches
                int count = 0;
                foreach (char y in input)
                {
                    if (x.Equals(y))
                        count++;
                }

                if (count == numberOfTimes)
                    return true;
            }
            return false;
        }
        
        static List<string> ParseFile()
        {
            List<string> output = new List<string>();
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("Input2.txt");

            while ((line = file.ReadLine()) != null)
            {
               output.Add(line);
            }
            file.Close();
            return output;
        }

    }
}

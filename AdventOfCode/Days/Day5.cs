using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day5
    {
        public static void Day5Solution()
        {
            var input = File.ReadAllText("Inputs/Input5.txt");
            List<char> inputArray = new List<char>();
            inputArray.AddRange(input);

            // Part 1
            List<char> originalInput1 = new List<char>(inputArray);
            List<char> result1 = FullyReact(originalInput1);

            // Part 2
            char[] alphabet = new[]
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
                'v', 'w', 'x', 'y', 'z'
            };
            List<char> originalInput2 = new List<char>(inputArray);

            Dictionary<char, int> scores = new Dictionary<char, int>();

            foreach (char letter in alphabet)
            {
                List<char> alteredInput = new List<char>(originalInput2);
                alteredInput = RemoveLetters(alteredInput, letter);
                List<char> result = FullyReact(alteredInput);
                scores.Add(letter, result.Count);
            }

            var winner = scores.OrderBy(x => x.Value).First().Value;

            Console.WriteLine(result1.Count);
            Console.WriteLine(winner);
            Console.Read();

        }

        static List<char> FullyReact(List<char> input)
        {
            // Part 1
            var alteredInput = new List<char>(input);

            bool keepProcessing = true;
            while (keepProcessing)
            {
                var originalInput = new List<char>(alteredInput);
                alteredInput = ProcessReactions(alteredInput);
                if (originalInput.SequenceEqual(alteredInput))
                    keepProcessing = false;
            }

            return alteredInput;
        }


        static List<char> ProcessReactions(List<char> input)
        {
            for(int i = 0; i < input.Count - 1; i++)
            {
                char value1 = input[i];
                char value2 = input[i + 1];
                if (value1 != value2 && Char.ToUpper(value1) == Char.ToUpper(value2))
                {
                    //then a reaction occurs
                    input.RemoveAt(i);
                    input.RemoveAt(i);
                }
            }
            return input;
        }

        static List<char> RemoveLetters(List<char> input, char letter)
        {
            return input.Where(t => !Char.ToLower(t).Equals(Char.ToLower(letter))).ToList();
        }

    }


}

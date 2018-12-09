using System;
using System.Collections.Generic;
namespace ConsoleApp1
{
    public class Day1
    {
        public static void Day1Solution()
        {
            int frequency = 0;
            List<int> frequencies = new List<int>();
            string line;
            while (true)
            {
                System.IO.StreamReader file = new System.IO.StreamReader("Input1.txt");

                while ((line = file.ReadLine()) != null)
                {
                    int number = Int32.Parse(line);
                    frequency = frequency + number;
                    foreach (var freq in frequencies)
                    {
                        if (frequency.Equals(freq))
                            System.Console.WriteLine("Frequency reached twice: " + freq);
                    }

                    frequencies.Add(frequency);
                }

                file.Close();
                Console.WriteLine("Final frequency: " + frequency);
            }
        }

    }
}

using System;
using System.Collections.Generic;
namespace ConsoleApp1
{
    class Day3
    {
        static void Main(string[] args)
        {
            var input = ParseFile();


        
        }


        static List<string> ParseFile()
        {
            List<string> output = new List<string>();
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("Input3.txt");

            while ((line = file.ReadLine()) != null)
            {
               output.Add(line);
            }
            file.Close();
            return output;
        }

    }
}

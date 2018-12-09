using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public static class Common
    {
        public static List<string> ParseFile(string filename)
        {
            List<string> output = new List<string>();
            string line;
            StreamReader file = new StreamReader("Inputs/" + filename);

            while ((line = file.ReadLine()) != null)
            {
                output.Add(line);
            }
            file.Close();
            return output;
        }
    }
}

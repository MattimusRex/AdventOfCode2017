using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int numValid = 0;
            var lines = File.ReadAllLines(@"C:\Users\matthew.lay\Documents\Visual Studio 2015\Projects\AdventOfCodeSoln\Day4-1\input.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                numValid++;
                string[] splitLine = lines[i].Split(' ');
                Dictionary<string, int> words = new Dictionary<string, int>();
                for (int j = 0; j < splitLine.Length; j++)
                {
                    splitLine[j] = sortString(splitLine[j]);
                    if (words.ContainsKey(splitLine[j]))
                    {
                        numValid--;
                        break;
                    }
                    else
                    {
                        words.Add(splitLine[j], 1);
                    }
                }
            }
            Console.WriteLine(numValid);
        }

        static private string sortString(string str)
        {
            char[] chars = str.ToArray();
            Array.Sort(chars);
            return new string(chars);
        }
    }
}

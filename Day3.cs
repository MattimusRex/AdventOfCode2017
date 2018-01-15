using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem3
{
    class Program
    {
        static void Main(string[] args)
        {
            int checksum = 0;
            var lines = File.ReadAllLines(@"C:\Users\Matt\Dropbox\quarter 3\Analysis of Algorithms CS325\week 9\AdventOfCodeSoln\Problem3\input1.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] splitLine = lines[i].Split('\t');
                int min = 10000;
                int max = -1;
                for (int j = 0; j < splitLine.Length; j++)
                {
                    int num = Int32.Parse(splitLine[j]);
                    if (num < min)
                    {
                        min = num;
                    }
                    if (num > max)
                    {
                        max = num;
                    }
                }
                checksum += (max - min);
            }
            Console.WriteLine(checksum);
        }
    }
}

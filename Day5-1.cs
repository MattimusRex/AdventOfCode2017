using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(@"C:\Users\Matt\Dropbox\quarter 3\Analysis of Algorithms CS325\week 9\AdventOfCodeSoln\Day5-1\input.txt");
            int[] jumps = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                jumps[i] = Int32.Parse(lines[i]);
            }

            int counter = 0;
            int index = 0;
            while (index > -1 && index < jumps.Length)
            {
                index += jumps[index]++;
                counter++;
            }
            Console.WriteLine(counter);
        }
    }
}

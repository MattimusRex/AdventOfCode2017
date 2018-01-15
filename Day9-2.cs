using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<char> chars = new Stack<char>();
            string input = File.ReadAllText(@"C:\Users\Matt\Dropbox\quarter 3\Analysis of Algorithms CS325\week 9\AdventOfCodeSoln\Day9-1\input.txt");
            bool garbage = false;
            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                //if garbage, only check for skip or end garbage
                if (garbage)
                {
                    if (input[i] == '!')
                    {
                        i++;
                    }
                    else if (input[i] == '>')
                    {
                        garbage = false;
                    }
                    else
                    {
                        count++;
                    }
                }
                else if (input[i] == '<')
                {
                    garbage = true;
                }
            }
            Console.WriteLine(count);
        }
    }
}

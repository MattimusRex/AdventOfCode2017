using System;
using System.IO;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Day11_1
{
    class Program
    {
        private static bool madeChange = false;

        static void Main(string[] args)
        {
            string input = File.ReadAllText(@"C:\Users\matthew.lay\Documents\Visual Studio 2015\Projects\AdventOfCodeSoln\Day11-1\input.txt");
            string[] splitInput = input.Split(',');
            List<string> directions = new List<string>(splitInput);
            do
            {
                directions = Condense(directions);
            } while (madeChange);

            Console.WriteLine(directions.Count());
        }

        static private List<string> Condense(List<string> input)
        {
            madeChange = false;
            List<string> output = new List<string>(input.Count());
            //0 n, 1 ne, 2 se, 3 s, 4 sw, 5 nw
            int[] dirCounts = new int[6];

            //get counts
            for (int i = 0; i < input.Count(); i++)
            {
                dirCounts[GetIndex(input[i])]++;
            }

            //condense counts
            int[] newDirCounts = new int[6];
            while (dirCounts[GetIndex("n")] > 0 && dirCounts[GetIndex("s")] > 0)
            {
                madeChange = true;
                dirCounts[GetIndex("n")]--;
                dirCounts[GetIndex("s")]--;
            }
            while (dirCounts[GetIndex("nw")] > 0 && dirCounts[GetIndex("se")] > 0)
            {
                madeChange = true;
                dirCounts[GetIndex("nw")]--;
                dirCounts[GetIndex("se")]--;
            }
            while (dirCounts[GetIndex("ne")] > 0 && dirCounts[GetIndex("sw")] > 0)
            {
                madeChange = true;
                dirCounts[GetIndex("ne")]--;
                dirCounts[GetIndex("sw")]--;
            }
            while (dirCounts[GetIndex("n")] > 0 && dirCounts[GetIndex("se")] > 0)
            {
                madeChange = true;
                dirCounts[GetIndex("n")]--;
                dirCounts[GetIndex("se")]--;
                newDirCounts[GetIndex("ne")]++;
            }
            while (dirCounts[GetIndex("nw")] > 0 && dirCounts[GetIndex("ne")] > 0)
            {
                madeChange = true;
                dirCounts[GetIndex("nw")]--;
                dirCounts[GetIndex("ne")]--;
                newDirCounts[GetIndex("n")]++;
            }
            while (dirCounts[GetIndex("sw")] > 0 && dirCounts[GetIndex("se")] > 0)
            {
                madeChange = true;
                dirCounts[GetIndex("sw")]--;
                dirCounts[GetIndex("se")]--;
                newDirCounts[GetIndex("s")]++;
            }
            while (dirCounts[GetIndex("ne")] > 0 && dirCounts[GetIndex("s")] > 0)
            {
                madeChange = true;
                dirCounts[GetIndex("ne")]--;
                dirCounts[GetIndex("s")]--;
                newDirCounts[GetIndex("ne")]++;
            }
            while (dirCounts[GetIndex("n")] > 0 && dirCounts[GetIndex("sw")] > 0)
            {
                madeChange = true;
                dirCounts[GetIndex("n")]--;
                dirCounts[GetIndex("sw")]--;
                newDirCounts[GetIndex("nw")]++;
            }
            while (dirCounts[GetIndex("nw")] > 0 && dirCounts[GetIndex("s")] > 0)
            {
                madeChange = true;
                dirCounts[GetIndex("nw")]--;
                dirCounts[GetIndex("s")]--;
                newDirCounts[GetIndex("sw")]++;
            }
            for (int i = 0; i < dirCounts.Length; i++)
            {
                newDirCounts[i] += dirCounts[i];
            }
            while (newDirCounts[0]-- > 0)
            {
                output.Add("n");
            }
            while (newDirCounts[1]-- > 0)
            {
                output.Add("nw");
            }
            while (newDirCounts[2]-- > 0)
            {
                output.Add("sw");
            }
            while (newDirCounts[3]-- > 0)
            {
                output.Add("s");
            }
            while (newDirCounts[4]-- > 0)
            {
                output.Add("se");
            }
            while (newDirCounts[5]-- > 0)
            {
                output.Add("ne");
            }
            return output;
        }

        static private int GetIndex(string dir)
        {
            if (dir == "n")
                return 0;
            if (dir == "ne")
                return 1;
            if (dir == "se")
                return 2;
            if (dir == "s")
                return 3;
            if (dir == "sw")
                return 4;
            if (dir == "nw")
                return 5;
            else
            {
                return -1;
            }
        }
    }
}

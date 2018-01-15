using System;
using System.CodeDom;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string input =
                File.ReadAllText(
                    @"C:\Users\matthew.lay\Documents\Visual Studio 2015\Projects\AdventOfCodeSoln\Day11-1\input.txt");
            string[] splitInput = input.Split(',');
            int[] dirCount = new int[6];
            //0 n, 1 ne, 2 se, 3 s, 4 sw, 5 nw
            int maxDist = 0;
            int curDist = 0;
            for (int i = 0; i < splitInput.Length; i++)
            {
                //direction of movement
                if (splitInput[i] == "n")
                {
                    //opposite direction
                    if (dirCount[GetIndex("s")] > 0)
                    {
                        dirCount[GetIndex("s")]--;
                        curDist--;
                    }
                    //side direction
                    else if (dirCount[GetIndex("se")] > 0)
                    {
                        dirCount[GetIndex("se")]--;
                        dirCount[GetIndex("ne")]++;
                    }
                    //side direction
                    else if (dirCount[GetIndex("sw")] > 0)
                    {
                        dirCount[GetIndex("sw")]--;
                        dirCount[GetIndex("nw")]++;
                    }
                    else
                    {
                        dirCount[GetIndex("n")]++;
                        curDist++;
                        if (curDist > maxDist)
                        {
                            maxDist = curDist;
                        }
                    }
                }
                else if (splitInput[i] == "ne")
                {
                    //opposite direction
                    if (dirCount[GetIndex("sw")] > 0)
                    {
                        dirCount[GetIndex("sw")]--;
                        curDist--;
                    }
                    //side direction
                    else if (dirCount[GetIndex("nw")] > 0)
                    {
                        dirCount[GetIndex("nw")]--;
                        dirCount[GetIndex("n")]++;
                    }
                    //side direction
                    else if (dirCount[GetIndex("s")] > 0)
                    {
                        dirCount[GetIndex("s")]--;
                        dirCount[GetIndex("se")]++;
                    }
                    else
                    {
                        dirCount[GetIndex("ne")]++;
                        curDist++;
                        if (curDist > maxDist)
                        {
                            maxDist = curDist;
                        }
                    }
                }
                else if (splitInput[i] == "se")
                {
                    //opposite direction
                    if (dirCount[GetIndex("nw")] > 0)
                    {
                        dirCount[GetIndex("nw")]--;
                        curDist--;
                    }
                    //side direction
                    else if (dirCount[GetIndex("sw")] > 0)
                    {
                        dirCount[GetIndex("sw")]--;
                        dirCount[GetIndex("s")]++;
                    }
                    //side direction
                    else if (dirCount[GetIndex("n")] > 0)
                    {
                        dirCount[GetIndex("n")]--;
                        dirCount[GetIndex("ne")]++;
                    }
                    else
                    {
                        dirCount[GetIndex("se")]++;
                        curDist++;
                        if (curDist > maxDist)
                        {
                            maxDist = curDist;
                        }
                    }
                }
                else if (splitInput[i] == "s")
                {
                    //opposite direction
                    if (dirCount[GetIndex("n")] > 0)
                    {
                        dirCount[GetIndex("n")]--;
                        curDist--;
                    }
                    //side direction
                    else if (dirCount[GetIndex("ne")] > 0)
                    {
                        dirCount[GetIndex("ne")]--;
                        dirCount[GetIndex("se")]++;
                    }
                    //side direction
                    else if (dirCount[GetIndex("nw")] > 0)
                    {
                        dirCount[GetIndex("nw")]--;
                        dirCount[GetIndex("sw")]++;
                    }
                    else
                    {
                        dirCount[GetIndex("s")]++;
                        curDist++;
                        if (curDist > maxDist)
                        {
                            maxDist = curDist;
                        }
                    }
                }
                else if (splitInput[i] == "sw")
                {
                    //opposite direction
                    if (dirCount[GetIndex("ne")] > 0)
                    {
                        dirCount[GetIndex("ne")]--;
                        curDist--;
                    }
                    //side direction
                    else if (dirCount[GetIndex("se")] > 0)
                    {
                        dirCount[GetIndex("se")]--;
                        dirCount[GetIndex("s")]++;
                    }
                    //side direction
                    else if (dirCount[GetIndex("n")] > 0)
                    {
                        dirCount[GetIndex("n")]--;
                        dirCount[GetIndex("nw")]++;
                    }
                    else
                    {
                        dirCount[GetIndex("sw")]++;
                        curDist++;
                        if (curDist > maxDist)
                        {
                            maxDist = curDist;
                        }
                    }
                }
                else if (splitInput[i] == "nw")
                {
                    //opposite direction
                    if (dirCount[GetIndex("se")] > 0)
                    {
                        dirCount[GetIndex("se")]--;
                        curDist--;
                    }
                    //side direction
                    else if (dirCount[GetIndex("ne")] > 0)
                    {
                        dirCount[GetIndex("ne")]--;
                        dirCount[GetIndex("n")]++;
                    }
                    //side direction
                    else if (dirCount[GetIndex("s")] > 0)
                    {
                        dirCount[GetIndex("s")]--;
                        dirCount[GetIndex("sw")]++;
                    }
                    else
                    {
                        dirCount[GetIndex("nw")]++;
                        curDist++;
                        if (curDist > maxDist)
                        {
                            maxDist = curDist;
                        }
                    }
                }
            }
            Console.WriteLine(maxDist);
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

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(@"C:\Users\matthew.lay\Documents\Visual Studio 2015\Projects\AdventOfCodeSoln\Day13-1\input.txt");
            int totalDepth = Int32.Parse(lines[lines.Length - 1].Split(':')[0]) + 1;
            int[] ranges = new int[totalDepth];
            bool[] depths = new bool[totalDepth];
            bool[] back = new bool[totalDepth];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(':', ' ');
                //0 is depth, 2 is range
                depths[Int32.Parse(parts[0])] = true;
                ranges[Int32.Parse(parts[0])] = Int32.Parse(parts[2]);
            }

            int[] scannerPosition = new int[totalDepth];
            IncrementScanners(scannerPosition, ranges, depths, back);
            int packetIndex = 0;
            int totalSeverity = 0;
            for (int i = 1; i < totalDepth; i++)
            {
                //packet moves
                packetIndex++;
                if (depths[packetIndex] && scannerPosition[packetIndex] == 0)
                {
                    totalSeverity += i * ranges[i];
                }

                //scanners move
                IncrementScanners(scannerPosition, ranges, depths, back);
            }

            Console.WriteLine(totalSeverity);


        }

        static private void IncrementScanners(int[] pos, int[] ranges, bool[] depths, bool[] back)
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (depths[i])
                {
                    if (!back[i])
                    {
                        pos[i] = (pos[i] + 1) % ranges[i];
                        if (pos[i] == 0)
                        {
                            pos[i] = ranges[i] - 2;
                            back[i] = true;
                        }
                    }
                    else
                    {
                        pos[i] = pos[i] - 1;
                        if (pos[i] == -1)
                        {
                            pos[i] = 1;
                            back[i] = false;
                        }
                    }
                }
            }
        }
    }
}

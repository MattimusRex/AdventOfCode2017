using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(@"C:\Users\matthew.lay\Documents\Visual Studio 2015\Projects\AdventOfCodeSoln\Day13-1\input.txt");
            int totalDepth = Int32.Parse(lines[lines.Length - 1].Split(':')[0]) + 1;
            int[] ranges = new int[totalDepth];
            bool[] back = new bool[totalDepth];
            bool[] backCopy = new bool[totalDepth];
            bool[] depths = new bool[totalDepth];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(':', ' ');
                //0 is depth, 2 is range
                depths[Int32.Parse(parts[0])] = true;
                ranges[Int32.Parse(parts[0])] = Int32.Parse(parts[2]);
            }
            bool collision = true;
            int delayCount = -1;
            int[] scannerPosition = new int[totalDepth];
            int[] scannerCopy;
            while (collision)
            {
                int packetIndex = -1;
                delayCount++;
                collision = false;
                scannerCopy = (int[]) scannerPosition.Clone();
                backCopy = (bool[]) back.Clone();
                for (int i = 0; i < totalDepth; i++)
                {
                    //packet moves
                    packetIndex++;
                    if (depths[packetIndex] && scannerPosition[packetIndex] == 0)
                    {
                        collision = true;
                        scannerPosition = (int[])scannerCopy.Clone();
                        back = (bool[]) backCopy.Clone();
                        IncrementScanners(scannerPosition, ranges, depths, back);
                        break;
                    }

                    //scanners move
                    IncrementScanners(scannerPosition, ranges, depths, back);
                }
            }
            Console.WriteLine(delayCount);
            
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

        static private void printArray(int[] array, string arrName)
        {
            Console.Write(arrName + ": ");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.Write('\n');
        }
    }
}

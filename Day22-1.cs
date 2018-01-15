using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day22_1
{
    class Program
    {

        public class OP
        {
            public int x;
            public int y;

            public OP (int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        private class OPEqualityComparer : IEqualityComparer<OP>
        {
            public bool Equals(OP a, OP b)
            {
                if (a.x == b.x && a.y == b.y)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(OP pair)
            {
                string combined = "Ordered Pair" + pair.x.ToString() + pair.y.ToString();
                return (combined.GetHashCode());
            }
        }

        static void Main(string[] args)
        {
            HashSet<OP> infected = new HashSet<OP>(new OPEqualityComparer());
            var lines = File.ReadAllLines(@"C:\Users\Matt\Dropbox\quarter 3\Analysis of Algorithms CS325\week 9\AdventOfCodeSoln\Day22-1\input.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '#')
                    {
                        infected.Add(new OP(i, j));
                    }
                }
            }
            string direction = "up"; 
            int intCausedInfection = 0;
            OP current = new OP(lines.Length / 2, lines[0].Length / 2);

            for (int i = 0; i < 10000; i++)
            {
                PerformBurst(ref direction, infected, ref intCausedInfection, ref current);
            }

            Console.WriteLine(intCausedInfection);
        }

        static private void PerformBurst(ref string direction, HashSet<OP> infectedList, ref int intCausedInfection, ref OP current)
        {
            //turn
            bool infected = infectedList.Contains(current);
            if (infected)
            {
                direction = TurnRight(direction);
            }
            else
            {
                direction = TurnLeft(direction);
            }

            //swap infection
            if (infected)
            {
                infectedList.Remove(current);
            }
            else
            {
                intCausedInfection++;
                infectedList.Add(current);
            }

            //move
            current = MoveForward(direction, current);
        }

        static private string TurnRight(string curDir)
        {
            string newDir;
            if (curDir == "up")
                newDir = "right";
            else if (curDir == "right")
                newDir = "down";
            else if (curDir == "down")
                newDir = "left";
            else
                newDir = "up";
            return newDir;
        }

        static private string TurnLeft(string curDir)
        {
            string newDir;
            if (curDir == "up")
                newDir = "left";
            else if (curDir == "right")
                newDir = "up";
            else if (curDir == "down")
                newDir = "right";
            else
                newDir = "down";
            return newDir;
        }

        static private OP MoveForward(string curDir, OP current)
        {
            if (curDir == "up")
                current = new OP(current.x - 1, current.y);
            else if (curDir == "right")
                current = new OP(current.x, current.y + 1);
            else if (curDir == "down")
                current = new OP(current.x + 1, current.y);
            else
                current = new OP(current.x, current.y - 1);
            return current;
        }
    }
}

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

            public OP(int x, int y)
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
            Dictionary<OP, char> status = new Dictionary<OP, char>(new OPEqualityComparer());
            var lines = File.ReadAllLines(@"C:\Users\Matt\Dropbox\quarter 3\Analysis of Algorithms CS325\week 9\AdventOfCodeSoln\Day22-1\input.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '#')
                    {
                        status.Add(new OP(i, j), 'i');
                    }
                    else
                    {
                        status.Add(new OP(i, j), 'c');
                    }
                }
            }
            OP current = new OP(lines.Length / 2, lines[0].Length / 2);
            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 3; j++)
            //    {
            //        status.Add(new OP(i, j), 'c');
            //    }
            //}
            //status[new OP(0, 1)] = 'i';
            //status[new OP(2, 0)] = 'i';
            //OP current = new OP(1, 1);

            string direction = "up";
            int intCausedInfection = 0;
            

            for (int i = 0; i < 10000000; i++)
            {
                PerformBurst(ref direction, status, ref intCausedInfection, ref current);
            }

            Console.WriteLine(intCausedInfection);
        }

        static private void PerformBurst(ref string direction, Dictionary<OP, char> statusList, ref int intCausedInfection, ref OP current)
        {
            //turn and affect
            char status = statusList[current];
            if (status == 'i')
            {
                direction = TurnRight(direction);
                statusList[current] = 'f';
            }
            else if (status == 'c')
            {
                direction = TurnLeft(direction);
                statusList[current] = 'w';
            }
            else if (status == 'f')
            {
                direction = Reverse(direction);
                statusList[current] = 'c';
            }
            //weakened
            else
            {
                statusList[current] = 'i';
                intCausedInfection++;
            }

            //move
            current = MoveForward(direction, current, statusList);
        }

        static private string Reverse(string curDir)
        {
            string newDir;
            if (curDir == "up")
                newDir = "down";
            else if (curDir == "right")
                newDir = "left";
            else if (curDir == "down")
                newDir = "up";
            else
                newDir = "right";
            return newDir;
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

        static private OP MoveForward(string curDir, OP current, Dictionary<OP, char> statusList)
        {
            if (curDir == "up")
                current = new OP(current.x - 1, current.y);
            else if (curDir == "right")
                current = new OP(current.x, current.y + 1);
            else if (curDir == "down")
                current = new OP(current.x + 1, current.y);
            else
                current = new OP(current.x, current.y - 1);

            if (!statusList.ContainsKey(current))
            {
                statusList.Add(current, 'c');
            }
            return current;
        }
    }
}

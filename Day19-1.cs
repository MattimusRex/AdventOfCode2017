using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Day19_1
{
    class Program
    {

        private class OP
        {
            public OP(int i, int j)
            {
                this.i = i;
                this.j = j;
            }
            public int i;
            public int j;
        }

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(@"C:\Users\matthew.lay\Documents\Visual Studio 2015\Projects\AdventOfCodeSoln\Day19-1\input.txt");
            char[][] maze = new char[lines[0].Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                maze[i] = lines[i].ToCharArray();
            }

            OP start = FindStart(maze[0]);
            OP current = start;
            int count = 0;
            string curDir = "down";
            do
            {
                count++;
                current = GetNext(maze, ref current, ref curDir);

            } while (maze[current.i][current.j] != ' ');

            Console.WriteLine(count);


        }

        static OP GetNext(char[][] maze, ref OP current, ref string curDir)
        {
            OP newOP;
            char curChar = maze[current.i][current.j];
            if (curChar == '|' || curChar == '-')
            {
                newOP = MoveCurDir(maze, ref current, ref curDir);
            }
            else if (curChar == '+')
            {
                curDir = FindNewDir(maze, ref current, ref curDir);
                newOP = MoveCurDir(maze, ref current, ref curDir);
            }
            else
            {
                Console.Write(maze[current.i][current.j]);
                newOP = MoveCurDir(maze, ref current, ref curDir);
            }
            return newOP;

        }

        static string FindNewDir(char[][] maze, ref OP current, ref string curDir)
        {
            string newDir;
            if (maze[current.i + 1][current.j] != ' ' && curDir != "up")
            {
                newDir = "down";
            }
            else if (maze[current.i - 1][current.j] != ' ' && curDir != "down")
            {
                newDir = "up";
            }
            else if (maze[current.i][current.j + 1] != ' ' && curDir != "left")
            {
                newDir = "right";
            }
            else
            {
                newDir = "left";
            }
            return newDir;
        }

        static OP MoveCurDir(char[][] maze, ref OP current, ref string curDir)
        {
            OP newOP;
            if (curDir == "down")
            {
                newOP = new OP(current.i + 1, current.j);
            }
            else if (curDir == "up")
            {
                newOP = new OP(current.i - 1, current.j);
            }
            else if (curDir == "left")
            {
                newOP = new OP(current.i, current.j - 1);
            }
            else
            {
                newOP = new OP(current.i, current.j + 1);
            }
            return newOP;
        }

        static OP FindStart(char[] firstRow)
        {
            int startJ = -1;
            for (int i = 0; i < firstRow.Length; i++)
            {
                if (firstRow[i] == '|')
                {
                    startJ = i;
                }
            }
            OP start = new OP(0, startJ);
            return start;
        }
    }
}

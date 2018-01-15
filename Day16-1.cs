using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16_1
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] dancers = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p' };
            var inputText = File.ReadAllText(@"C:\Users\Matt\Dropbox\quarter 3\Analysis of Algorithms CS325\week 9\AdventOfCodeSoln\Day16-1\input.txt");
            string[] moves = inputText.Split(',');
            for (int i = 0; i < moves.Length; i++)
            {
                PerformMove(moves[i], dancers);
            }

            foreach(char c in dancers)
            {
                Console.Write(c);
            }
        }

        static private void PerformMove(string move, char[] dancers)
        {
            if (move[0] == 's')
            {
                Spin(move, dancers);
            }
            else if (move[0] == 'x')
            {
                Exchange(move, dancers);
            }
            else if (move[0] == 'p')
            {
                Partner(move, dancers);
            }
        }

        static private void Spin(string move, char[] dancers)
        {
            int numOfSpins = Int32.Parse(move.Substring(1));
            for (int j = 0; j < numOfSpins; j++)
            {
                char last = dancers[dancers.Length - 1];
                for (int i = dancers.Length - 1; i > 0; i--)
                {
                    dancers[i] = dancers[i - 1];
                }
                dancers[0] = last;
            }
        }

        static private void Exchange(string move, char[] dancers)
        {
            string[] parts = move.Substring(1).Split('/');
            Swap(dancers, Int32.Parse(parts[0]), Int32.Parse(parts[1]));
        }

        static private void Partner(string move, char[] dancers)
        {
            int indexA = Array.IndexOf(dancers, move[1]);
            int indexB = Array.IndexOf(dancers, move[3]);
            Swap(dancers, indexA, indexB);
        }

        static private void Swap(char[] dancers, int indexA, int indexB)
        {
            char temp = dancers[indexA];
            dancers[indexA] = dancers[indexB];
            dancers[indexB] = temp;
        }

        static private void PrintArray(char[] dancers)
        {
            foreach (char c in dancers)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine();
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16_2
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] dancers = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p' };
            string dancerString = new String(dancers);
            var inputText = File.ReadAllText(@"C:\Users\Matt\Dropbox\quarter 3\Analysis of Algorithms CS325\week 9\AdventOfCodeSoln\Day16-1\input.txt");
            string[] moves = inputText.Split(',');
            for (int i = 0; i < 40; i++)
            {

                Dance(moves, ref dancers);
            }

            PrintArray(dancers);
        }

        static private void Dance(string[] moves, ref char[] dancers) 
        {
            for (int i = 0; i < moves.Length; i++)
            {
                PerformMove(moves[i], ref dancers);
            }
        }

        static private void PerformMove(string move, ref char[] dancers)
        {
            if (move[0] == 's')
            {
                Spin(move, ref dancers);
            }
            else if (move[0] == 'x')
            {
                Exchange(move, ref dancers);
            }
            else if (move[0] == 'p')
            {
                Partner(move, ref dancers);
            }
        }

        static private void Spin(string move, ref char[] dancers)
        {
            int numOfSpins = Int32.Parse(move.Substring(1));
            char[] newDancers = new char[dancers.Length];
            for (int i = 0; i < dancers.Length; i++)
            {
                newDancers[(i + numOfSpins) % dancers.Length] = dancers[i];
            }
            dancers = newDancers;
        }

        static private void Exchange(string move, ref char[] dancers)
        {
            string[] parts = move.Substring(1).Split('/');
            Swap(ref dancers, Int32.Parse(parts[0]), Int32.Parse(parts[1]));
        }

        static private void Partner(string move, ref char[] dancers)
        {
            int indexA = Array.IndexOf(dancers, move[1]);
            int indexB = Array.IndexOf(dancers, move[3]);
            Swap(ref dancers, indexA, indexB);
        }

        static private void Swap(ref char[] dancers, int indexA, int indexB)
        {
            char temp = dancers[indexA];
            dancers[indexA] = dancers[indexB];
            dancers[indexB] = temp;
        }

        static private void PrintArray(char[] dancers)
        {
            foreach (char c in dancers)
            {
                Console.Write(c);
            }
            Console.WriteLine();
        }
    }
}

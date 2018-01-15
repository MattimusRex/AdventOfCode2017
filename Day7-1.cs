using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(@"C:\Users\matthew.lay\Documents\Visual Studio 2015\Projects\AdventOfCodeSoln\Day7-1\input.txt");
            string[] names = new string[lines.Length];
            int[] weights = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                //split line
                string[] parts = lines[i].Split(' ');
                //name of tower for this line
                names[i] = parts[0];
                //weight of tower
                weights[i] = Int32.Parse(parts[1].Substring(1, parts[1].Length - 2));
                //if more than 2 parts, tower has towers on top
                if (parts.Length > 2)
                {
                    List<string> towersOnTop = new List<string>();
                    for (int j = 3; j < parts.Length; j++)
                    {
                        towersOnTop.Add(parts[j].Replace(",", ""));
                    }
                }
            }
        }
    }
}

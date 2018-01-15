/**
 * reads in data, splitting it into tower names, weights, and the towers that are balanced on top of the tower
 * creates a dict of towers and weights and a dict of towers and the towers immediately on top of it
 * recursively totals the weights of the towers to find the tower with the highest weight. 
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //process input
            var lines = File.ReadAllLines(@"C:\Users\Matt\Dropbox\quarter 3\Analysis of Algorithms CS325\week 9\AdventOfCodeSoln\Day7-1\input.txt");
            string[] names = new string[lines.Length];
            int[] weights = new int[lines.Length];
            Dictionary<string, int> towerWeights = new Dictionary<string, int>();
            Dictionary<string, List<string>> towerTowers = new Dictionary<string, List<string>>();
            for (int i = 0; i < lines.Length; i++)
            {
                //split line
                string[] parts = lines[i].Split(' ');
                //name of tower for this line
                names[i] = parts[0];
                //weight of tower
                towerWeights.Add(parts[0], Int32.Parse(parts[1].Substring(1, parts[1].Length - 2)));
                //if more than 2 parts, tower has towers on top
                List<string> towersOnTop = new List<string>();
                if (parts.Length > 2)
                {
                    //add towers to list
                    for (int j = 3; j < parts.Length; j++)
                    {
                        towersOnTop.Add(parts[j].Replace(",", ""));
                    }
                    //put info in dicts
                    towerTowers.Add(parts[0], towersOnTop);
                }
            }

            for (int i = 0; i < names.Length; i++)
            {
                List<string> towers;
                if (towerTowers.TryGetValue(names[i], out towers))
                {
                    int expWeight = getWeight(towers[0], towerWeights, towerTowers);
                    for (int j = 1; j < towers.Count(); j++)
                    {
                        int actualWeight = getWeight(towers[j], towerWeights, towerTowers);
                        if (expWeight != actualWeight)
                        {
                            Console.WriteLine(towers[0] + ":" + expWeight + " " + towers[j] + ":" + actualWeight);
                        }
                    }
                }
            }
        }

        static private int getWeight(string name, Dictionary<string, int> towerWeights, Dictionary<string, List<string>> towerTowers)
        {
            int weight;
            towerWeights.TryGetValue(name, out weight);
            if (!towerTowers.ContainsKey(name))
            {
                return weight;
            }
            else
            {
                List<string> towers;
                towerTowers.TryGetValue(name, out towers);
                for (int i = 0; i < towers.Count(); i++)
                {
                    weight += getWeight(towers[i], towerWeights, towerTowers);
                }
                return weight;
            }
        }
    }
}

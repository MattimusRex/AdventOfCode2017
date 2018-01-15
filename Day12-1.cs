using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Day12_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(@"C:\Users\matthew.lay\Documents\Visual Studio 2015\Projects\AdventOfCodeSoln\Day12-1\input.txt");
            int[] components = new int[lines.Length];
            int[] componentCounts = new int[lines.Length];
            //set all nodes to be in their own component
            InitializeComponents(components);
            //set all component counts to be 1
            InitializeComponentCounts(componentCounts);
            //read each line
            for (int i = 0; i < lines.Length; i++)
            {
                //split line into parts
                string[] parts = lines[i].Split(' ');
                //parts[0] is the node
                int curNode = Int32.Parse(parts[0]);
                //parts[2] through parts[parts.Length - 1] are connected nodes
                for (int j = 2; j < parts.Length; j++)
                {
                    //remove comma
                    int target = Int32.Parse(parts[j].Replace(",", ""));
                    //find bigger component
                    int bigComponent;
                    int smallComponent;
                    if (components[curNode] != components[target])
                    {
                        if (componentCounts[components[curNode]] >= componentCounts[components[target]])
                        {
                            bigComponent = components[curNode];
                            smallComponent = components[target];
                        }
                        else
                        {
                            bigComponent = components[target];
                            smallComponent = components[curNode];
                        }
                        //adjust counts
                        AdjustCounts(bigComponent, smallComponent, componentCounts);
                        //change components of nodes
                        AdjustComponents(bigComponent, smallComponent, components);
                    }
                }
            }
            Console.WriteLine(componentCounts[components[0]]);
        }

        static private void InitializeComponents(int[] components)
        {
            for (int i = 0; i < components.Length; i++)
            {
                components[i] = i;
            }
        }

        static private void InitializeComponentCounts(int[] counts)
        {
            for (int i = 0; i < counts.Length; i++)
            {
                counts[i] = 1;
            }
        }

        static private void AdjustComponents(int bigComp, int smallComp, int[] comps)
        {
            for (int i = 0; i < comps.Length; i++)
            {
                if (comps[i] == smallComp)
                {
                    comps[i] = bigComp;
                }
            }
        }

        static private void AdjustCounts(int bigComp, int smallComp, int[] compCounts)
        {
            compCounts[bigComp] += compCounts[smallComp];
            compCounts[smallComp] = 0;
        }
    }
}

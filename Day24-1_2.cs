using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day24_1
{
    class Program
    {

        public class Vertex
        {
            public int id;
            public int port1;
            public int port2;
            public bool visited;
            public int openPort;
            public int weight;

            public Vertex(int id, int port1, int port2)
            {
                this.id = id;
                this.port1 = port1;
                this.port2 = port2;
                visited = false;
                weight = (port1 + port2);
            }

            public bool Connected(Vertex v)
            {
                if (port1 == v.port1 || port1 == v.port2 || port2 == v.port1 || port2 == v.port2)
                {
                    return true;
                }
                return false;
            }

            public int GetOtherPort(int port)
            {
                if (port == port1)
                {
                    return port2;
                }
                else if (port == port2)
                {
                    return port1;
                }
                else
                    return -1;
            }

            public bool OpenPath(Vertex v)
            {
                if (openPort == v.port1 || openPort == v.port2)
                {
                    return true;
                }
                return false;
            }
        }

        static void Main(string[] args)
        {
            //turn input into vertices
            List<Vertex> vertices = new List<Vertex>();
            vertices.Add(new Vertex(0, 0, 0));
            vertices[0].openPort = 0;
            var lines = File.ReadAllLines(@"C:\Users\Matt\Dropbox\quarter 3\Analysis of Algorithms CS325\week 9\AdventOfCodeSoln\Day24-1\input.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                var parts = lines[i].Split('/');
                vertices.Add(new Vertex(i + 1, Int32.Parse(parts[0]), Int32.Parse(parts[1])));
            }

            //create adjacency list of vertices
            List<List<Vertex>> adjList = new List<List<Vertex>>();
            for (int i = 0; i < vertices.Count(); i++)
            {
                //create list for this vertex
                List<Vertex> temp = new List<Vertex>();
                //find connecting vertices
                for (int j = 0; j < vertices.Count(); j++)
                {
                    //dont add itself
                    if (vertices[j].id != vertices[i].id)
                    {
                        //if matching sides
                        if (vertices[i].Connected(vertices[j]))
                        {
                            Vertex v = vertices[j];
                            temp.Add(v);
                        }
                    }
                }
                adjList.Add(temp);
            }

            BruteForce(vertices, adjList, 0, 0, 0);
            //PrintAdjList(vertices, adjList);
            Console.WriteLine(max);
        }

        static int max = Int32.MinValue;
        static int maxLength = Int32.MinValue;
        static private void BruteForce(List<Vertex> vertices, List<List<Vertex>> adjList, int index, int curSum, int length)
        {   
            Vertex v = vertices[index];
            v.visited = true;
            curSum += v.weight;
            for (int i = 0; i < adjList[v.id].Count; i++)
            {
                Vertex next = adjList[v.id][i];
                if (next.visited == false && v.OpenPath(next))
                {
                    next.openPort = next.GetOtherPort(v.openPort);
                    BruteForce(vertices, adjList, next.id, curSum, length + 1);
                }
            }
            v.visited = false;
            if (length == maxLength && curSum > max) 
            {
                max = curSum;
            }
            else if (length > maxLength)
            {
                max = curSum;
                maxLength = length;
            }
        }

        static private void PrintAdjList(List<Vertex> vertices, List<List<Vertex>> adjList)
        {
            for (int i = 0; i < adjList.Count(); i++)
            {
                Console.Write(vertices[i].port1 + "/" + vertices[i].port2 + " -> ");
                for (int j = 0; j < adjList[i].Count(); j++)
                {
                    Console.Write(adjList[i][j].port1 + "/" + adjList[i][j].port2 + " ,");
                }
                Console.WriteLine();
            }
        }
    }
}

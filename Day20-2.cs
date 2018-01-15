using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20_1
{
    class Program
    {
        public class Particle
        {
            public Triplet p;
            public Triplet v;
            public Triplet a;

            public Particle(int pX, int pY, int pZ, int vX, int vY, int vZ, int aX, int aY, int aZ)
            {
                p = new Triplet(pX, pY, pZ);
                v = new Triplet(vX, vY, vZ);
                a = new Triplet(aX, aY, aZ);
            }

            public void IncreaseVelocity()
            {
                v.x += a.x;
                v.y += a.y;
                v.z += a.z;
            }

            public void IncreasePosition()
            {
                p.x += v.x;
                p.y += v.y;
                p.z += v.z;
            }

            public int DistanceFromOrigin()
            {
                return Math.Abs(p.x) + Math.Abs(p.y) + Math.Abs(p.z);
            }

            public bool Collide(Particle p)
            {
                return ((this.p.x == p.p.x) && (this.p.y == p.p.y) && (this.p.z == p.p.z));
            }

            public override string ToString()
            {
                return "p=<" + p.x + "," + p.y + "," + p.z + ">  v=< " + v.x + "," + v.y + "," + v.z + " >  a=< " + a.x + "," + a.y + "," + a.z + ">";
            }
        }

        private class TripletEqualityComparer : IEqualityComparer<Triplet>
        {
            public bool Equals(Triplet a, Triplet b)
            {
                return ((a.x == b.x) && (a.y == b.y) && (a.z == b.z));
            }

            public int GetHashCode(Triplet a)
            {
                string combined = a.x.ToString() + a.y.ToString() + a.z.ToString();
                return (combined.GetHashCode());
            }
        }

        public class Triplet
        {
            public int x;
            public int y;
            public int z;

            public Triplet(int x, int y, int z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
        }


        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(@"C:\Users\Matt\Dropbox\quarter 3\Analysis of Algorithms CS325\week 9\AdventOfCodeSoln\Day20-1\input.txt");
            List<Particle> particles = new List<Particle>(lines.Length);
            //p=parts[1,2,3]  v=parts[6,7,8]  a=parts[11,12,13] 
            for (int i = 0; i < lines.Length; i++)
            {
                var parts = lines[i].Split('<', ',', '>');
                Int32.Parse(parts[1]);
                Particle temp = new Particle(Int32.Parse(parts[1]), Int32.Parse(parts[2]), Int32.Parse(parts[3]), Int32.Parse(parts[6]),
                    Int32.Parse(parts[7]), Int32.Parse(parts[8]), Int32.Parse(parts[11]), Int32.Parse(parts[12]), Int32.Parse(parts[13]));
                particles.Add(temp);
            }

            //run simulation
            for (int i = 0; i < 10000; i++)
            {
                AdvanceParticles(particles);
                ProcessCollisions(ref particles);
            }

            Console.WriteLine(particles.Count());
        }

        private static void ProcessCollisions(ref List<Particle> particles)
        {
            HashSet<int> indexes = new HashSet<int>();
            for (int i = 0; i < particles.Count(); i++)
            {
                for (int j = i + 1; j < particles.Count(); j++)
                {
                    if (particles[i].Collide(particles[j]))
                    {
                        indexes.Add(i);
                        indexes.Add(j);
                    }
                }
            }
            List<Particle> newParticles = new List<Particle>();
            for (int i = 0; i < particles.Count(); i++)
            {
                if (!indexes.Contains(i))
                {
                    newParticles.Add(particles[i]);
                }
            }
            particles = newParticles;
        }

        private static void AdvanceParticles(List<Particle> particles)
        {
            //increase velocity
            foreach (Particle particle in particles)
            {
                particle.IncreaseVelocity();
                particle.IncreasePosition();
            }
        }
    }
}

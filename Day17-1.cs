using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int skip = 377;
            int currentIndex = 1;
            List<int> buffer = new List<int>(50000001);
            buffer.Add(0);
            buffer.Add(1);
            for (int i = 2; i < 2018; i++)
            {
                currentIndex = (currentIndex + skip) % buffer.Count();
                buffer.Insert(++currentIndex, i);
            }
            Console.WriteLine(buffer[currentIndex + 1]);
        }
    }
}

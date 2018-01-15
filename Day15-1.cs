using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Int64 prevA = 65;
            Int64 prevB = 8921;
            Int64 factorA = 16807;
            Int64 factorB = 48271;
            Int64 divisor = 2147483647;
            int count = 0;
            Queue<Int64> stackA = new Queue<Int64>();
            Queue<Int64> stackB = new Queue<Int64>();
            for (int i = 0; i < 5000000; i++)
            {
                prevA = prevA * factorA % divisor;
                prevB = prevB * factorB % divisor;
                if (prevA % 4 == 0)
                {
                    stackA.Enqueue(prevA);
                }
                if (prevB % 8 == 0)
                {
                    stackB.Enqueue(prevB);
                }

                if (stackA.Count() != 0 && stackB.Count() != 0)
                {
                    string binaryA = Convert.ToString(stackA.Dequeue(), 2).PadLeft(32, '0');
                    string binaryB = Convert.ToString(stackB.Dequeue(), 2).PadLeft(32, '0');
                    string binASub = binaryA.Substring(16);
                    string binBSub = binaryB.Substring(16);
                    if (binASub.Equals(binBSub))
                    {
                        count++;
                    }
                }
            }
            Console.WriteLine(count);
        }
    }
}

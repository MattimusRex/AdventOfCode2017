using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int counter = 0;
            int strCounter = 0;
            int[] banks = { 4, 10, 4, 1, 8, 4, 9, 14, 5, 1, 14, 15, 0, 15, 3, 5 };
            Dictionary<string, int> seen = new Dictionary<string, int>();
            string strBanks = string.Join(",", banks);
            while (!seen.ContainsKey(strBanks))
            {
                seen.Add(strBanks, strCounter++);
                //distribute blocks
                Distribute(banks);
                counter++;
                strBanks = string.Join(",", banks);
            }
            int startCount;
            seen.TryGetValue(strBanks, out startCount);
            Console.WriteLine(strCounter - startCount);
        }

        private static void Distribute(int[] banks)
        {
            //find bank with highest num of blocks
            int maxIndex = 0;
            for (int i = 1; i < banks.Length; i++)
            {
                if (banks[i] > banks[maxIndex])
                {
                    maxIndex = i;
                }
            }
            //distribute blocks
            int numBlocks = banks[maxIndex];
            banks[maxIndex] = 0;
            int index = maxIndex + 1;
            while (numBlocks > 0)
            {
                if (index >= banks.Length)
                {
                    index = index % banks.Length;
                }
                banks[index] += 1;
                index++;
                numBlocks--;
            }
        }
    }
}


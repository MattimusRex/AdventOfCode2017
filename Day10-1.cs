using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = { 189, 1, 111, 246, 254, 2, 0, 120, 215, 93, 255, 50, 84, 15, 94, 62 };
            int[] list = new int[256];
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = i;
            }
            int curPos = 0;
            int skip = 0;

            for (int i = 0; i < input.Length; i++)
            {
                int length = input[i];
                reverseLength(curPos, length, list);
                curPos = (curPos + skip + length) % list.Length;
                skip++;
            }
            Console.WriteLine(list[0] * list[1]);
        }

        static private void reverseLength(int start, int length, int[] list)
        {
            int end = (start + length - 1) % list.Length;
            for (int i = 0; i < length / 2; i++)
            {
                if (end == -1)
                {
                    end = list.Length - 1;
                }
                if (start == list.Length)
                {
                    start = 0;
                }
                int temp = list[start];
                list[start++] = list[end];
                list[end--] = temp;
            }
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //process input into assci values
            string rawInput = "189,1,111,246,254,2,0,120,215,93,255,50,84,15,94,62";
            int[] input = processRawInput(rawInput);
            int[] list = new int[256];
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = i;
            }
            int curPos = 0;
            int skip = 0;

            //run 64 rounds of hash
            for (int i = 0; i < 64; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    int length = input[j];
                    reverseLength(curPos, length, list);
                    curPos = (curPos + skip + length) % list.Length;
                    skip++;
                }
            }

            //make dense hash
            int[] denseHash = makeDense(list);

            //convert to hex string
            string hex = "";
            foreach (int num in denseHash)
            {
                hex += num.ToString("X2");
            }
            Console.WriteLine(hex);
        }

        static private int[] makeDense(int[] list)
        {
            //condense sparse hash into dense hash
            int[] denseHash = new int[16];
            int denseIndex = 0;
            int xorResult = list[0];
            for (int i = 1; i < list.Length; i++)
            {
                if (i % 16 == 0)
                {
                    denseHash[denseIndex++] = xorResult;
                    xorResult = list[i];
                }
                else
                {
                    xorResult ^= list[i];
                }
            }
            denseHash[denseIndex] = xorResult;
            return denseHash;
        }

        static private int[] processRawInput(string rawInput)
        {
            int[] input = new int[rawInput.Length + 5];
            int[] suffix = { 17, 31, 73, 47, 23 };
            int i = 0;
            foreach (char c in rawInput)
            {
                input[i++] = c;
            }
            foreach (int num in suffix)
            {
                input[i++] = num;
            }
            return input;
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

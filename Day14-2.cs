using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14_2
{
    class Program
    {
        public class XYCor
        {
            public int x;
            public int y;

            public XYCor(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }


        static void Main(string[] args)
        {
            //process input into assci values
            string inputStringPrefix = "hfdlxzhv";
            int[][] matrix = new int[128][];
            FillMatrix(matrix, inputStringPrefix);
            int count = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    if (matrix[i][j] == 1)
                    {
                        count++;
                        XYCor start = new XYCor(i, j);
                        FindRegion(matrix, start);
                    }
                }
            }
            Console.WriteLine(count);
        }

        static private void FindRegion(int[][] matrix, XYCor start)
        {
            Queue<XYCor> todo = new Queue<XYCor>();
            todo.Enqueue(start);
            while (todo.Count() != 0)
            {
                XYCor current = todo.Dequeue();
                AddNeighbors(matrix, todo, current);
                matrix[current.x][current.y] = -1;
            }
        }

        static private void AddNeighbors(int[][] matrix, Queue<XYCor> todo, XYCor current)
        {
            int x = current.x;
            int y = current.y;
            if (x - 1 > -1 && matrix[x - 1][y] == 1)
            {
                XYCor temp = new XYCor(x - 1, y);
                todo.Enqueue(temp);
            }
            if (x + 1 < matrix.Length && matrix[x + 1][y] == 1)
            {
                XYCor temp = new XYCor(x + 1, y);
                todo.Enqueue(temp);
            }
            if (y - 1 > -1 && matrix[x][y - 1] == 1)
            {
                XYCor temp = new XYCor(x, y - 1);
                todo.Enqueue(temp);
            }
            if (y + 1 < matrix.Length && matrix[x][y + 1] == 1)
            {
                XYCor temp = new XYCor(x, y + 1);
                todo.Enqueue(temp);
            }
        }

        static private void FillMatrix(int[][] matrix, string inputStringPrefix)
        {
            for (int i = 0; i < 128; i++)
            {
                string key = inputStringPrefix + "-" + i.ToString();
                string hex = knotHash(key);
                string binary = "";
                for (int j = 0; j < hex.Length; j++)
                {
                    binary += Convert.ToString(Convert.ToInt32(hex[j].ToString(), 16), 2).PadLeft(4, '0');
                }
                int[] temp = new int[128];
                for (int j = 0; j < temp.Length; j++)
                {
                    temp[j] = Int32.Parse(binary[j].ToString());
                }
                matrix[i] = temp;
            }
        }

        static private int CountOnes(string binary)
        {
            int sum = 0;
            foreach (char c in binary)
            {
                if (c == '1')
                {
                    sum++;
                }
            }
            return sum;
        }

        static private string knotHash(string key)
        {
            //process input into assci values
            int[] input = StringToAscii(key);
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
            return hex;
        }

        static private int[] StringToAscii(string inputString)
        {
            int[] input = new int[inputString.Length + 5];
            int i = 0;
            int[] suffix = { 17, 31, 73, 47, 23 };
            foreach (char c in inputString)
            {
                input[i++] = c;
            }
            foreach (int num in suffix)
            {
                input[i++] = num;
            }
            return input;
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

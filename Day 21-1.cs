using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(@"C:\Users\Matt\Dropbox\quarter 3\Analysis of Algorithms CS325\week 9\AdventOfCodeSoln\Day21-1\input.txt");
            Dictionary<string, string> rules = new Dictionary<string, string>();
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(' ');
                rules.Add(parts[0], parts[2]);
            }
            string start = ".#./..#/###";
            char[][] arr = StringToArray(start);
            List<char[][]> arrays;

            //run 5 iterations
            for (int i = 0; i < 5; i++)
            {
                //divide arrays
                arrays = DivideArr(arr);

                //enhance
                for (int j = 0; j < arrays.Count(); j++)
                {
                    arrays[j] = EnhanceArray(arrays[j], rules);
                }

                //combine arrays
                arr = CombineArrays(arrays);
            }

            //count how many "on"
            Console.WriteLine(CountOnPixels(arr));
        }

        static private int CountOnPixels(char[][] arr)
        {
            int count = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] == '#')
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        static private char[][] EnhanceArray(char[][] arr, Dictionary<string, string> rules)
        {
            string oldArrString = ArrayToString(arr);
            string newArrString;
            //try original
            if (rules.TryGetValue(oldArrString, out newArrString))
            {
                return StringToArray(newArrString);
            }
            //try flip vert
            char[][] alteredArr = FlipArrayVert(arr);
            oldArrString = ArrayToString(alteredArr);
            if (rules.TryGetValue(oldArrString, out newArrString))
            {
                return StringToArray(newArrString);
            }
            //try flip hori
            alteredArr = FlipArrayHori(arr);
            oldArrString = ArrayToString(alteredArr);
            if (rules.TryGetValue(oldArrString, out newArrString))
            {
                return StringToArray(newArrString);
            }
            //try each rotation
            alteredArr = RotateArrayRight(arr);
            oldArrString = ArrayToString(alteredArr);
            if (rules.TryGetValue(oldArrString, out newArrString))
            {
                return StringToArray(newArrString);
            }
            alteredArr = RotateArrayRight(alteredArr);
            oldArrString = ArrayToString(alteredArr);
            if (rules.TryGetValue(oldArrString, out newArrString))
            {
                return StringToArray(newArrString);
            }
            alteredArr = RotateArrayRight(alteredArr);
            oldArrString = ArrayToString(alteredArr);
            if (rules.TryGetValue(oldArrString, out newArrString))
            {
                return StringToArray(newArrString);
            }

            //flip vertically and try all rotations
            char[][] flippedArr = FlipArrayVert(arr);
            alteredArr = RotateArrayRight(flippedArr);
            oldArrString = ArrayToString(alteredArr);
            if (rules.TryGetValue(oldArrString, out newArrString))
            {
                return StringToArray(newArrString);
            }
            alteredArr = RotateArrayRight(flippedArr);
            oldArrString = ArrayToString(alteredArr);
            if (rules.TryGetValue(oldArrString, out newArrString))
            {
                return StringToArray(newArrString);
            }
            alteredArr = RotateArrayRight(flippedArr);
            oldArrString = ArrayToString(alteredArr);
            if (rules.TryGetValue(oldArrString, out newArrString))
            {
                return StringToArray(newArrString);
            }

            //flip hori and try all rotations
            flippedArr = FlipArrayHori(arr);
            alteredArr = RotateArrayRight(flippedArr);
            oldArrString = ArrayToString(alteredArr);
            if (rules.TryGetValue(oldArrString, out newArrString))
            {
                return StringToArray(newArrString);
            }
            alteredArr = RotateArrayRight(flippedArr);
            oldArrString = ArrayToString(alteredArr);
            if (rules.TryGetValue(oldArrString, out newArrString))
            {
                return StringToArray(newArrString);
            }
            alteredArr = RotateArrayRight(flippedArr);
            oldArrString = ArrayToString(alteredArr);
            if (rules.TryGetValue(oldArrString, out newArrString))
            {
                return StringToArray(newArrString);
            }

            //nothing matches, something wrong
            else
            {
                Console.WriteLine("ERROR!");
                return new char[1][];
            }
        }


        static private char[][] CombineArrays(List<char[][]> arrays)
        {
            if (arrays.Count() == 1)
            {
                return arrays[0];
            }

            int newArraySize = (int)Math.Sqrt(arrays.Count()) * arrays[0].Length;
            char[][] arr = new char[newArraySize][];
            for (int i = 0; i < arr.Length; i++)
            {
                char[] newRow = new char[arr.Length];
                arr[i] = newRow;
            }

            int iOffset = 0;
            int jOffset = 0;

            for (int arrIndex = 0; arrIndex < arrays.Count(); arrIndex++)
            {
                char[][] smallArr = arrays[arrIndex];
                for (int i = 0; i < smallArr.Length; i++)
                {
                    for (int j = 0; j < smallArr[i].Length; j++)
                    {
                        arr[i + iOffset][j + jOffset] = smallArr[i][j];
                    }
                }
                iOffset += smallArr.Length;
                if (iOffset % newArraySize == 0)
                {
                    jOffset += smallArr.Length;
                    iOffset = 0;
                }
            }
            return arr; 
        }


        static private List<char[][]> DivideArr(char[][] arr)
        {
            int size = arr.Length;
            int divisor = size % 2 == 0 ? 2 : 3;
            int numOfArrays = (size * size) / (divisor * divisor);
            List<char[][]> arrays = new List<char[][]>(numOfArrays);
            //iterate through the individual smaller arrays in the big array
            for (int i = 0; i < arr.Length; i += divisor)
            {
                for (int j = 0; j < arr[i].Length; j += divisor)
                {
                    char[][] newArr = new char[divisor][];
                    for (int k = i, m = 0; k < divisor + i; k++, m++)
                    {
                        char[] newRow = new char[divisor];
                        newArr[m] = newRow;
                        for (int l = j, n = 0; l < divisor + j; l++, n++)
                        {
                            newArr[m][n] = arr[k][l];
                        }
                    }
                    arrays.Add(newArr);
                }
            }
            return arrays;
        }


        static private char[][] RotateArrayRight(char[][] arr)
        {
            char[][] rotatedArr = new char[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
            {
                char[] newRow = new char[arr[i].Length];
                rotatedArr[i] = newRow;
            }

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    rotatedArr[j][arr[i].Length -  1 - i] = arr[i][j];
                }
            }
            return rotatedArr;
        }


        static private char[][] FlipArrayHori(char[][] arr)
        {
            char[][] flippedArr = new char[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
            {
                char[] newRow = new char[arr[i].Length];
                flippedArr[i] = newRow;
            }

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    flippedArr[j][i] = arr[j][arr.Length - 1 - i];
                }
            }
            return flippedArr;
        }


        static private char[][] FlipArrayVert(char[][] arr)
        {
            char[][] flippedArr = new char[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
            {
                char[] newRow = new char[arr[i].Length];
                flippedArr[i] = newRow;
            }

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    flippedArr[flippedArr.Length - 1 - i][j] = arr[i][j];
                }
            }
            return flippedArr;
        }


        static private string ArrayToString(char[][] arr)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    str.Append(arr[i][j]);
                }
                str.Append('/');
            }
            str.Remove(str.Length - 1, 1);
            return str.ToString();
        }


        static private char[][] StringToArray(string str)
        {
            var rows = str.Split('/');
            char[][] arr = new char[rows.Length][];
            for (int i = 0; i < rows.Length; i++)
            {
                char[] newRow = new char[rows[i].Length];
                arr[i] = newRow;
                for (int j = 0; j < rows[i].Length; j++)
                {
                    arr[i][j] = rows[i][j];
                }
            }
            return arr;
        }


        static private void PrintArray(char[][] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(new string(arr[i]));
            }
        }

        static private void PrintList(List<char[][]> arrays)
        {
            foreach (char[][] arr in arrays)
            {
                PrintArray(arr);
            }
        }

        static private void RunTests()
        {
            //string testStart = ".#./..#/###";
            //string testSize4 = "#..#/..../..../#..#";
            //string testCombineString = "##./#../...";
            //TestConversion(start);
            //TestFlips(start);
            //TestRotation(start);
            //TestDivide(start);
            //TestCombine(testCombineString);
        }

        static private void TestConversion(string testString)
        {
            Console.WriteLine("Testing Conversion Methods");
            Console.WriteLine("Original String:  " + testString);
            char[][] testArr = StringToArray(testString);
            Console.WriteLine("Converted to Array");
            PrintArray(testArr);
            string stringFromArray = ArrayToString(testArr);
            Console.WriteLine("Back to String:  " + stringFromArray);
            Console.WriteLine("");
        }

        static private void TestFlips(string testString)
        {
            char[][] testArr = StringToArray(testString);

            Console.WriteLine("Testing Flips");
            Console.WriteLine("Original Array");
            PrintArray(testArr);
            char[][] flippedArr = FlipArrayVert(testArr);
            Console.WriteLine("Flipped Vertically");
            PrintArray(flippedArr);
            flippedArr = FlipArrayVert(flippedArr);
            Console.WriteLine("Flipped Back To Original");
            PrintArray(flippedArr);
            flippedArr = FlipArrayHori(testArr);
            Console.WriteLine("Flipped Horizontally");
            PrintArray(flippedArr);
            flippedArr = FlipArrayHori(flippedArr);
            Console.WriteLine("Flipped Back To Original");
            PrintArray(flippedArr);
            Console.WriteLine("");
        }

        static private void TestRotation(string testString)
        {
            char[][] testArr = StringToArray(testString);

            Console.WriteLine("Testing Rotation");
            Console.WriteLine("Original Array");
            PrintArray(testArr);
            char[][] rotatedArr = RotateArrayRight(testArr);
            Console.WriteLine("Rotated Once");
            PrintArray(rotatedArr);
            rotatedArr = RotateArrayRight(rotatedArr);
            Console.WriteLine("Rotated Twice");
            PrintArray(rotatedArr);
            rotatedArr = RotateArrayRight(rotatedArr);
            Console.WriteLine("Rotated Three Times");
            PrintArray(rotatedArr);
            rotatedArr = RotateArrayRight(rotatedArr);
            Console.WriteLine("Rotated Four Times");
            PrintArray(rotatedArr);
            Console.WriteLine("Original Array");
            PrintArray(testArr);
        }

        static private void TestDivide(string testString)
        {
            char[][] testArr = StringToArray(testString);

            Console.WriteLine("Testing Divide");
            Console.WriteLine("Original Array");
            PrintArray(testArr);
            Console.WriteLine("Split Arrays");
            List<char[][]> arrays = DivideArr(testArr);
            PrintList(arrays);
        }

        static private void TestCombine(string testString)
        {
            char[][] testArr = StringToArray(testString);

            Console.WriteLine("Testing Combine");
            Console.WriteLine("Small Array");
            PrintArray(testArr);
            Console.WriteLine("Combine Arrays");
            List<char[][]> arrays = new List<char[][]>();
            arrays.Add(testArr);
            arrays.Add(testArr);
            arrays.Add(testArr);
            arrays.Add(testArr);
            testArr = CombineArrays(arrays);
            PrintArray(testArr);
        }
    }
}

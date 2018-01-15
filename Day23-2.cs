using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23_2
{
    class Program
    {
        static void Main(string[] args)
        {
            long h = 0;
            long b = 93;
            long c = b;
            b *= 100;
            b -= -100000;
            c = b;
            c -= -17000;

            while (b - (c + 17) != 0)
            {
                if (!IsPrime(b))
                {
                    h++;
                }
                b += 17;
            }
            Console.WriteLine(h);
        }

        static private bool IsPrime(long number)
        {
            int boundary = (int)Math.Floor(Math.Sqrt(number));
            for (int i = 2; i <= boundary; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        static private void OldAttempt()
        {
            var lines = File.ReadAllLines(@"C:\Users\Matt\Dropbox\quarter 3\Analysis of Algorithms CS325\week 9\AdventOfCodeSoln\Day23-1\input.txt");
            int counter = 0;
            //create registers
            Dictionary<string, long> registers = new Dictionary<string, long>();

            //set all registers to 0
            for (int i = 0; i< 8; i++)
            {
                char reg = (char)(i + 97);
                registers.Add(reg.ToString(), 0);
            }

             registers["a"] = 1;

            //main loop
            for (long i = 0; i<lines.Length; i++)
            {
                counter++;
                //0 is action, 1 and 2 are regs
                //get parts of the line that program0 and program1 are on
                string[] parts = lines[i].Split(' ');
                //do the action
                Console.WriteLine(lines[i]);
                PerformInstruction(parts, registers, ref i);
            }
        }

        static private bool IsRegister(string str)
        {
            return Char.IsLetter(str[0]);
        }

        static private void PerformInstruction(string[] parts, Dictionary<string, long> registers, ref long index)
        {
            string instruction = parts[0];
            long value;
            if (IsRegister(parts[2]))
            {
                value = registers[parts[2]];
            }
            else
            {
                value = Int64.Parse(parts[2]);
            }

            //set register to value
            if (instruction == "set")
            {
                registers[parts[1]] = value;
                Console.WriteLine("Set - Register " + parts[1] + " now " + registers[parts[1]]);
            }

            //add value to register
            else if (instruction == "sub")
            {
                registers[parts[1]] -= value;
                Console.WriteLine("Sub - Register " + parts[1] + " now " + registers[parts[1]]);
            }

            //multiply register by value 
            else if (instruction == "mul")
            {
                registers[parts[1]] *= value;
                Console.WriteLine("Mul - Register " + parts[1] + " now " + registers[parts[1]]);
            }

            //jump
            else if (instruction == "jnz")
            {
                if ((IsRegister(parts[1]) && registers[parts[1]] != 0) || (!IsRegister(parts[1]) && Int64.Parse(parts[1]) != 0))
                {
                    index += value - 1;
                }
            }
        }
    }
}

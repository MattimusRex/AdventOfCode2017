using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(@"C:\Users\matthew.lay\Documents\Visual Studio 2015\Projects\AdventOfCodeSoln\Day18-1\input.txt");
            Dictionary<string, Int64> registers = new Dictionary<string, Int64>();
            Int64 lastPlayed = -123456;
            bool done = false;
            for (Int64 i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
                //0 is action, 1 is register, 2 is value (can be number or register)
                //OR 0 is action and 2 is value (can be number or register
                string[] parts = lines[i].Split(' ');
                
                //if this register isnt in the dict yet, add it with value of 0
                if (!registers.ContainsKey(parts[1]))
                {
                    registers.Add(parts[1], 0);
                }
                if (parts.Length == 3)
                {
                    if (IsRegister(parts[2]))
                    {
                        if (!registers.ContainsKey(parts[2]))
                        {
                            registers.Add(parts[2], 0);
                        }
                    }
                }

                //do the action
                PerformInstruction(parts, registers, ref lastPlayed, ref done, ref i);
                if (done)
                {
                    break;
                }
            }
            Console.WriteLine(lastPlayed);
        }

        static private bool IsRegister(string str)
        {
            return Char.IsLetter(str[0]);
        }

        static private void PerformInstruction(string[] parts, Dictionary<string, Int64> registers, ref Int64 lastPlayed, ref bool done, ref Int64 i)
        {
            string instruction = parts[0];
            Int64 value;

            if (parts.Length == 2)
            {
                if (IsRegister(parts[1]))
                {
                    value = registers[parts[1]];
                }
                else
                {
                    value = Int64.Parse(parts[1]);
                }
            }
            else
            {
                if (IsRegister(parts[2]))
                {
                    value = registers[parts[2]];
                }
                else
                {
                    value = Int64.Parse(parts[2]);
                }
            }

            //store value as last sound played
            if (instruction == "snd")
            {
                lastPlayed = value;
                Console.WriteLine(lastPlayed);
            }
            
            //set register to value
            else if (instruction == "set")
            {
                registers[parts[1]] = value;
                Console.WriteLine("Set - Register " + parts[1] + " now " + registers[parts[1]]);
            } 

            //add value to register
            else if (instruction == "add")
            {
                registers[parts[1]] += value;
                Console.WriteLine("Add - Register " + parts[1] + " now " + registers[parts[1]]);
            }

            //multiply register by value 
            else if (instruction == "mul")
            {
                registers[parts[1]] *= value;
                Console.WriteLine("Mul - Register " + parts[1] + " now " + registers[parts[1]]);
            }

            //mod register by value
            else if (instruction == "mod")
            {
                if (value == 0)
                {
                    registers[parts[1]] = 0;
                    Console.WriteLine("Error div by 0");
                }
                else
                {
                    registers[parts[1]] = registers[parts[1]] % value;
                    Console.WriteLine("Mod - Register " + parts[1] + " now " + registers[parts[1]]);
                }
            }

            else if (instruction == "rcv")
            {
                if (value != 0)
                {
                    done = true;
                }
            }

            //jump
            else if (instruction == "jgz")
            {
                Int64 testValue;

                if (IsRegister(parts[1]))
                {
                    testValue = registers[parts[1]];
                }
                else
                {
                    testValue = Int32.Parse(parts[1]);
                }

                if (testValue > 0)
                {
                    i += value - 1;
                }
            } 
        }
    }
}

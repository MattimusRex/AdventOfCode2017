using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18_2
{
    class Program
    {
        static private bool blocked0 = false;
        static private bool blocked1 = false;
        static private int count = 0;
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(@"C:\Users\matthew.lay\Documents\Visual Studio 2015\Projects\AdventOfCodeSoln\Day18-1\input.txt");
            
            //create both sets of registers
            Dictionary<string, long> registers0 = new Dictionary<string, long>();
            Dictionary<string, long> registers1 = new Dictionary<string, long>();
            
            //create both queues
            Queue<long> q0 = new Queue<long>();
            Queue<long> q1 = new Queue<long>();
            
            //add program id to registers
            registers0.Add("p", 0);
            registers1.Add("p", 1);

            //add non changing indentifier to registers
            registers0.Add("reg", 0);
            registers1.Add("reg", 1);

            //main loop
            //uses i index for registers0 and j index for registers1
            for (long i = 0, j= 0; i < lines.Length; i++, j++)
            {
                //0 is action, 1 is register, 2 is value (can be number or register)
                //OR 0 is action and 2 is value (can be number or register
                //get parts of the line that program0 and program1 are on
                string[] parts0 = lines[i].Split(' ');
                string[] parts1 = lines[j].Split(' ');

                //if this register isnt in the dict yet, add it with value of 0
                if (!registers0.ContainsKey(parts0[1]))
                {
                    registers0.Add(parts0[1], 0);
                    
                }
                if (!registers1.ContainsKey(parts1[1]))
                {
                    registers1.Add(parts1[1], 0);
                }

                //if this register isnt in the dict yet, add it with value of 0
                if (parts0.Length == 3)
                {
                    if (IsRegister(parts0[2]))
                    {
                        if (!registers0.ContainsKey(parts0[2]))
                        {
                            registers0.Add(parts0[2], 0);
                        }
                    }
                }
                if (parts1.Length == 3)
                {
                    if (IsRegister(parts1[2]))
                    {
                        if (!registers1.ContainsKey(parts1[2]))
                        {
                            registers1.Add(parts1[2], 0);
                        }
                    }
                }



                //do the action
                PerformInstruction(parts0, registers0, ref i, q0, q1);
                PerformInstruction(parts1, registers1, ref j, q0, q1);

                if (blocked0 == true && blocked1 == true)
                {
                    Console.WriteLine(count);
                    break;
                }

            }
        }

        static private bool IsRegister(string str)
        {
            return Char.IsLetter(str[0]);
        }

        static private void PerformInstruction(string[] parts, Dictionary<string, long> registers, ref long index, Queue<long> q0, Queue<long> q1)
        {
            string instruction = parts[0];
            long value;

            if (parts.Length == 2)
            {
                if (IsRegister(parts[1]))
                {
                    value = registers[parts[1]];
                }
                else
                {
                    value = long.Parse(parts[1]);
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
                    value = long.Parse(parts[2]);
                }
            }

            if (instruction == "snd")
            {
                if (registers["reg"] == 0)
                {
                    q1.Enqueue(value);
                }
                else
                {
                    count++;
                    q0.Enqueue(value);
                }
            }

            //set register to value
            else if (instruction == "set")
            {
                registers[parts[1]] = value;
                //Console.WriteLine("Set - Register " + parts[1] + " now " + registers[parts[1]]);
            }

            //add value to register
            else if (instruction == "add")
            {
                registers[parts[1]] += value;
                //Console.WriteLine("Add - Register " + parts[1] + " now " + registers[parts[1]]);
            }

            //multiply register by value 
            else if (instruction == "mul")
            {
                registers[parts[1]] *= value;
                //Console.WriteLine("Mul - Register " + parts[1] + " now " + registers[parts[1]]);
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
                    //Console.WriteLine("Mod - Register " + parts[1] + " now " + registers[parts[1]]);
                }
            }

            else if (instruction == "rcv")
            {
                if (registers["reg"] == 0)
                {
                    if (q0.Count() == 0)
                    {
                        blocked0 = true;
                        index--;
                    }
                    else
                    {
                        blocked0 = false;
                        registers[parts[1]] = q0.Dequeue();
                    }
                }
                if (registers["reg"] == 1)
                {
                    if (q1.Count() == 0)
                    {
                        blocked1 = true;
                        index--;
                    }
                    else
                    {
                        blocked1 = false;
                        registers[parts[1]] = q1.Dequeue();
                    }
                }
            }

            //jump
            else if (instruction == "jgz")
            {
                long testValue;

                if (IsRegister(parts[1]))
                {
                    testValue = registers[parts[1]];
                }
                else
                {
                    testValue = long.Parse(parts[1]);
                }

                if (testValue > 0)
                {
                    index += value - 1;
                }
            }
        }
    }
}

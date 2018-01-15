using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8_2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Dictionary<string, int> registers = new Dictionary<string, int>();
            int maxValue = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                //0 is register to be worked on
                //1 is inc or dec
                //2 is amount
                //3 is 'if'
                //4 is conditional register
                //5 is conditional operator
                //6 is conditional value
                string[] parts = lines[i].Split(' ');
                int conditionalRegisterValue = GetValue(parts[4], registers);
                if (CheckCondition(conditionalRegisterValue, parts[6], parts[5]))
                {
                    ModifyRegister(parts[0], parts[1], parts[2], registers);
                    if (registers[parts[0]] > maxValue)
                    {
                        maxValue = registers[parts[0]];
                    }
                }
            }
            Console.WriteLine(maxValue);
        }

        private static void ModifyRegister(string register, string oper, string value,
            Dictionary<string, int> registers)
        {
            int intValue = Int32.Parse(value);
            if (oper == "inc")
            {
                if (registers.ContainsKey(register))
                {
                    registers[register] += intValue;
                }
                else
                {
                    registers[register] = intValue;
                }
            }
            else if (oper == "dec")
            {
                if (registers.ContainsKey(register))
                {
                    registers[register] -= intValue;
                }
                else
                {
                    registers[register] = -intValue;
                }
            }
        }

        private static int GetValue(string register, Dictionary<string, int> registers)
        {
            int conditionalRegisterValue;
            if (registers.ContainsKey(register))
            {
                conditionalRegisterValue = registers[register];
            }
            else
            {
                conditionalRegisterValue = 0;
            }
            return conditionalRegisterValue;
        }

        private static bool CheckCondition(int intX, string y, string condition)
        {
            int intY = Int32.Parse(y);
            if (condition == ">")
            {
                return intX > intY;

            }
            else if (condition == ">=")
            {
                return intX >= intY;
            }
            else if (condition == "<")
            {
                return intX < intY;
            }
            else if (condition == "<=")
            {
                return intX <= intY;
            }
            else if (condition == "==")
            {
                return intX == intY;
            }
            else if (condition == "!=")
            {
                return intX != intY;
            }
            else
            {
                Console.WriteLine(condition);
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day25_1
{
    class Program
    {
        const int NUMBER_OF_STEPS = 12656374;

        static void Main(string[] args)
        {
            char state = 'A';
            Dictionary<int, int> tape = new Dictionary<int, int>();
            int position = 0;
            int counter = 0;

            for (int i = 0; i < NUMBER_OF_STEPS; i++)
            {
                int value;
                if (!tape.TryGetValue(position, out value))
                {
                    value = 0;
                    tape.Add(position, value);
                }
                ExecuteState(ref state, ref position, ref value, tape, ref counter);
            }
            Console.WriteLine(counter);
        }

        private static void ExecuteState(ref char state, ref int position, ref int value, Dictionary<int, int> tape, ref int counter)
        {
            if (state == 'A')
            {
                if (value == 0)
                {
                    value = 1;
                    tape[position] = value;
                    counter++;
                    position++;
                    state = 'B';
                }
                //value is 1
                else
                {
                    value = 0;
                    tape[position] = value;
                    counter--;
                    position--;
                    state = 'C';
                }
            }

            else if (state == 'B')
            {
                if (value == 0)
                {
                    value = 1;
                    tape[position] = value;
                    counter++;
                    position--;
                    state = 'A';
                }
                //value is 1
                else
                {
                    position--;
                    state = 'D';
                }
            }

             else if (state == 'C')
            {
                if (value == 0)
                {
                    value = 1;
                    tape[position] = value;
                    counter++;
                    position++;
                    state = 'D';
                }
                //value is 1
                else
                {
                    value = 0;
                    tape[position] = value;
                    counter--;
                    position++;
                    state = 'C';
                }
            }

            else if (state == 'D')
            {
                if (value == 0)
                {
                    position--;
                    state = 'B';
                }
                //value is 1
                else
                {
                    value = 0;
                    tape[position] = value;
                    counter--;
                    position++;
                    state = 'E';
                }
            }

            else if (state == 'E')
            {
                if (value == 0)
                {
                    value = 1;
                    tape[position] = value;
                    counter++;
                    position++;
                    state = 'C';
                }
                //value is 1
                else
                {
                    position--;
                    state = 'F';
                }
            }

            else if (state == 'F')
            {
                if (value == 0)
                {
                    value = 1;
                    tape[position] = value;
                    counter++;
                    position--;
                    state = 'E';
                }
                //value is 1
                else
                {
                    position++;
                    state = 'A';
                }
            }
        }
    }
}

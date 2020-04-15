using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;

namespace DremDOS
{
    public class Calculator
    {
        public static void Calculate(string[] arguments)
        {
            if (arguments.Length == 3)
            {
                if (int.TryParse(arguments[0], out int intA) && int.TryParse(arguments[2], out int intB))
                {
                    if (arguments[1] == "+")
                    {
                        Console.Write(intA + intB);
                    }
                    else if (arguments[1] == "-")
                    {
                        Console.Write(intA - intB);
                    }
                    else if (arguments[1] == "*")
                    {
                        Console.Write(intA * intB);
                    }
                    else if (arguments[1] == "/")
                    {
                        Console.Write(intA / intB);
                    }
                    else if (arguments[1] == "%")
                    {
                        Console.Write(intA % intB);
                    }
                    else
                    {
                        Console.Write("Error: Invalid operator. Supported: +, -, *, /, %");
                    }

                }
                else
                {
                    Console.Write("Error: One or both numbers are not valid. Is it a number?");
                }
            }
            else
            {
                Console.Write("Error: Incorrect amount of arguments. Expects 3. Did you seperate the ints and operator with spaces?");
            }
        }
    }
}
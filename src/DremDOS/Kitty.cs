using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;

namespace DremDOS
{
    public class Kitty
    {
        public static void kitty(string[] arguments)
        {
            if (arguments.Length == 0)
            {
                Console.WriteLine("Kitty 0.0.1");
                Console.WriteLine("\"We find the defendant kitty\"\n");
                Kernel.Help(new string[1] { "kitty" });
            }
            else if (arguments.Length > 1)
            {
                if (arguments[1] == "/r")
                {
                    if (int.TryParse(arguments[0].Split(":")[0], out int temp) && File.Exists(arguments[0]))
                    {
                        Console.WriteLine("Contents of " + arguments[0] + ":\n");
                        string text = System.IO.File.ReadAllText(arguments[0]);
                        Console.WriteLine(text);
                    }
                    else if (File.Exists(Directory.GetCurrentDirectory() + arguments[0]))
                    {
                        Console.WriteLine("Contents of " + Directory.GetCurrentDirectory() + arguments[0] + ":\n");
                        string text = System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + arguments[0]);
                        Console.WriteLine(text);
                    }
                    else
                    {
                        Console.WriteLine("Error: this file does not exist");
                    }
                }
                else if (arguments[1] == "/w")
                {
                    if (arguments.Length > 2)
                    {
                        if (int.TryParse(arguments[0].Split(":")[0], out int temp) && File.Exists(arguments[0]))
                        {
                            Console.WriteLine("Writing to " + arguments[0]);
                            System.IO.File.WriteAllText(arguments[0], arguments[2]);
                        }
                        else if (File.Exists(Directory.GetCurrentDirectory() + arguments[0]))
                        {
                            Console.WriteLine("Writing to " + Directory.GetCurrentDirectory() + arguments[0]);
                            System.IO.File.WriteAllText((Directory.GetCurrentDirectory() + arguments[0]), arguments[2]);
                        }
                        else
                        {
                            Console.WriteLine("Error: this file does not exist");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: No text to write to file");
                    }
                }
                else if (arguments[1] == "/nf")
                {
                    if (int.TryParse(arguments[0].Split(":")[0], out int temp))
                    {
                        Console.WriteLine("Error: Please only use this to create a file in the current directory");
                    }
                    else
                    {
                        if (arguments[0].Split(".").Length == 2)
                        {
                            File.Create(Directory.GetCurrentDirectory() + arguments[0]);
                        } else
                        {
                            Console.WriteLine("Error: Please add an extension to the end of the filename or ensure there is\n only one period in the filename.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error: Invalid switch.");
                }
            }
            else
            {
                Console.WriteLine("Kitty 0.0.1");
                Console.WriteLine("\"We find the defendant kitty\"\n");
                Kernel.Help(new string[1] { "kitty" });
            }
        }
    }
}
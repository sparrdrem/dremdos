using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;

namespace DremDOS
{
    public class FileOperations
    {
        public static void removeFile(string[] arguments)
        {
            if (arguments.Length >= 0)
            {
                if (int.TryParse(arguments[0].Split(":")[0], out int temp) && File.Exists(arguments[0]))
                {
                    File.Delete(arguments[0]);
                    Console.WriteLine(arguments[0] + " deleted.");
                }
                else if (File.Exists(Directory.GetCurrentDirectory() + arguments[0]))
                {
                    File.Delete(Directory.GetCurrentDirectory() + arguments[0]);
                    Console.WriteLine(Directory.GetCurrentDirectory() + arguments[0] + " deleted.");
                }
                else
                {
                    Console.WriteLine("Error: File does not exist");
                }
            }
            else
            {
                Console.WriteLine("Error: This file does not exist");
            }
        }

        public static void copyFile(string[] arguments)
        {
            if (!(arguments.Length == 2))
            {
                Kernel.Help(new string[1] { "copy" });
            }
            else
            {
                if (int.TryParse(arguments[0].Split(":")[0], out int temp) && File.Exists(arguments[0]))
                {
                    if (int.TryParse(arguments[1].Split(":")[0], out temp))
                    {
                        File.Copy(arguments[0], arguments[1]);
                        Console.WriteLine("File copied");
                    }
                    else
                    {
                        File.Copy(arguments[0], Directory.GetCurrentDirectory() + arguments[1]);
                        Console.WriteLine("File copied");
                    }
                }
                else if (File.Exists(Directory.GetCurrentDirectory() + arguments[0]))
                {
                    if (int.TryParse(arguments[1].Split(":")[0], out temp))
                    {
                        File.Copy(Directory.GetCurrentDirectory() + arguments[0], arguments[1]);
                        Console.WriteLine("File copied");
                    }
                    else
                    {
                        File.Copy(Directory.GetCurrentDirectory() + arguments[0], Directory.GetCurrentDirectory() + arguments[1]);
                        Console.WriteLine("File copied");
                    }
                }
                else
                {
                    Console.WriteLine("Error: File doesn't exist");
                }

            }
        }

        public static void moveFile(string[] arguments)
        {
            if (!(arguments.Length == 2))
            {
                Kernel.Help(new string[1] { "move" });
            }
            else
            {
                if (int.TryParse(arguments[0].Split(":")[0], out int temp) && File.Exists(arguments[0]))
                {
                    if (int.TryParse(arguments[1].Split(":")[0], out temp))
                    {
                        //File.Move(arguments[0], arguments[1]);
                        File.Copy(arguments[0], arguments[1]);
                        File.Delete(arguments[0]);
                        Console.WriteLine("File moved");
                    }
                    else
                    {
                        //File.Move(arguments[0], Directory.GetCurrentDirectory() + arguments[1]);
                        File.Copy(arguments[0], Directory.GetCurrentDirectory() + arguments[1]);
                        File.Delete(arguments[0]);
                        Console.WriteLine("File moved");
                    }
                }
                else if (File.Exists(Directory.GetCurrentDirectory() + arguments[0]))
                {
                    if (int.TryParse(arguments[1].Split(":")[0], out temp))
                    {
                        //File.Move(Directory.GetCurrentDirectory() + arguments[0], arguments[1]);
                        File.Copy(Directory.GetCurrentDirectory() + arguments[0], arguments[1]);
                        File.Delete(Directory.GetCurrentDirectory() + arguments[0]);
                        Console.WriteLine("File moved");
                    }
                    else
                    {
                        //File.Move(Directory.GetCurrentDirectory() + arguments[0], Directory.GetCurrentDirectory() + arguments[1]);
                        File.Copy(Directory.GetCurrentDirectory() + arguments[0], Directory.GetCurrentDirectory() + arguments[1]);
                        File.Delete(Directory.GetCurrentDirectory() + arguments[0]);
                        Console.WriteLine("File moved");
                    }
                }
                else
                {
                    Console.WriteLine("Error: File doesn't exist");
                }
                
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;

namespace DremDOS
{
    public class DirectoryOperations
    {
        public static void removeDirectory(string[] arguments)
        {
            if (arguments.Length >= 0)
            {
                if (int.TryParse(arguments[0].Split(":")[0], out int temp) && Directory.Exists(arguments[0]))
                {
                    if (directoryIsEmpty(arguments[0]))
                    {
                        Directory.Delete(arguments[0]);
                    }
                    else
                    {
                        Console.Write("Error: Directory is not empty.");
                    }
                }
                else if (Directory.Exists(Directory.GetCurrentDirectory() + arguments[0]))
                {
                    if (directoryIsEmpty(Directory.GetCurrentDirectory() + arguments[0]))
                    {
                        Directory.Delete(Directory.GetCurrentDirectory() + arguments[0]);
                    }
                    else
                    {
                        Console.Write("Error: Directory is not empty.");
                    }
                }
                else
                {
                    Console.Write("Error: This directory does not exist.");
                }
            }
            else
            {
                Console.Write("Error: No directory was supplied.");
            }
        }

        public static void createDirectory(string[] arguments)
        {
            if (arguments.Length >= 0)
            {
                Directory.CreateDirectory(arguments[0]);
            }
            else
            {
                Console.WriteLine("Error: No directory was supplied.");
            }
        }

        public static bool directoryIsEmpty(string path)
        {
            if (Directory.GetFiles(path).Length == 0 && Directory.GetDirectories(path).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Cd(string[] arguments)
        {
            if (arguments.Length > 0)
            {
                ChangeDirectory(arguments[0]);
            }
            else
            {
                ChangeDirectory(null);
            }
        }

        protected static void ChangeDirectory(string path)
        {
            if (path == null)
            {
                Console.Write(Directory.GetCurrentDirectory());
            }
            else if (path == "..")
            {
                string[] temp = Directory.GetCurrentDirectory().Split("\\");
                string reconst = "";
                if (temp.Length > 2)
                {
                    for (int i = 0; i < temp.Length - 2; i++)
                    {
                        reconst += temp[i] + "\\";
                    }
                    if (reconst.Substring(reconst.Length - 1) != "\\")
                    {
                        reconst += "\\";
                    }
                    //Console.Write("Changing to " + reconst + "\n");
                    Directory.SetCurrentDirectory(reconst);
                }
            }
            else if (int.TryParse(path.Split(":")[0], out int temp))
            {
                if (Directory.Exists(path))
                {
                    if (path.Substring(path.Length - 1) != "\\")
                    {
                        path += "\\";
                    }
                    Directory.SetCurrentDirectory(path);
                }
                else
                {
                    Console.Write("Invalid directory.\n");
                }
            }
            else
            {
                if (Directory.Exists(Directory.GetCurrentDirectory() + path))
                {
                    path = Directory.GetCurrentDirectory() + path;
                    if (path.Substring(path.Length - 1) != "\\")
                    {
                        path += "\\";
                    }
                    Directory.SetCurrentDirectory(path);
                }
                else
                {
                    Console.Write("Invalid directory.\n");
                }
            }
        }
    }
}
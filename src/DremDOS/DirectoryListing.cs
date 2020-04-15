using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;

namespace DremDOS
{
    public class DirectoryListing
    {
        public static void Dir()
        {
            Console.Write("Directory listing of " + Directory.GetCurrentDirectory() + "\n\n");

            if (DirectoryOperations.directoryIsEmpty(Directory.GetCurrentDirectory()))
            {
                Console.WriteLine("This directory is empty.");
                return;
            }
            string[] directories = GetDirectories(Directory.GetCurrentDirectory());
            string[] files = GetFiles(Directory.GetCurrentDirectory());

            if (directories[0] != "$")
            {
                for (int i = 0; i < directories.Length; i++)
                {
                    Console.Write("[" + directories[i] + "]\n");
                }
             }
             if (files[0] != "$")
             {
                for (int i = 0; i < files.Length; i++)
                {
                    Console.Write(files[i] + "\n");
                }
            }
         }

        protected static string[] GetFiles(string path)
        {
            string[] Files;
            string[] temp = new string[Directory.GetFiles(path).Length];
            if (temp.Length > 0)
                Files = Directory.GetFiles(path);
            else
                Files = new string[1] { "$" };

            return Files;
        }

        protected static string[] GetDirectories(string path)
        {
            string[] Directories;
            string[] temp = new string[Directory.GetDirectories(path).Length];
            if (temp.Length > 0)
                Directories = Directory.GetDirectories(path);
            else
                Directories = new string[1] { "$" };

            return Directories;
        }
    }
}
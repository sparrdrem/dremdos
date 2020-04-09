using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;

namespace DremDOS
{
    public class Kernel : Sys.Kernel
    {
        string _osVersion = "0.0.1-RC1";
        protected override void BeforeRun()
        {
            // Initialize filesystems
            var fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            // Clear the screen
            Console.Clear();
            // Nice little startup logo thing
            // The sound is totally not a reference to the Commodore PET
            Console.Beep(4000, 100);
            Console.Beep(8000, 100);
            Console.WriteLine("    ___                ___  ____  ____  ");
            Console.Beep(4000, 100);
            Console.Beep(8000, 100);
            Console.WriteLine("   / _ \\_______ __ _  / _ \\/ __ \\/ __/ ");
            Console.Beep(4000, 100);
            Console.Beep(8000, 100);
            Console.WriteLine("  / // / __/ -_)  ' \\/ // / /_/ /\\ \\  ");
            Console.Beep(4000, 100);
            Console.Beep(8000, 100);
            Console.WriteLine(" /____/_/  \\__/_/_/_/____/\\____/___/ ");
            // Display OS information
            Command();
        }

        protected override void Run()
        {
            // Command prompt
            Console.Write("\n" + Directory.GetCurrentDirectory() + "> ");
            var input = Console.ReadLine();

            // Split the input into an array
            string[] preparse = parseCommand(input);

            // Take the command out of the array and put it into the final arguments array. Convert the List into an array. We now have our arguments!
            string command = preparse[0];
            string[] arguments = new string[preparse.Length - 1];
            for (int i = 0; i < arguments.Length; i++)
            {
                arguments[i] = preparse[i + 1];
            }

            // If command == <command> do command
            if (command == "beep")
            {
                Beep(arguments);
            }
            else if (command == "command")
            {
                Command();
            }
            else if (command == "cls")
            {
                Console.Clear();
            }
            else if (command == "help")
            {
                Help(arguments);
            }
            else if (command == "dir")
            {
                Dir();
            }
            else if (command == "drivelist")
            {
                Drivelist();
            }
            else if (command == "cd")
            {
                Cd(arguments);
            }
            else if (command == "checktime")
            {
                CheckTime();
            }
            else if (command == "calculate")
            {
                Calculate(arguments);
            }
            else if (command == "shutdown")
            {
                Console.Write("Goodbye!\n");
                Console.Beep(1975, 200);
                Console.Beep(1318, 200);
                Console.Beep(880, 200);
                Console.Beep(987, 300);
                Shutdown(arguments);
            }
            else if (command == "kitty")
            {
                Kitty(arguments);
            }
            else if (command == "rm")
            {
                removeFile(arguments);
            }
            else if (command == "rmdir")
            {
                removeDirectory(arguments);
            }
            else if (command == "mkdir")
            {
                createDirectory(arguments);
            }
            else // Else, say it's a bad command.
            {
                Console.Write("Bad command.\n");
            }
        }

        // Props to Çağrı
        // https://stackoverflow.com/a/61112392/11500310
        public string[] parseCommand(string word)
        {
            List<string> result = new List<string>();
            var split1 = word.Split('"');
            for (int i = 0; i < split1.Length; i++)
            {
                split1[i] = split1[i].Trim();
            }
            for (int i = 0; i < split1.Length; i++)
            {
                if (i % 2 == 0)
                {
                    var split2 = split1[i].Split(' ');
                    foreach (var el in split2)
                    {
                        result.Add(el);
                    }
                }
                else
                {
                    result.Add(split1[i]);
                }

            }
            string[] arr = new string[result.Count];
            for (int i = 0; i < result.Count; i++)
            {
                arr[i] = result[i];
            }
            return arr;

        }

        protected void Command()
        {
            Console.Write("DremDOS " + _osVersion + "\n");
            Console.Write("Copyright (C) Innovation Inc. 2020\n");
        }

        protected void Beep(string[] arguments)
        {
            // Initialize variables
            int frequency = 0;
            int time = 0;
            // Test if there are enough arguments
            if (arguments.Length >= 2)
            {
                if (int.TryParse(arguments[0], out frequency) && int.TryParse(arguments[1], out time))
                {
                    // Checks if the frequency is within the frequencies allowed by C#
                    if (frequency >= 37 && frequency <= 32767)
                    {
                        Console.Beep(frequency, time);
                    }
                    else // If one of the arguments can't be casted to an int, display the following error.
                    {
                        Console.Beep(1000, 100);
                        Console.Write("Error: Frequency must be between 37 and 32767Hz\n");
                    }
                }
            }
            else // If there aren't enough arguments, do the default beep.
            {
                Console.Beep(1000, 1000);
            }
        }

        protected void Dir()
        {
            Console.Write("Directory listing of " + Directory.GetCurrentDirectory() + "\n\n");
            string[] directories = GetDirectories(Directory.GetCurrentDirectory());
            string[] files = GetFiles(Directory.GetCurrentDirectory());

            for (int i = 0; i < directories.Length; i++)
            {
                Console.Write("[" + directories[i] + "]\n");
            }

            for (int i = 0; i < files.Length; i++)
            {
                Console.Write(files[i] + "\n");
            }
        }

        protected void Cd(string[] arguments)
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
        protected void Drivelist()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo d in drives)
            {
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("  Drive type: {0}", d.DriveType);
                if (d.IsReady == true)
                {
                    Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                    Console.WriteLine("  File system: {0}", d.DriveFormat);
                    Console.WriteLine(
                        "  Available space to current user:{0, 15} bytes",
                        d.AvailableFreeSpace);

                    Console.WriteLine(
                        "  Total available space:          {0, 15} bytes",
                        d.TotalFreeSpace);

                    Console.WriteLine(
                        "  Total size of drive:            {0, 15} bytes ",
                        d.TotalSize);
                }
            }
        }

        public void Kitty(string[] arguments)
        {
            if (arguments.Length == 0)
            {
                Console.WriteLine("Kitty 0.0.1");
                Console.WriteLine("\"We find the defendant kitty\"\n");
                Help(new string[1] { "kitty" });
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
                        File.Create(Directory.GetCurrentDirectory() + arguments[0]);
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
                Help(new string[1] { "kitty" });
            }
        }

        public string[] GetFiles(string path)
        {
            string[] Files = new string[Directory.GetFiles(path).Length];
            if (Files.Length > 0)
                Files = Directory.GetFiles(path);
            else
                Files[0] = "No files found.";

            return Files;
        }

        public string[] GetDirectories(string path)
        {
            string[] Directories = new string[Directory.GetDirectories(path).Length];
            if (Directories.Length > 0)
                Directories = Directory.GetDirectories(path);
            else
                Directories[0] = "No directories found.";

            return Directories;
        }

        public void ChangeDirectory(string path)
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
                    Console.Write("Changing to " + reconst + "\n");
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

        protected void removeFile(string[] arguments)
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

        protected void removeDirectory(string[] arguments)
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

        protected void createDirectory(string[] arguments)
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

        public bool directoryIsEmpty(string path)
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

        protected void CheckTime()
        {
            string time = DateTime.Now.ToString();
            Console.Write("{0}", time);
        }

        protected void Calculate(string[] arguments)
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

        protected void Shutdown(string[] arguments)
        {
            if (arguments.Length > 0)
            {
                if (arguments[0] == "/r")
                {
                    Cosmos.System.Power.Reboot();
                }
                else
                {
                    Console.Write("Error: " + arguments[0] + " is not valid.");
                }
            }
            Cosmos.System.Power.Shutdown();
        }

        protected void Help(string[] arguments)
        {
            // Tests if the arguments has the correct length
            if (arguments.Length >= 1)
            {
                if (arguments[0] == "help")
                {
                    Console.Write("help - help menu or get help on a command\n");
                    Console.Write("help <command> - get help on a specific command");
                }
                else if (arguments[0] == "command")
                {
                    Console.Write("command - outputs information about DremDOS");
                }
                else if (arguments[0] == "beep")
                {
                    Console.Write("beep - make the PC speaker go beep!\n");
                    Console.Write("beep <frequency> <time> - make a more specific beep");
                }
                else if (arguments[0] == "cls")
                {
                    Console.Write("cls - clear the screen");
                }
                else if (arguments[0] == "dir")
                {
                    Console.Write("dir - display all files and folders in the current directory");
                }
                else if (arguments[0] == "cd")
                {
                    Console.Write("cd - change the directory\n");
                    Console.Write("cd <directory> - change to <directory>\n");
                    Console.Write("  You can either specify a specific directory or relative directory.\n");
                    Console.Write("cd .. - go up one directory");
                }
                else if (arguments[0] == "drivelist")
                {
                    Console.Write("drivelist - list all currently attached drives, their filesystems, and other information");
                }
                else if (arguments[0] == "checktime")
                {
                    Console.Write("checktime - check the time");
                }
                else if (arguments[0] == "calculate")
                {
                    Console.Write("calculate - a basic calculator\n");
                    Console.Write("calculate <int> <operator> <int>");
                }
                else if (arguments[0] == "shutdown")
                {
                    Console.Write("shutdown - shut down the computer\n");
                    Console.Write("shutdown /r - restart the computer");
                }
                else if (arguments[0] == "kitty")
                {
                    Console.Write("kitty - read and write files\n");
                    Console.Write("kitty <file> [/r] [/w] [/nf] <text>\n");
                    Console.Write("Example: kitty foo.txt /r\n");
                    Console.Write("  Read foo.txt and output it to the screen\n");
                    Console.Write("Example: kitty foo.txt /w \"bar\"\n");
                    Console.Write("  Write \"bar\" to foo.txt (without quotes)");
                    Console.Write("Example: kitty foo.txt /nf\n");
                    Console.Write("  Create a new file named \"foo.txt\" in the current directory");
                }
                else if (arguments[0] == "rm")
                {
                    Console.WriteLine("rm - remove a file from the drive");
                    Console.WriteLine("rm <file>");
                }
                else if (arguments[0] == "rmdir")
                {
                    Console.WriteLine("rmdir - remove an empty directory");
                    Console.WriteLine("rmdir <directory>");
                }
                else if (arguments[0] == "mkdir")
                {
                    Console.WriteLine("mkdir - create an empty directory");
                    Console.WriteLine("mkdir <directory>");
                }
                else // Display the default help menu if the command requested in the first argument isn't valid
                {
                    Console.Write("DremDOS Help Menu\n\n");
                    Console.Write("command - ouputs information about DremDOS\n");
                    Console.Write("beep - make the PC speaker go beep!\n");
                    Console.Write("cls - clear the screen\n");
                    Console.Write("dir - display all files and folders in the current directory\n");
                    Console.Write("cd - change the directory\n");
                    Console.Write("drivelist - list all currently attached drives and their filesystems\n");
                    Console.Write("checktime - check the time\n");
                    Console.Write("calculate - a basic calculator\n");
                    Console.Write("shutdown - shut down the computer\n");
                    Console.Write("kitty - read and write files\n");
                    Console.Write("rm - remove a file from the drive\n");
                    Console.Write("rmdir - remove an empty directory\n");
                    Console.Write("mkdir - create an empty directory\n");
                    Console.Write("help - help menu or get help on a command\n\n");
                    Console.Write("Tip: Try help <command>");
                }
            }
            else // Default help menu
            {
                // Ack I'm tired I'll figure this out later
                Console.Write("DremDOS Help Menu\n\n");
                Console.Write("command - ouputs information about DremDOS\n");
                Console.Write("beep - make the PC speaker go beep!\n");
                Console.Write("cls - clear the screen\n");
                Console.Write("dir - display all files and folders in the current directory\n");
                Console.Write("cd - change the directory\n");
                Console.Write("drivelist - list all currently attached drives and their filesystems\n");
                Console.Write("checktime - check the time\n");
                Console.Write("calculate - a basic calculator\n");
                Console.Write("shutdown - shut down the computer\n");
                Console.Write("kitty - read and write files\n");
                Console.Write("rm - remove a file from the drive\n");
                Console.Write("rmdir - remove an empty directory\n");
                Console.Write("mkdir - create an empty directory\n");
                Console.Write("help - help menu or get help on a command\n\n");
                Console.Write("Tip: Try help <command>");
            }
        }
    }
}

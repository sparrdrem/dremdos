using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;

namespace DremDOS
{
    public class Kernel : Sys.Kernel
    {
        string _osVersion = "0.0.1";
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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("    ___                ___  ____  ____  ");
            Console.Beep(4000, 100);
            Console.Beep(8000, 100);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("   / _ \\_______ __ _  / _ \\/ __ \\/ __/ ");
            Console.Beep(4000, 100);
            Console.Beep(8000, 100);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  / // / __/ -_)  ' \\/ // / /_/ /\\ \\  ");
            Console.Beep(4000, 100);
            Console.Beep(8000, 100);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" /____/_/  \\__/_/_/_/____/\\____/___/ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nNOTICE: THIS SOFTWARE IS PROVIDED AS-IS AND INCLUDES ABSOLUTELY NO WARRENTY. ANY DAMAGE TO YOUR DEVICE AND/OR DATA IS ON YOU!");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("YOU HAVE BEEN WARNED!");
            Console.ForegroundColor = ConsoleColor.White;
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
                DirectoryListing.Dir();
            }
            else if (command == "drivelist")
            {
                Drives.Drivelist();
            }
            else if (command == "cd")
            {
                DirectoryOperations.Cd(arguments);
            }
            else if (command == "checktime")
            {
                CheckTime();
            }
            else if (command == "calculate")
            {
                Calculator.Calculate(arguments);
            }
            else if (command == "shutdown")
            {
                Console.Write("Goodbye!\n");
                Console.Beep(1975, 200);
                Console.Beep(1318, 200);
                Console.Beep(880, 200);
                Console.Beep(987, 300);
                PowerOperations.Shutdown(arguments);
            }
            else if (command == "kitty")
            {
                Kitty.kitty(arguments);
            }
            else if (command == "rm")
            {
                FileOperations.removeFile(arguments);
            }
            else if (command == "rmdir")
            {
                DirectoryOperations.removeDirectory(arguments);
            }
            else if (command == "mkdir")
            {
                DirectoryOperations.createDirectory(arguments);
            }
            else if (command == "copy")
            {
                FileOperations.copyFile(arguments);
            }
            else if (command == "move")
            {
                FileOperations.moveFile(arguments);
            }
            else if (command == "initgui")
            {
                DesktopEnvironment.InitGUI(arguments);
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

        protected void CheckTime()
        {
            string time = DateTime.Now.ToString();
            Console.Write("{0}", time);
        }

        public static void Help(string[] arguments)
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
                else if (arguments[0] == "copy")
                {
                    Console.WriteLine("copy - copy a file");
                    Console.WriteLine("copy <source> <destination>");
                    Console.WriteLine("Note: Copy is currently very limited.");
                }
                else if (arguments[0] == "move")
                {
                    Console.WriteLine("move - move a file");
                    Console.WriteLine("move <source> <destination>");
                    Console.WriteLine("Note: currently doesn't work");
                }
                else if (arguments[0] == "initgui")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Usage: initgui [resolution]");
                    Console.WriteLine("");
                    Console.WriteLine("Available Resolutions:");
                    Console.WriteLine("       640x480     800x600    1024x768");
                    Console.WriteLine("      1280x720    1366x768    1920x1080");
                    Console.WriteLine("");
                    Console.WriteLine("Example: initgui 800x600");
                    Console.WriteLine("");
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
                    Console.Write("copy - copy a file\n");
                    Console.Write("move - move a file\n");
                    Console.Write("initgui - start the experimental desktop environment\n");
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
                Console.Write("copy - copy a file");
                Console.Write("move - move a file");
                Console.Write("initgui - start the experimental desktop environment\n");
                Console.Write("help - help menu or get help on a command\n\n");
                Console.Write("Tip: Try help <command>");
            }
        }
    }
}

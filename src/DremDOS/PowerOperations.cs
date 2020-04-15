using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;

namespace DremDOS
{
    public class PowerOperations
    {
        public static void Shutdown(string[] arguments)
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
    }
}
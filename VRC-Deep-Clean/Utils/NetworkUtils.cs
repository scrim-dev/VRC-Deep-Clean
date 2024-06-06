using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRC_Deep_Clean.Utils
{
    internal class NetworkUtils
    {
        public static void Reset()
        {
            DoCommand("ipconfig /release");
            DoCommand("ipconfig /renew");
            DoCommand("netsh winsock reset");
            DoCommand("netsh int ip reset");
        }

        private static void DoCommand(string command)
        {
            CustomLog.Warn("Doing a network reset...");
            CustomLog.Log($"Command: {command}");

            ProcessStartInfo PSI = new("cmd", "/c " + command)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process process = new();
            process.StartInfo = PSI;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                CustomLog.Error($"{command} failed!\nError: {error}");
            }

            CustomLog.Log(output);
        }
    }
}

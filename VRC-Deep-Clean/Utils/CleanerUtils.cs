using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRC_Deep_Clean.Utils
{
    internal class CleanerUtils
    {
        public static void Delete(string path)
        {
            try
            {
                Directory.Delete(path, true); //Recursive
                Thread.Sleep(1000); //Wait a bit
                if (!Directory.Exists(path)) { CustomLog.Log("Directory deleted!"); }
            }
            catch (Exception e)
            {
                CustomLog.Error($"Failed to delete directory: '{path}'\nReason: {e}");
            }
        }

        public static void DeleteViaCMD(string path)
        {
            try
            {
                ProcessStartInfo pstart = new()
                {
                    FileName = "cmd.exe",
                    Verb = "runas",
                    Arguments = $"rmdir /s /q {path}" //Force delete (I think? lol)
                };

                Process.Start(pstart);

                if (!Directory.Exists(path)) { CustomLog.Log("Directory deleted!"); }
            }
            catch (Exception e)
            {
                CustomLog.Error($"Failed to delete directory: '{path}'\nReason: {e}");
            }
        }

        //Software\VRChat
        public static void RegDelete(string key)
        {
            try
            {
                Registry.CurrentUser.DeleteSubKeyTree(key);
                if (Registry.CurrentUser.OpenSubKey(key) != null)
                {
                    CustomLog.Log("Registry key deleted / removed!");
                }
            }
            catch (Exception ex)
            {
                CustomLog.Error($"{ex.Message}");
            }
        }
    }
}

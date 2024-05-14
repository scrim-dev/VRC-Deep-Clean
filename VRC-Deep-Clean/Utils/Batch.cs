using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRC_Deep_Clean.Utils
{
    internal class Batch
    {
        public static readonly string BatchFolder = Directory.GetCurrentDirectory() + "\\Bat";
        public static readonly string StartVRCBatNoVR = BatchFolder + "\\StartVRC_NoVR.bat";
        public static readonly string StartVRCBatVR = BatchFolder + "\\StartVRC_VR.bat";
        public static readonly string InstallVRC = BatchFolder + "\\Install.bat";
        public static readonly string UninstallVRC = BatchFolder + "\\Uninstall.bat";

        public static void Create()
        {
            CustomLog.SetWorkingTitle("Making VRChat batch files...");

            if (!Directory.Exists(BatchFolder))
            {
                try
                {
                    Directory.CreateDirectory(BatchFolder);
                }
                catch { }
            }

            //novr
            try
            {
                File.WriteAllText(StartVRCBatNoVR, "start steam://rungameid/438100 -novr");

                if(File.Exists(StartVRCBatNoVR))
                {
                    CustomLog.Log("Batch file 1/4 created");
                }
            }
            catch
            {
                CustomLog.Error("Failed to write batch file 1/4");
            }

            //vrmode
            try
            {
                File.WriteAllText(StartVRCBatVR, "start steam://rungameid/438100 -vrmode");

                if (File.Exists(StartVRCBatVR))
                {
                    CustomLog.Log("Batch file 2/4 created");
                }
            }
            catch
            {
                CustomLog.Error("Failed to write batch file 2/4");
            }

            //Install
            try
            {
                File.WriteAllText(InstallVRC, "start steam://install/438100");

                if (File.Exists(InstallVRC))
                {
                    CustomLog.Log("Batch file 3/4 created");
                }
            }
            catch
            {
                CustomLog.Error("Failed to write batch file 3/4");
            }

            //Uninstall
            try
            {
                File.WriteAllText(UninstallVRC, "start steam://uninstall/438100");

                if (File.Exists(UninstallVRC))
                {
                    CustomLog.Log("Batch file 4/4 created");
                }
            }
            catch
            {
                CustomLog.Error("Failed to write batch file 4/4");
            }

            CustomLog.SetWorkingTitle(string.Empty);
        }
    }
}

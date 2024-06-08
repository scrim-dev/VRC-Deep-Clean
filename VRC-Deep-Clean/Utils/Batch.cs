using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRC_Deep_Clean.Utils
{
    internal class Batch
    {
        private static int VRCid { get; } = 438100; //App ID
        public static readonly string BatchFolder = Directory.GetCurrentDirectory() + "\\Bat";
        public static readonly string StartVRCBatNoVR = BatchFolder + "\\StartVRC_NoVR.bat";
        public static readonly string StartVRCBatVR = BatchFolder + "\\StartVRC_VR.bat";
        public static readonly string InstallVRC = BatchFolder + "\\Install.bat";
        public static readonly string UninstallVRC = BatchFolder + "\\Uninstall.bat";
        public static readonly string OpenGH = BatchFolder + "\\GH.bat";

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
                File.WriteAllText(StartVRCBatNoVR, $"start steam://rungameid/{VRCid} -novr");

                if(File.Exists(StartVRCBatNoVR))
                {
                    CustomLog.Log("Batch file 1/5 created");
                }
            }
            catch
            {
                CustomLog.Error("Failed to write batch file 1/5");
            }

            //vrmode
            try
            {
                File.WriteAllText(StartVRCBatVR, $"start steam://rungameid/{VRCid} -vrmode");

                if (File.Exists(StartVRCBatVR))
                {
                    CustomLog.Log("Batch file 2/5 created");
                }
            }
            catch
            {
                CustomLog.Error("Failed to write batch file 2/5");
            }

            //Install
            try
            {
                File.WriteAllText(InstallVRC, $"start steam://install/{VRCid}");

                if (File.Exists(InstallVRC))
                {
                    CustomLog.Log("Batch file 3/5 created");
                }
            }
            catch
            {
                CustomLog.Error("Failed to write batch file 3/5");
            }

            //Uninstall
            try
            {
                File.WriteAllText(UninstallVRC, $"start steam://uninstall/{VRCid}");

                if (File.Exists(UninstallVRC))
                {
                    CustomLog.Log("Batch file 4/5 created");
                }
            }
            catch
            {
                CustomLog.Error("Failed to write batch file 4/5");
            }

            //Github redirect
            try
            {
                File.WriteAllText(OpenGH, $"start https://github.com/scrim-dev/VRC-Deep-Clean/releases");

                if (File.Exists(OpenGH))
                {
                    CustomLog.Log("Batch file 5/5 created");
                }
            }
            catch
            {
                CustomLog.Error("Failed to write batch file 5/5");
            }

            CustomLog.SetWorkingTitle(string.Empty);
        }
    }
}

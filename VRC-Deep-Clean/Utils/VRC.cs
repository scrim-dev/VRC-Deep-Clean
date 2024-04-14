using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRC_Deep_Clean.Utils
{
    internal class VRC
    {
        private static int vrc_id = 438100;
        public static void Install()
        {
            CustomLog.Warn("[INSTALL] Sending Steam App protocol / command...");

            CustomLog.SetWorkingTitle("Installing VRChat...");
            try
            {
                Process.Start($"steam://install/{vrc_id}");
            }
            catch (Exception ex)
            {
                CustomLog.Error($"Uhh that wasn't supposed to happen\nERROR: {ex}");
            }
        }

        public static void Uninstall()
        {
            CustomLog.Warn("[UNINSTALL] Sending Steam App protocol / command...");

            CustomLog.SetWorkingTitle("Uninstalling VRChat...");
            try
            {
                Process.Start($"steam://uninstall/{vrc_id}");
            }
            catch (Exception ex)
            {
                CustomLog.Error($"Uhh that wasn't supposed to happen\nERROR: {ex}");
            }
        }

        public static void StartVRC()
        {
            CustomLog.Warn("[LAUNCH] Sending Steam App protocol / command...");

            CustomLog.SetWorkingTitle("Launching VRChat...");
            try
            {
                Process.Start($"steam://launch/{vrc_id}");
            }
            catch (Exception ex)
            {
                CustomLog.Error($"Uhh that wasn't supposed to happen\nERROR: {ex}");
            }
        }

        public static void Clean()
        {
            CustomLog.Warn("Starting clean up process, this may take a while...");
            Thread.Sleep(1000);
            CustomLog.SetWorkingTitle("Cleaning in progress...");

            CustomLog.Warn("Getting base directories...");
            string LocalLowPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\..\\" + "LocalLow";
            string LocalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\..\\" + "Local";
            string RoamPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\..\\" + "Roaming";

            CustomLog.Warn("Getting VRChat & Unity specific / related folders...");
            Thread.Sleep(1000);
            CustomLog.Warn("This is going to HEAVILY clear everything that's cache related and temporary " +
                "(you MAY or MAY not need to sign into Unity / VRChat related apps after this) " +
                "If you don't want to continue you can just cancel by clicking the 'X' on the top right, if not the cleaner will begin in 15s.");
            Thread.Sleep(16000);
            CustomLog.Warn("Cleaning process started!");

            #region LocalLow
            if (Directory.Exists(LocalLowPath + "\\VRChat"))
            {
                CustomLog.Warn($"Found: VRChat locallow path, attempting to delete...");
                CleanerUtils.Delete(LocalLowPath + "\\VRChat");
            }
            else
            {
                CustomLog.Error("VRChat locallow folder doesn't exist (it's probably deleted already or missing)");
            }

            if (Directory.Exists(LocalLowPath + "\\Unity"))
            {
                CustomLog.Warn($"Found: Unity locallow path, attempting to delete...");
                CleanerUtils.Delete(LocalLowPath + "\\Unity");
            }
            else
            {
                CustomLog.Error("Unity locallow folder doesn't exist (it's probably deleted already or missing)");
            }

            if (Directory.Exists(LocalLowPath + "\\DefaultCompany"))
            {
                CustomLog.Warn($"Found: Unity's defualt folder locallow path, attempting to delete...");
                CleanerUtils.Delete(LocalLowPath + "\\DefaultCompany");
            }
            else
            {
                CustomLog.Error("Unity's defualt locallow folder doesn't exist (it's probably deleted already or missing)");
            }
            #endregion

            #region Local
            if (Directory.Exists(LocalPath + "\\VRChatCreatorCompanion"))
            {
                CustomLog.Warn($"Found: VRCCC (Creator Companion) local path, attempting to delete...");
                CleanerUtils.Delete(LocalPath + "\\VRChatCreatorCompanion");
            }
            else
            {
                CustomLog.Error("VRCCC (Creator Companion) local folder doesn't exist (it's probably deleted already or missing)");
            }

            if (Directory.Exists(LocalPath + "\\Unity"))
            {
                CustomLog.Warn($"Found: Unity local path, attempting to delete...");
                CleanerUtils.Delete(LocalPath + "\\Unity");
            }
            else
            {
                CustomLog.Error("Unity local folder doesn't exist (it's probably deleted already or missing)");
            }

            if (Directory.Exists(LocalPath + "\\Temp\\VRChat"))
            {
                CustomLog.Warn($"Found: Temp directory VRChat path, attempting to delete...");
                CleanerUtils.Delete(LocalPath + "\\Temp\\VRChat");
            }
            else
            {
                CustomLog.Error("VRC Temp directory folder doesn't exist (it's probably deleted already or missing)");
            }

            if (Directory.Exists(LocalPath + "\\GameAnalytics"))
            {
                CustomLog.Warn($"Found: GameAnalytics directory, attempting to delete...");
                CleanerUtils.Delete(LocalPath + "\\GameAnalytics");
            }
            else
            {
                CustomLog.Error("GameAnalytics local appdata directory folder doesn't exist (it's probably deleted already or missing)");
            }
            #endregion

            #region Roam
            if (Directory.Exists(RoamPath + "\\Unity"))
            {
                CustomLog.Warn($"Found: Unity Roaming, attempting to delete...");
                CleanerUtils.Delete(RoamPath + "\\Unity");
            }
            else
            {
                CustomLog.Error("Unity Roaming folder doesn't exist (it's probably deleted already or missing)");
            }

            if (Directory.Exists(RoamPath + "\\UnityHub"))
            {
                CustomLog.Warn($"Found: UnityHub Roaming, attempting to delete...");
                CleanerUtils.Delete(RoamPath + "\\UnityHub");
            }
            else
            {
                CustomLog.Error("UnityHub cache folder doesn't exist (it's probably deleted already or missing)");
            }

            /*
             * Idk if I should add this or not
             * 
            if (Directory.Exists(RoamPath + "\\VRCX"))
            {
                CustomLog.Warn($"Found: VRCX Roaming path, attempting to delete...");
                CleanerUtils.Delete(RoamPath + "\\VRCX");
            }
            else
            {
                CustomLog.Error("VRCX Cache folder doesn't exist (it's probably deleted already or missing)");
            }
            */
            #endregion

            CustomLog.Log("All folders checked and cleaned!");
            Thread.Sleep(1100);
            CustomLog.Warn("Beginning Regedit...");

            CleanerUtils.RegDelete(@"Software\VRChat");
            CustomLog.Log("Clean up completed!");
            Thread.Sleep(4500);
        }

        public static void NoAnalytics()
        {
            CustomLog.SetWorkingTitle("Blocking analytics...");

            string Hosts = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\drivers\\etc\\hosts";
            string[] Domains = new string[]
            {
                "api.amplitude.com",
                "api2.amplitude.com",
                "api.lab.amplitude.com",
                "api.eu.amplitude.com",
                "regionconfig.amplitude.com",
                "regionconfig.eu.amplitude.com",
                "o1125869.ingest.sentry.io",
                "analytics.amplitude.com",
                "analytics.eu.amplitude.com",
                "api3.amplitude.com",
                "cdn.amplitude.com",
                "info.amplitude.com",
                "static.amplitude.com",
                "ads-brand-postback.unityads.unity3d.com",
                "ads-game-configuration-master.ads.prd.ie.internal.unity3d.com",
                "ads-game-configuration.unityads.unity3d.com",
                "ads-privacy-api.prd.mz.internal.unity3d.com",
                "adserver.unityads.unity3d.com",
                "analytics.cloud.unity3d.com",
                "analytics.social.unity.com",
                "analytics.uca.cloud.unity3d.com",
                "analytics.unity3d.com",
                "api.uca.cloud.unity3d.com",
                "auction.unityads.unity3d.com",
                "cdp.cloud.unity3d.com",
                "config.uca.cloud.unity3d.com",
                "config.unityads.unity3d.com",
                "data-optout-service.uca.cloud.unity3d.com",
                "ecommerce.iap.unity3d.com",
                "events.iap.unity3d.com",
                "events.mz.unity3d.com",
                "geocdn.unityads.unity3d.com",
                "httpkafka.unityads.unity3d.com",
                "mediation-tracking.prd.mz.internal.unity3d.com",
                "perf-events.cloud.unity3d.com",
                "public.cloud.unity3d.com",
                "publisher-event.ads.prd.ie.internal.unity3d.com",
                "thind-gke-euw.prd.data.corp.unity3d.com",
                "tracking.prd.mz.internal.unity3d.com",
                "unityads.unity3d.com",
                "userreporting.cloud.unity3d.com",
                "webview.unityads.unity3d.com"
            };

            try
            {
                using (StreamWriter sw = File.AppendText(Hosts))
                {
                    foreach (string domain in Domains)
                    {
                        sw.WriteLine($"0.0.0.0 {domain}"); //Block em RAHHHHH
                        CustomLog.Warn($"Blocked: {domain}");
                    }
                }
                CustomLog.Log("Analytics blocked!");
            }
            catch (Exception ex)
            {
                CustomLog.Error($"Something failed!\n{ex.Message}");
            }
        }

        public static void KillPotentialProcesses()
        {
            CustomLog.SetWorkingTitle("Checking and killing processes...");

            //Very badly coded I know
            CustomLog.Warn("Checking for active processes and killing them");
            Thread.Sleep(1000);

            var p1 = Process.GetProcessesByName("VRChat");
            var p2 = Process.GetProcessesByName("EasyAntiCheat_EOS");
            var p3 = Process.GetProcessesByName("start_protected_game");

            if (p1.Length > 0)
            {
                foreach (var process in p1)
                {
                    try
                    {
                        process.Kill();
                        CustomLog.Log($"Process {process.ProcessName} killed successfully.");
                    }
                    catch (Exception ex)
                    {
                        CustomLog.Error($"Error killing process {process.ProcessName}: {ex.Message}");
                    }
                }
            }
            else { CustomLog.Warn("VRChat process is not active."); }

            if (p2.Length > 0)
            {
                foreach (var process in p2)
                {
                    try
                    {
                        process.Kill();
                        CustomLog.Log($"Process {process.ProcessName} killed successfully.");
                    }
                    catch (Exception ex)
                    {
                        CustomLog.Error($"Error killing process {process.ProcessName}: {ex.Message}");
                    }
                }
            }
            else { CustomLog.Warn("EOS (epic_online_services) process is not active."); }

            if (p3.Length > 0)
            {
                foreach (var process in p3)
                {
                    try
                    {
                        process.Kill();
                        CustomLog.Log($"Process {process.ProcessName} killed successfully.");
                    }
                    catch (Exception ex)
                    {
                        CustomLog.Error($"Error killing process {process.ProcessName}: {ex.Message}");
                    }
                }
            }
            else { CustomLog.Warn("SPG (start_protected_game) process is not active."); }
        }
    }
}

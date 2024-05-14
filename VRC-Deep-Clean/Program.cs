using System.Text;
using VRC_Deep_Clean.Utils;

namespace VRC_Deep_Clean
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = string.Empty;
            Console.OutputEncoding = Encoding.UTF8;

            Console.SetWindowSize(120, 35);

            CustomLog._Thread(true);

            Batch.Create();

            VRC.KillPotentialProcesses();
            Thread.Sleep(3000);

            MainMenu: //Default jump
            Console.Clear();
            CustomLog.Logo();
            CustomLog.SetWorkingTitle("Menu Selection.");
            CustomLog.Msg("Welcome! Please select an option:");

            CustomLog.Msg("↓↓↓\n\n[1] Delete and Clean up ALL of VRChat | [2] Delete, Uninstall, Clean ALL of VRChat and Reinstall | " +
                "[3] Simple Reinstall");

            try
            {
#pragma warning disable CS8604
                int selection = int.Parse(Console.ReadLine());
#pragma warning restore CS8604
                switch (selection)
                {
                    case 1:
                        Console.Clear();
                        CustomLog.Logo();
                        CustomLog.Warn("Please wait...");
                        VRC.Clean();
                        VRC.NoAnalytics();

                        YesNoSect:
                        CustomLog.SetWorkingTitle("Launch VRChat?");
                        CustomLog.Warn("Would you like to start VRChat? (Type yes or no)");
#pragma warning disable CS8600 
                        string input = Console.ReadLine();
#pragma warning restore CS8600 

#pragma warning disable CS8602 
                        if (input.ToLower() == "yes")
                        {
                            VRC.StartVRC();
                            CustomLog.Log("VRC protocol started!");
                            Thread.Sleep(3400);
                            Quit();
                        }
                        else if (input.ToLower() == "no")
                        {
                            Quit();
                        }
                        else
                        {
                            CustomLog.Error("Invalid input. Please enter 'yes' or 'no'.");
                            Thread.Sleep(2300);
                            goto YesNoSect;
                        }
#pragma warning restore CS8602 
                        break;
                    case 2:
                        Console.Clear();
                        CustomLog.Logo();
                        CustomLog.Warn("Please wait...");
                        VRC.Uninstall();
                        CustomLog.Warn("While you uninstall VRC DeepClean will prepare for install.");
                        Thread.Sleep(13000);
                        VRC.Clean();
                        VRC.NoAnalytics();
                        Thread.Sleep(3000);
                        VRC.Install();
                        CustomLog.Log("Finished all steps!");
                        Thread.Sleep(3000);
                        Quit();
                        break;
                    case 3:
                        Console.Clear();
                        CustomLog.Logo();
                        VRC.Reinstall();
                        break;
                    default:
                        Console.Clear();
                        CustomLog.Logo();
                        CustomLog.Error("Selection number is bigger or lesser than the given options.");
                        CustomLog.Msg("Returning in 4s...");
                        Thread.Sleep(4000);
                        Console.Clear();
                        goto MainMenu;
                }
            }
            catch
            {
                Console.Clear();
                CustomLog.Logo();
                CustomLog.Error("Incorrect format used or non-existent selection chosen");
                CustomLog.Msg("Returning in 4s...");
                Thread.Sleep(4000);
                Console.Clear();
                goto MainMenu;
            }

            Quit();
        }

        private static void Quit()
        {
            CustomLog.SaveLogs();
            CustomLog.SetWorkingTitle("Shutting down app...");
            Console.Clear();
            CustomLog.Warn("Goodbye! :D");
            CustomLog.Warn("Closing app in 3s");
            Thread.Sleep(3000);
            Environment.Exit(0);
        }
    }
}

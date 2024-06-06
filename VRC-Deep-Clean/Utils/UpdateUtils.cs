using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRC_Deep_Clean.Utils
{
    internal class UpdateUtils
    {
        public static void Check()
        {
            //Manual cuz I'm lowkey lazy
            Console.Clear();
            CustomLog.Logo();
            CustomLog.SetWorkingTitle("Redirecting to github...");
            CustomLog.Log("Opening github...");
            try { Process.Start(Batch.OpenGH); } 
            catch { CustomLog.Error("Failed to redirect to github please visit manually at my website or: https://github.com/scrim-dev/VRC-Deep-Clean/releases"); }
            Thread.Sleep(4000);
        }
    }
}

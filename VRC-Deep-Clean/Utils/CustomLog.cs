using System.Drawing;
using Cons = Colorful.Console;

namespace VRC_Deep_Clean.Utils
{
    internal class CustomLog
    {
        //255, 41, 126 - secondary color
        //187, 28, 255 - main color
        public static string ConsoleTitle { get; set; } = "VRC Deep Clean";
        private static string ConsoleWorkTitleThing { get; set; }
        public static void SetWorkingTitle(string title) => ConsoleWorkTitleThing = title;

        private static CancellationTokenSource Cancellation = new();
        private static List<char> AppName = new()
        {
            'D','e','e','p','C','l','e','a','n'
        };

        public static void _Thread(bool StartOrEnd)
        {
            CancellationToken token = Cancellation.Token;
            Thread consthrd = new(() => TitleThread(token));

            if (StartOrEnd)
            {
                consthrd.Start();
            }
            else
            {
                Cancellation.Cancel();
                consthrd.Join();
            }
        }

        private static void TitleThread(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    Cons.Title = ConsoleTitle + " | " + ConsoleWorkTitleThing;
                    Thread.Sleep(500);
                }
            }
            catch (OperationCanceledException)
            {
                Cons.WriteLine("Thread cancelled");
            }
        }

        public static void Logo()
        {
            Cons.WriteAscii("VRC DEEP CLEAN", Color.FromArgb(187, 28, 255));
        }

        public static void Msg(string text)
        {
            Cons.Write("[", Color.Gray);
            Cons.Write(DateTime.Now.ToString("hh:mm:ss"), Color.Magenta);
            Cons.Write("] [", Color.Gray);
            Cons.WriteWithGradient(AppName, Color.BlueViolet, Color.Fuchsia, 14);
            Cons.Write("]", Color.Gray);
            Cons.Write($" {text}\n", Color.White);
        }

        public static void Log(string text)
        {
            Cons.Write("[", Color.Gray);
            Cons.Write(DateTime.Now.ToString("hh:mm:ss"), Color.Magenta);
            Cons.Write("] [", Color.Gray);
            Cons.WriteWithGradient(AppName, Color.BlueViolet, Color.Fuchsia, 14);
            Cons.Write("]", Color.Gray);

            Cons.Write(" [", Color.Gray);
            Cons.Write("LOG", Color.Cyan);
            Cons.Write("]", Color.Gray);
            Cons.Write($" {text}\n", Color.White);
        }

        public static void Warn(string text)
        {
            Cons.Write("[", Color.Gray);
            Cons.Write(DateTime.Now.ToString("hh:mm:ss"), Color.Magenta);
            Cons.Write("] [", Color.Gray);
            Cons.WriteWithGradient(AppName, Color.BlueViolet, Color.Fuchsia, 14);
            Cons.Write("]", Color.Gray);

            Cons.Write(" [", Color.Gray);
            Cons.Write("WARN", Color.Yellow);
            Cons.Write("]", Color.Gray);
            Cons.Write($" {text}\n", Color.White);
        }

        public static void Error(string text)
        {
            Cons.Write("[", Color.Gray);
            Cons.Write(DateTime.Now.ToString("hh:mm:ss"), Color.Magenta);
            Cons.Write("] [", Color.Gray);
            Cons.WriteWithGradient(AppName, Color.BlueViolet, Color.Fuchsia, 14);
            Cons.Write("]", Color.Gray);

            Cons.Write(" [", Color.Gray);
            Cons.Write("ERROR", Color.Red);
            Cons.Write("]", Color.Gray);
            Cons.Write($" {text}\n", Color.White);
        }
    }
}

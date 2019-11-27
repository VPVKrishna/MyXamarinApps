using System;
using System.IO;
using Xamarin.Forms;

namespace MyUnityApp.UnityApp
{
    public interface ILogger
    {
        void LogMessage(string message);
    }

    public class ConsoleLogger : ILogger
    {
        public void LogMessage(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class FileLogger : ILogger
    {

        public void LogMessage(string message)
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "myUnityLog.txt");
            bool doesExist = File.Exists(fileName);
            //File.WriteAllText(fileName, message);
            File.AppendAllText(fileName, message+"\n");

            string text = File.ReadAllText(fileName);
        }
    }

    public class ExternalFileLogger : ILogger
    {
        public void LogMessage(string message)
        {
            DependencyService.Get<ILogger>().LogMessage(message);
        }
    }
}

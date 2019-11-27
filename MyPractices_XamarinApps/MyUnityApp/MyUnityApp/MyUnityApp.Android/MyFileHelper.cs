using System.Text;
using Android.Content;
using Android.OS;
using Java.IO;
using Java.Lang;
using MyUnityApp.Droid;
using MyUnityApp.UnityApp;

[assembly: Xamarin.Forms.Dependency(typeof(MyFileHelper))]
namespace MyUnityApp.Droid
{
    public class MyFileHelper : ILogger
    {
        private string LogFilePath { set; get; }

        public MyFileHelper()
        {
            File dirPath = new File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDocuments), "UnityLog");
            if (!dirPath.Exists())
            {
                dirPath.Mkdirs();
            }
            var newfile = new File(dirPath, "MyFileLogger.txt");
            bool isExists = newfile.Exists();
            if (!isExists)
            {
                newfile.CreateNewFile();
            }
            LogFilePath = newfile.AbsolutePath;
        }

        public void LogMessage(string message)
        {
            try
            {               
                using (FileOutputStream outfile = new FileOutputStream(LogFilePath, true))
                {
                    outfile.Write(Encoding.ASCII.GetBytes(message+"\n"));
                    outfile.Close();
                }
            }catch(Exception e) {
                e.PrintStackTrace();
            }
        }
    }
}
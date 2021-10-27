using Android.Content;
using ProjectNamespace.Droid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WindesHeartApp.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(BackgroundDependency_Android))]
namespace ProjectNamespace.Droid
{
    public class BackgroundDependency_Android : Java.Lang.Object, IBackgroundDependency
    {
        string fileName = "";
        IEnumerable<Heartrate> heartrates;

        public void ExecuteCommand(string fileN, IEnumerable<Heartrate> heart)
        {
            fileName = fileN;
            heartrates = heart;
            Thread thread = new Thread(new ThreadStart(SaveAndView));
            thread.Start();
        }

        public async void SaveAndView()
        {
            try
            {
                Context context = Android.App.Application.Context;
                var filePath = context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);

                string root = null;
                //Get the root path in android device.
                if (Android.OS.Environment.IsExternalStorageEmulated)
                {
                    root = Android.OS.Environment.ExternalStorageDirectory.ToString();
                }
                else
                    root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

                //Create directory and file 
                Java.IO.File myDir = new Java.IO.File(root + "/Windesheart/");
                myDir.Mkdir();

                Java.IO.File file = new Java.IO.File(myDir, fileName);

                //Remove if the file exists
                if (file.Exists()) file.Delete();

                string fileN = file.ToString();
                //Write the stream into the file
                //Java.IO.FileOutputStream outs = new Java.IO.FileOutputStream(file);

                using (var streamWriter = new StreamWriter(filePath + fileName, false))
                {
                    streamWriter.WriteLine("HeartrateVal, Time");
                    foreach (Heartrate h in heartrates)
                    {
                        streamWriter.WriteLine(h.HeartrateValue + ", " + h.DateTime);
                    }
                }

                ////outs.Flush();
                //outs.Close();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }
    }
}
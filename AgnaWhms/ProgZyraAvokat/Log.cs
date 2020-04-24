using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Forms;
namespace ProgZyraAvokat
{
    public static class Log
    {
        private const int NumberOfRetries = 10;
        private const int DelayOnRetry = 1000;
        private static object _locker = new object();
        //public string clsFunction;
        //public string clsMessage;
        //public Log(string function, string message)
        //{
        //  clsFunction = function;
        //  clsMessage = message; 
        //}
        public static void LogData(string function, string message)
        {
            try
            {
                //string path = ("C:\\Inetpub\\wwwroot\\TestDllService\\Bin\\AppLog.txt");
                string path = "";
                lock (_locker)
                {
                    //string path = (System.IO.Directory.GetCurrentDirectory() + "\\AppLog.txt");
                    //if (Global.tabletName.IndexOf("dardhari") == -1 && Global.tabletName.IndexOf("dardhari") == -1)
                    //{
                    //    path = (@"C:\Soft\appAlpha\bin\Debug" + "\\AppLog.txt");
                    //}
                    //else
                    //{
                    path = (System.IO.Directory.GetCurrentDirectory() + "\\AppLog.txt");
                    if (!File.Exists(path))
                    {
                        File.Create(path);
                    }

                    //}

                    System.IO.StreamWriter log = new System.IO.StreamWriter(path, true);
                    //string path2 = Server.MapPath("~");
                    //log.WriteLine(System.DateTime.Now.ToString() + ",Funksioni " + function + ", " + message);


                    for (int i = 1; i <= NumberOfRetries; ++i)
                    {
                        try
                        {
                            // Do stuff with file
                            log.WriteLine(System.DateTime.Now.ToString() + ",Funksioni " + function + ", " + message);
                            break; // When done we can break loop
                        }
                        catch (System.IO.IOException e)
                        {
                            // You may check error code to filter some exceptions, not every error
                            // can be recovered.
                            if (i == NumberOfRetries) // Last one, (re)throw exception and exit
                                throw;
                            //Thread.Sleep(DelayOnRetry);
                            System.Threading.Thread.Sleep(DelayOnRetry);
                        }
                    }

                    log.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Log err " + ex.Message);
            }
        }
        public static void emptyText(string function, string message)
        {
            //string path = ("C:\\Inetpub\\wwwroot\\TestDllService\\Bin\\AppLog.txt");
            string path = "";
            lock (_locker)
            {
                //string path = (System.IO.Directory.GetCurrentDirectory() + "\\AppLog.txt");
                //if (Global.tabletName.IndexOf("dardhari") == -1)
                //{
                //    path = (@"C:\Soft\appAlpha\bin\Debug" + "\\AppLog.txt");
                //}
                //else
                //{
                    path = (System.IO.Directory.GetCurrentDirectory() + "\\AppLog.txt");
                //}

                System.IO.File.WriteAllText(path, String.Empty);

            }
        }

    }
}

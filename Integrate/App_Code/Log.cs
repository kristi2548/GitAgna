using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Reflection;

/// <summary>
/// Summary description for log
/// </summary>
public static class Log
{
    private const int NumberOfRetries = 10;
    private const int DelayOnRetry = 1000;
    private static object _locker = new object();
    public static void LogData(string function, string message)
    {
        //string path = ("C:\\Inetpub\\wwwroot\\TestDllService\\Bin\\AppLog.txt");
        lock (_locker)
        {
            string path = (HttpRuntime.AppDomainAppPath + "\\Bin\\AppLog.txt");
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

}
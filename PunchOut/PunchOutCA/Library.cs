using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace iBuyCL
{
    public static class Library
    {
        //Create a log method (WriteErrorLog) to log the exceptions.  
        public static void WriteErrorLog(Exception ex)
        {
            StreamWriter sw = null;
            try
            {
                string LogFile = "LogFile_" + DateTime.Now.ToString("ddMMyyyy hhmmss") + ".txt";
                sw = new StreamWriter(ConfigurationManager.AppSettings["LogLocation"].ToString() + "\\" + LogFile, true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        //Create one more log method (WriteErrorLog) to log the custom messages.
        public static void WriteErrorLog(string Message, string type, string issueEarnerId, int badgeType)
        {
            if (ConfigurationManager.AppSettings["LogStorage"].ToString() == "1")
            {
                try
                {
                    if (type == "Log")
                    {
                        StreamWriter sw = null;
                        string LogFile = "LogFile_" + DateTime.Now.ToString("MMddyyyy  hhmmss") + ".txt";
                        sw = new StreamWriter(ConfigurationManager.AppSettings["LogLocation"].ToString() + "\\" + LogFile, true);
                        sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                        sw.Flush();
                        sw.Close();
                    }
                    else if (type == "Request")
                    {
                        DirectoryInfo dir = new DirectoryInfo(ConfigurationManager.AppSettings["SourceLocation"].ToString() + "\\Request");
                        if (!dir.Exists)
                        {
                            dir.Create();
                        }
                        StreamWriter sw = null;
                        string LogFile = issueEarnerId + "_" + badgeType + "_" + DateTime.Now.ToString("MMddyyyy hhmmss") + ".txt";
                        sw = new StreamWriter(ConfigurationManager.AppSettings["SourceLocation"].ToString() + "\\Request\\" + LogFile, true);
                        sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                        sw.Flush();
                        sw.Close();
                    }
                    else if (type == "Response")
                    {
                        DirectoryInfo dir = new DirectoryInfo(ConfigurationManager.AppSettings["SourceLocation"].ToString() + "\\Response");
                        if (!dir.Exists)
                        {
                            dir.Create();
                        }
                        StreamWriter sw = null;
                        string LogFile = issueEarnerId + "_" + badgeType + "_" + DateTime.Now.ToString("MMddyyyy hhmmss") + ".txt";
                        sw = new StreamWriter(ConfigurationManager.AppSettings["SourceLocation"].ToString() + "\\Response\\" + LogFile, true);
                        sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                        sw.Flush();
                        sw.Close();
                    }
                }
                catch
                {

                }
            }
        }
    }
}

using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Configuration;
using System.Data.SqlClient;

namespace iBuyCL
{
    public class Program
    {
        static HttpClient iBuyCLClient;
        static string ServiceUrl = ConfigurationManager.AppSettings.Get("ServiceURL");
        // string locationSource = ConfigurationManager.AppSettings["SourceLocation"];
        public Program()
        {
            AcclaimAuthentication();
        }
        //ConfigurationManager.AppSettings["AcclaimServiceUrl"].ToString();  
        private static void AcclaimAuthentication()
        {
            iBuyCLClient = new HttpClient();
            iBuyCLClient.BaseAddress = new Uri(ServiceUrl);
            iBuyCLClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }
        static void Main(string[] args)
        {

        }
        public string ProcessXMLFiles()
        {
            try
            {
                Console.WriteLine("Provide file path for xml...");
                string url = string.Empty;
                string responseStr = string.Empty;
                AcclaimAuthentication();
                string contents = string.Empty;
                #region //Configure Path and Request Method = Post
                foreach (string file in Directory.EnumerateFiles(ConfigurationManager.AppSettings.Get("SourceFolderPath"), "*.xml"))
                {
                    contents = File.ReadAllText(file);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ServiceUrl);
                    byte[] bytes;
                    bytes = System.Text.Encoding.ASCII.GetBytes(contents);
                    request.ContentType = "text/xml; encoding='utf-8'";
                    request.ContentLength = bytes.Length;
                    request.Method = "POST";
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                    HttpWebResponse response;
                    response = (HttpWebResponse)request.GetResponse();

                    #region //response.StatusCode == HttpStatusCode.OK
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream responseStream = response.GetResponseStream();
                        responseStr = new StreamReader(responseStream).ReadToEnd();

                        //Response string from web request
                        var data = XElement.Parse(responseStr);
                        var urlData = (from value in data.Descendants("Response").Elements("PunchOutSetupResponse").Elements("StartPage")
                                       select new StartPage
                                       {
                                           URL = (string)value.Element("URL").Value,
                                       });
                         url = urlData.Select(x => x.URL).FirstOrDefault();
                        if (!string.IsNullOrEmpty(url))
                        {
                            File.Move(file, ConfigurationManager.AppSettings.Get("DestinationFolderPath") + "/" + Path.GetFileName(file));
                        }
                    }
                    #endregion
                }
                #endregion
                return url;
            }
            catch (Exception exception)
            {
                Console.WriteLine("There is exeption");
                return null;
            }
        }
        private static StringContent JSonSerializeObject(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.ASCII, "text/xml");
        }
        public void SaveDatabase(string URl)
        {
            //SqlConnection 
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=77.68.36.158;Database=PODApp;uid=PODApp;pwd=xxxxx";
            SqlCommand insertCommand = new SqlCommand("INSERT INTO TableName (FirstColumn) VALUES (@0)", conn);
            insertCommand.Parameters.Add(new SqlParameter("0", URl));
        }
    }
}

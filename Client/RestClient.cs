using CDDSS_API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Client
{
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    /// <summary>
    /// Class used to access WEB API
    /// 
    /// After every Request the parameters are reseted. So for every request you have to set the Parameters. 
    /// </summary>
    public class RestClient
    {
        private static string Prefix = @"http://localhost:51853/";
        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }
        private string accessToken;
        private Dictionary<string, MemoryStream> filesDict;
        private static RestClient instance;
        private UserShort currentUser;

        public static RestClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RestClient();
                    instance.EndPoint = "";
                    instance.Method = HttpVerb.GET;
                    instance.ContentType = "text/json";
                    instance.PostData = "";
                }
                
                return instance;
            }
        }

        public UserShort CurrentUser
        {
            get
            {
                return currentUser;
            }
        }

        public string MakeRequest()
        {
            return MakeRequest("");
        }

        public bool Login(string username, string password){

            var client = Instance;
            client.EndPoint = "Token";
            client.Method = HttpVerb.POST;
            client.PostData = "userName=" + username + "&password=" + password + "&confirmpassword=&grant_type=password";
            var json = client.MakeRequest();
            Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            if (dict!=null && dict.ContainsKey("access_token"))
            {
                accessToken = dict["access_token"];

                client.EndPoint = "api/User/Current";
                client.Method = HttpVerb.GET;
                json = client.MakeRequest();
                currentUser = JsonConvert.DeserializeObject<UserShort>(json);
                return true;
            }
            return false;
        }

        public bool LoginOut()
        {
            if (accessToken != null)
            {
                var client = Instance;
                client.EndPoint = "api/Account/Logout";
                client.Method = HttpVerb.POST;
                var json = client.MakeRequest();
                if (json.ToString().Equals("OK"))
                {
                    accessToken = null;
                    currentUser = null;
                    return true;
                }
            }
            return false;
        }

        public string MakeRequest(string parameters)
        {
            var request = (HttpWebRequest)WebRequest.Create(Prefix + EndPoint + parameters);

            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;
            if (accessToken != null)
            {
                request.Headers.Add("Authorization", "Bearer " + accessToken);
            }

            if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.POST)
            {
                var encoding = new UTF8Encoding();
                var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                request.ContentLength = bytes.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var responseValue = string.Empty;

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }
                    else
                    {
                        responseValue = "OK";
                    }

                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                                if (responseValue.Length == 0)
                                {
                                    responseValue = response.StatusCode.ToString();
                                }
                            }
                    }

                    PostData = "";
                    return responseValue;
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
            finally
            {
                instance.EndPoint = "";
                instance.Method = HttpVerb.GET;
                instance.ContentType = "text/json";
                instance.PostData = "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename">name of the file</param>
        /// <param name="memorystream">file loaded in memory stream</param>
        /// <returns>false if file already added to upload</returns>
        public bool AddFile(string filename, MemoryStream memorystream)
        {
            if (filesDict == null)
            {
                filesDict = new Dictionary<string, MemoryStream>();
            }
            if (filesDict.ContainsKey(filename))
            {
                return false;
            }
            else
            {
                filesDict.Add(filename, memorystream);
                return true;
            }
        }

        /// <summary>
        /// source from http://stackoverflow.com/questions/15543150/httprequest-files-is-empty-when-posting-file-through-httpclient
        /// </summary>
        /// <param name="issue"></param>
        /// <param name="filename"></param>
        public void UploadFilesToRemoteUrl(int issue)
        {
            

            string url = Prefix+ "api/Document?issueid=" + issue;
            long length = 0;
            string boundary = "----------------------------" +
            DateTime.Now.Ticks.ToString("x");

            HttpWebRequest httpWebRequest2 = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest2.ContentType = "multipart/form-data; boundary=" +
            boundary;
            httpWebRequest2.Method = "POST";
            httpWebRequest2.KeepAlive = true;
            httpWebRequest2.Headers.Add("Authorization", "Bearer " + accessToken);

            Stream memStream = new System.IO.MemoryStream();

            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" +
            boundary + "\r\n");

            string formdataTemplate = "\r\n--" + boundary +
            "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

            memStream.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";

            foreach (KeyValuePair<string,MemoryStream> file in filesDict)
            {

                //string header = string.Format(headerTemplate, "file" + i, files[i]);
                string header = string.Format(headerTemplate, "uplTheFile", file.Key);

                byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

                memStream.Write(headerbytes, 0, headerbytes.Length);

                
                
                byte[] buffer = new byte[1024];

                memStream.Write(file.Value.ToArray(), 0, file.Value.ToArray().Length);

                memStream.Write(boundarybytes, 0, boundarybytes.Length);
                
            }

            filesDict = null;

            httpWebRequest2.ContentLength = memStream.Length;

            Stream requestStream = httpWebRequest2.GetRequestStream();

            memStream.Position = 0;
            byte[] tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();
            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();

            WebResponse webResponse2 = httpWebRequest2.GetResponse();

            Stream stream2 = webResponse2.GetResponseStream();
            StreamReader reader2 = new StreamReader(stream2);

            webResponse2.Close();
            httpWebRequest2 = null;
            webResponse2 = null;
        }
        

    } // class

        
        //sample method how to upload files
        //public static void RestFileUpload()
        //{
        //    try
        //    {
        //        var client = RestClient.Instance;
        //        client.Login("sinisa.zubic@gmx.at", "passme");
        //        client.Endpoint = @"http://localhost:51853/api/Document";
        //        client.Method = HttpVerb.POST;
        //        //var json = client.MakeRequest("?issue=1&filename=test.png");

        //        using (MemoryStream ms = new MemoryStream())
        //        using (FileStream file = new FileStream(@"C:\test.pdf", FileMode.Open, FileAccess.Read))
        //        {
        //            byte[] bytes = new byte[file.Length];
        //            file.Read(bytes, 0, (int)file.Length);
        //            ms.Write(bytes, 0, (int)file.Length);
        //            client.AddFile("test.pdf", ms);
        //        }
        //        client.UploadFilesToRemoteUrl(1);
        //    }
        //    catch (System.Net.WebException ex)
        //    {
        //        System.Console.WriteLine(ex.Message);
        //    }
        //}
    
}
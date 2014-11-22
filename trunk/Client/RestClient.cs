﻿using System;
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
    public class RestClient
    {
        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }
        private static string accessToken;
        private Dictionary<string, MemoryStream> filesDict;

        public RestClient()
        {

            EndPoint = "";
            Method = HttpVerb.GET;
            ContentType = "text/xml";
            PostData = "";
        }
        public RestClient(string endpoint)
        {
            EndPoint = endpoint;
            Method = HttpVerb.GET;
            ContentType = "text/xml";
            PostData = "";
        }
        public RestClient(string endpoint, HttpVerb method)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/xml";
            PostData = "";
        }

        public RestClient(string endpoint, HttpVerb method, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/xml";
            PostData = postData;
        }


        public string MakeRequest()
        {
            return MakeRequest("");
        }

        public static Boolean Login(string username, string password)
        {

            var client = new RestClient(@"http://localhost:51853/Token");
            client.Method = HttpVerb.POST;
            client.PostData = "userName=" + username + "&password=" + password + "&confirmpassword=&grant_type=password";
            var json = client.MakeRequest();
            Dictionary<string, string> dict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            if (dict.ContainsKey("access_token"))
            {
                accessToken = dict["access_token"];
                System.Console.WriteLine("Token: " + accessToken);
            }
            return false;
        }

        /// <summary>
        /// used for a standart rest request
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <returns>json</returns>
        public string MakeRequest(string parameters)
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

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

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                // grab the response
                //Shit
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }

                return responseValue;
            }
        }

        /// <summary>
        /// source from http://stackoverflow.com/questions/15543150/httprequest-files-is-empty-when-posting-file-through-httpclient
        /// </summary>
        /// <param name="issue"></param>
        /// <param name="filename"></param>
        public void UploadFilesToRemoteUrl(int issue)
        {


            string url = @"http://localhost:51853/api/Document?issueid=" + issue;
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

            foreach (KeyValuePair<string, MemoryStream> file in filesDict)
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

        
        //sample method how to upload files
        //public static void RestFileUpload()
        //{
        //    try
        //    {
        //        RestClient.Login("sinisa.zubic@gmx.at", "passme");
        //        var client = new RestClient(@"http://localhost:51853/api/Document");
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
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MalSearcher.Model.MalProxy
{
    public class WebGetter
    {
        public string Get(string url, string username, string password)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            request.Headers.Add("Authorization", "Basic " + encoded);
            return get(request);
        }

        public string Get(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            return get(request);
        }

        private string get(HttpWebRequest request)
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string data = "";

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                if (response.CharacterSet == null)
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                data = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
            }

            return data;
        }
    }
}

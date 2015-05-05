using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Diagnostics;

namespace ML
{
    /* 
     * Workload: 559 minutes
     *   Learnt: Better recursion skills, better C# knowledge, better understanding of basic principles in programming languages.
     */
    public class ML_Rest_Base
    {

        protected string url;
        protected string action;
        private int statusCode;
        private string method;
        private string response;
        private object responseObject;
        private Dictionary<string, object> requestBody;
        protected string apiKey;
        protected ulong id;
        private WebRequest connection;

        public ML_Rest_Base()
        {
            this.apiKey = "";
            this.action = "";
            this.Flush();
        }

        public void SetMethod(string method)
        {
            this.method = method;
            
        }

        public void SetType(string type)
        {
            this.url += type + "/";
        }

        public object Execute(Dictionary<string, object> data, string action)
        {
            Stream objStream;
            HttpWebResponse wResp;
            this.url += (this.action != "" ? this.action + "/" : "") + (this.id > 0 ? this.id.ToString() + "/" : "") + (action != "" ? action + "/" : "");
            Debug.Print(this.url);
            if (data != null)
            {
                foreach (KeyValuePair<string, object> entry in data)
                    this.AddToBody(entry.Key, entry.Value);
            }

            this.SetConnection();
            try
            {
                wResp = (HttpWebResponse)this.connection.GetResponse();
                this.statusCode = (int)wResp.StatusCode;
                objStream = wResp.GetResponseStream();
                this.response = "";

                using (var reader = new StreamReader(objStream))
                    this.response += reader.ReadToEnd();

            }
            catch (WebException we)
            {
                this.statusCode = (int)((HttpWebResponse)we.Response).StatusCode;
                this.response = "";

                using (var stream = we.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                    this.response += reader.ReadToEnd();
            }

            this.responseObject = (new JavaScriptSerializer()).DeserializeObject(this.response);
            this.Flush();

            return this.responseObject;
        }

        public string GetResponse()
        {
            return this.response;
        }

        public object GetResponseObject()
        {
            return this.responseObject;
        }

        public void AddToBody(string key, object value)
        {
            this.requestBody.Add(key, value);
        }

        public string BuildStream()
        {
            if (this.requestBody.Count() < 1)
                return "";

            string stream = this.BuildStream(this.requestBody, "", 0);
            stream = stream.Substring(0, stream.Length - 1);
            return stream;
        }

        private string BuildStream(object requestPart, string keyName, int level)
        {
            string buffer = "", key;
            int counter;
            level++;
            if (requestPart.GetType().IsGenericType && requestPart.GetType().GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                foreach (System.Collections.DictionaryEntry entry in (System.Collections.IDictionary) requestPart)
                {
                    key = (string)entry.Key;
                    buffer += this.BuildStream(entry.Value, keyName + (level > 1 ? "[" + key + "]" : key), level);
                }
            }
            else if (requestPart.GetType().IsGenericType && requestPart.GetType().GetGenericTypeDefinition() == typeof(List<>))
            {
                counter = 0;
                foreach (object entry in (System.Collections.IEnumerable)requestPart)
                {
                    buffer += this.BuildStream(entry, keyName + "[" + counter.ToString() + "]", level);
                    counter++;
                }
            }
            else
                buffer += keyName + "=" + System.Uri.EscapeUriString(requestPart.ToString()) + "&";

            return buffer;
        }

        private void SetConnection()
        {
            string stream;
            Stream dataStream;
            ASCIIEncoding encoding;
            byte[] data;

            stream = this.BuildStream();
            Debug.Print(stream);
            this.url += "?apiKey=" + this.apiKey;

            switch (this.method)
            {
                case "GET":
                case "DELETE":
                    this.url += stream != "" ? "&" + stream : "";
                    Debug.Print(this.url);
                    connection = WebRequest.Create(this.url);
                    connection.Method = this.method;
                    break;
                case "POST":
                case "PUT":
                    connection = WebRequest.Create(this.url);
                    connection.Method = this.method;
                    connection.ContentType = "application/x-www-form-urlencoded";
                    connection.ContentLength = stream.Length;
                    dataStream = connection.GetRequestStream();
                    encoding = new ASCIIEncoding();
                    data = encoding.GetBytes(stream);
                    dataStream.Write(data, 0, data.Length);
                    break;
            }
        }

        private void Flush()
        {
            this.id = 0;
            this.url = "https://api.mailersoft.com/api/v1/";
            this.method = "GET";
            this.requestBody = new Dictionary<string, object>();
        }
    }

}

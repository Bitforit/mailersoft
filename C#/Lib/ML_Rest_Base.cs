using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

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
            Console.WriteLine(this.url);
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
            Console.WriteLine(stream);
            this.url += "?apiKey=" + this.apiKey;

            switch (this.method)
            {
                case "GET":
                case "DELETE":
                    this.url += stream != "" ? "&" + stream : "";
                    Console.Write(this.url);
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

        static void Main(string[] args)
        {
            // Campaign: 30011384
            // List: 2009372 - for ML_Lists test; 2012541 - for subscribers
            ML_Messages mlMessages = new ML_Messages("w7k1h3q4l6c3o3e7l4v6a1l4t8q6c6d9");
            List<Dictionary<string, object>> recipients = new List<Dictionary<string, object>>();
            Dictionary<string, object> rec1, rec2, rec3;
            Dictionary<string, string> var1, var2, var3;
            rec1 = new Dictionary<string, object>();
            rec2 = new Dictionary<string, object>();
            rec3 = new Dictionary<string, object>();
            var1 = new Dictionary<string, string>();
            var2 = new Dictionary<string, string>();
            var3 = new Dictionary<string, string>();

            rec1.Add("recipientEmail", "first@example.com");
            rec1.Add("recipientName", "First Name");
            var1.Add("item1", "value11");
            var1.Add("item2", "value21");
            rec1.Add("variables", var1);

            rec2.Add("recipientEmail", "second@example.com");
            rec2.Add("recipientName", "Second Name");
            var2.Add("item1", "value12");
            var2.Add("item2", "value22");
            rec2.Add("variables", var1);

            rec3.Add("recipientEmail", "third@example.com");
            rec3.Add("recipientName", "Third Name");
            var3.Add("item1", "value13");
            var3.Add("item2", "value23");
            rec3.Add("variables", var1);

            recipients.Add(rec1);
            recipients.Add(rec2);
            recipients.Add(rec3);

            mlMessages.SetId(30011384).AddRecipients(recipients).Send();
            /*foreach (var entry in (Dictionary<string, object>)result)
            {
                Console.WriteLine(entry.Key + ": " + entry.Value);
            }*/
            Console.WriteLine(mlMessages.GetResponse());
            Console.Read();

        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML;
using System.Diagnostics;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            TextWriterTraceListener stdoutTrace = new TextWriterTraceListener(System.Console.Out);
            Debug.Listeners.Add(stdoutTrace);

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

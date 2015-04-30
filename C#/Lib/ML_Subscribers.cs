using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML
{
    public class ML_Subscribers : ML_Rest
    {
        public ML_Subscribers(string apiKey)
            : base(apiKey)
        {
            this.action = "subscribers";
        }

        public ML_Subscribers SetId(ulong id)
        {
            this.id = id;
            return this;
        }

        public object Add(string name, string email, List<Dictionary<string, string>> fields, bool resubscribe)
        {
            this.SetMethod("POST");
            this.AddToBody("name", name);
            this.AddToBody("email", email);
            this.AddToBody("fields", fields);
            this.AddToBody("resubscribe", resubscribe ? "1" : "0");
            return this.Execute(null, "");
        }

        public object Add(string name, string email, List<Dictionary<string, string>> fields)
        {
            return this.Add(name, email, fields, false);
        }

        public object AddAll(List<Dictionary<string, object>> subscribers, bool resubscribe)
        {
            this.SetMethod("POST");
            this.AddToBody("subscribers", subscribers);
            this.AddToBody("resubscribe", resubscribe ? "1" : "0");
            return this.Execute(null, "import");
        }

        public object AddAll(List<Dictionary<string, object>> subscribers)
        {
            return this.AddAll(subscribers, false);
        }

        public object Get(string email, bool history)
        {
            this.AddToBody("email", email);
            this.AddToBody("history", history ? "1" : "0");
            return this.Execute(null, "");
        }

        public object Remove(string email)
        {
            this.SetMethod("DELETE");
            this.AddToBody("email", email);
            return this.Execute(null, "");
        }

        public object Unsubscribe(string email)
        {
            this.SetMethod("POST");
            this.AddToBody("email", email);
            return this.Execute(null, "unsubscribe");
        }

    }
}

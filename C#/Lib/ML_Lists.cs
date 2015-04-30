using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML
{
    class ML_Lists : ML_Rest
    {
        public ML_Lists(string apiKey)
            : base(apiKey)
        {
            this.action = "lists";
        }

        public ML_Lists SetId(ulong id)
        {
            this.id = id;
            return this;
        }

        public object Add(string name)
        {
            this.SetMethod("POST");
            this.AddToBody("name", name);
            return this.Execute(null, "");
        }

        public object Update(string name)
        {
            this.SetMethod("PUT");
            this.AddToBody("name", name);
            return this.Execute(null, "");
        }

        public Object GetActive(Dictionary<string, object> data)
        {
            return this.Execute(data, "active");
        }

        public Object GetActive()
        {
            return this.GetActive(null);
        }

        public Object GetUnsubscribed(Dictionary<string, object> data)
        {
            return this.Execute(data, "unsubscribed");
        }

        public Object GetUnsubscribed()
        {
            return this.GetUnsubscribed(null);
        }

        public Object GetBounced(Dictionary<string, object> data)
        {
            return this.Execute(data, "bounced");
        }

        public Object GetBounced()
        {
            return this.GetBounced(null);
        }

    }
}

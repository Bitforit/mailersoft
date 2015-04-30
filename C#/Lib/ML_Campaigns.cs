using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML
{
    class ML_Campaigns : ML_Rest
    {

        public ML_Campaigns(string apiKey)
            : base(apiKey)
        {
            this.action = "campaigns";
        }

        public ML_Campaigns SetId(ulong id)
        {
            this.id = id;
            return this;
        }

        public object GetRecipients(Dictionary<string, object> data)
        {
            return this.Execute(data, "recipients");
        }

        public object GetRecipients()
        {
            return this.GetRecipients(null);
        }

        public object GetOpens(Dictionary<string, object> data)
        {
            return this.Execute(data, "opens");
        }

        public object GetOpens()
        {
            return this.GetOpens(null);
        }

        public object GetClicks(Dictionary<string, object> data)
        {
            return this.Execute(data, "clicks");
        }

        public object GetClicks()
        {
            return this.GetClicks(null);
        }

        public object GetUnsubscribes(Dictionary<string, object> data)
        {
            return this.Execute(data, "unsubscribes");
        }

        public object GetUnsubscribes()
        {
            return this.GetUnsubscribes(null);
        }

        public object GetBounces(Dictionary<string, object> data)
        {
            return this.Execute(data, "bounces");
        }

        public object GetBounces()
        {
            return this.GetBounces(null);
        }

        public object GetJunk(Dictionary<string, object> data)
        {
            return this.Execute(data, "junk");
        }

        public object GetJunk()
        {
            return this.GetJunk(null);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML
{
    public class ML_Rest : ML_Rest_Base
    {

        public ML_Rest(string apiKey)
            : base()
        {
            this.apiKey = apiKey;
        }

        public object GetAll(Dictionary<string, object> data)
        {
            return this.Execute(data, "");
        }

        public object GetAll()
        {
            return this.Execute(null, "");
        }

        public object Get(Dictionary<string, object> data, string name)
        {
            return this.Execute(data, name);
        }

        public object Get(Dictionary<string, object> data)
        {
            return this.Execute(data, "");
        }

        public object Get()
        {
            return this.Execute(null, "");
        }

        public object Delete()
        {
            this.SetMethod("DELETE");
            return this.Execute(null, "");
        }

    }
}

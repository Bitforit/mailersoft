using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML
{
    class ML_Messages : ML_Rest
    {

        private string recipientEmail, recipientName, emailFrom, nameFrom, type, language, replyEmail, replyName;
        private Dictionary<string, object> variables;
        private List<Dictionary<string, object>> batchRecipients;
        private List<Dictionary<string, string>> attachments;

        public ML_Messages(string apiKey)
            : base(apiKey)
        {
            this.action = "messages";
            this.Flush();
        }

        public ML_Messages SetId(ulong id)
        {
            this.id = id;
            return this;
        }

        public ML_Messages SetRecipient(string email, string name)
        {
            this.recipientEmail = email;
            this.recipientName = name;
            return this;
        }

        public ML_Messages SetFrom(string emailFrom, string name)
        {
            this.emailFrom = emailFrom;
            this.nameFrom = name;
            return this;
        }

        public ML_Messages SetType(string type)
        {
            this.type = type;
            return this;
        }

        public ML_Messages SetLanguage(string language)
        {
            this.language = language;
            return this;
        }

        public ML_Messages SetReplyTo(string replyEmail, string replyName)
        {
            this.replyEmail = replyEmail;
            this.replyName = replyName;
            return this;
        }

        public ML_Messages AddAttachment(string filename, string content)
        {
            Dictionary<string, string> attachment = new Dictionary<string, string>();
            attachment.Add("filename", filename);
            attachment.Add("content", content);
            this.attachments.Add(attachment);
            return this;
        }

        public ML_Messages AddRecipients(List<Dictionary<string, object>> recipients)
        {
            this.batchRecipients.AddRange(recipients);
            return this;
        }

        public ML_Messages SetVariable(string name, object value)
        {
            this.variables.Add(name, value);
            return this;
        }

        public ML_Messages SetVariables(Dictionary<string, object> variables)
        {
            foreach (var entry in variables)
                this.SetVariable(entry.Key, entry.Value);

            return this;
        }

        public object Send()
        {
            this.SetMethod("POST");
            this.AddToBody("mailId", this.id);
            this.AddToBody("language", this.language);
            this.AddToBody("fromName", this.nameFrom);
            this.AddToBody("fromEmail", this.emailFrom);
            if (this.batchRecipients.Count() > 0)
                this.AddToBody("batch", this.batchRecipients);
            else
            {
                this.AddToBody("recipientName", this.recipientName);
                this.AddToBody("recipientEmail", this.recipientEmail);
                this.AddToBody("variables", this.variables);
                this.AddToBody("attachments", this.attachments);
            }
            this.Flush();
            return this.Execute(null, "");
        }

        private void Flush()
        {
            this.recipientEmail = this.recipientName = this.emailFrom = this.nameFrom = this.type = this.language = "";
            this.variables = new Dictionary<string, object>();
            this.batchRecipients = new List<Dictionary<string, object>>();
            this.attachments = new List<Dictionary<string, string>>();
        }

    }
}

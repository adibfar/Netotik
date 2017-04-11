using System;
using System.Collections.Generic;

namespace Netotik.Common.Controller.MessageHelper
{
    [Serializable]
    public class Message
    {
        public const string TempDataKey = "TempDataShowMessage";
        private IList<TemplateMessage> _notyMessages;
        public bool DismissQueue { get; set; }
        public int MaxVisibleForQueue { get; set; }

        public IList<TemplateMessage> Messages
        {
            get { return _notyMessages; }
            set { _notyMessages = value; }
        }

        public Message()
        {
            Messages = new List<TemplateMessage>();
            MaxVisibleForQueue = 20;
        }
        public TemplateMessage AddNotyMessage(TemplateMessage message)
        {
            Messages.Add(message);
            return message;
        }


    }
}

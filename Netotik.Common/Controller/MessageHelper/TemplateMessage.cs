using System;

namespace Netotik.Common.Controller.MessageHelper
{
    [Serializable]
    public class TemplateMessage
    {
        public  MessageType Type{ get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public AnimationTypes CloseAnimation { get; set; }
        public AnimationTypes OpenAnimation { get; set; }
        public int AnimateSpeed { get; set; }
        public MessageCloseType CloseWith { get; set; }
    }
}
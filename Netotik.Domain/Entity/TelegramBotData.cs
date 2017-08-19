using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class TelegramBotData
    {
        public int Id { get; set; }
        public string ChatID { get; set; }
        public DateTime MessageDate { get; set; }
        public string Message { get; set; }
        public string MessageType { get; set; }

    }
}

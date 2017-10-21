using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;
using System.Web;
using System.ComponentModel;

namespace Netotik.ViewModels.Identity.UserRouter
{
    public class TelegramBotModel
    {
        public long Id { get; set; }
        public string TelegramBotToken { get; set; }
        public string AboutMessage { get; set; }
        public string ContactUsMessage { get; set; }
        public string ContactUsNumber { get; set; }
        public string ContactUsFirstName { get; set; }
        public string ContactUsLastName { get; set; }
    }
}

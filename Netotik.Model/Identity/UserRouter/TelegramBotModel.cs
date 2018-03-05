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
        [Display(ResourceType = typeof(Captions), Name = "TelegramBotToken")]
        public string TelegramBotToken { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "AboutMessage")]
        [MaxLength(200, ErrorMessage = "Max 200 Characters.(This is Telegram Limit)")]
        public string AboutMessage { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "ContactUsMessage")]
        public string ContactUsMessage { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "ContactUsNumber")]
        public string ContactUsNumber { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "FirstName")]
        public string ContactUsFirstName { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "LastName")]
        public string ContactUsLastName { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.IO;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http.Results;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Netotik.Services.Identity;
using Netotik.Services.Abstract;
using Netotik.Data;
using Netotik.Domain.Entity;

namespace Netotik.Web.Controllers
{
    public class TestController : ApiController
    {
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly ITelegramBotDataService _telegramBotDataService;
        private readonly IUnitOfWork _uow;

        public TestController()
        {
        }

        [HttpGet]
        [Route("api/test")]
        public string test()
        {
            return "okkkk";
        }

    }

}

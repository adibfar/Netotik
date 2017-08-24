﻿using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Netotik.Domain.Entity
{
    public class UserCompanyTelegram
    {
        public UserCompanyTelegram()
        {
        }
        public long Id { get; set; }
        public string TelegramBotToken { get; set; }
        public string AboutMessage { get; set; }
        public string ContactUsMessage { get; set; }
        public string ContactUsNumber { get; set; }

        public string ContactUsFirstName { get; set; }
        public string ContactUsLastName { get; set; }

        public virtual UserCompany UserCompany { get; set; }
    }
}

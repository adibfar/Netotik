using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Netotik.Domain.Entity
{
    public class SmsLog
    {
        public long Id { get; set; }
        public string MessageText { get; set; }
        public string From { get; set; }
        public string MobileNumber { get; set; }
        public string ResultId { get; set; }
        public ResultSms Result { get; set; }
        public DateTime CreateDate { get; set; }
        public long? UserId { get; set; }
        public virtual User User { get; set; }

    }

    public enum ResultSms : long
    {
        UsernameOrPasswordWrong = 0,
        Success,
        CreditNotValid,
        LimitedToDailySending,
        LimitedToSending,
        SenderNumberNotValid,
        SystemIsUpdating,
        TextNotValid,
        WebApiIsNotValid,
        UserDisabled,
        NotSended,
        UserDocumentsNeeded
    }

}

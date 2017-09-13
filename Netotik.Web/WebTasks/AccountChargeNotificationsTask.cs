using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Data.Entity;
using Netotik.Common.Scheduler;

namespace Netotik.Web.WebTasks
{
    public class AccountChargeNotificationsTask : ScheduledTaskTemplate
    {
        /// <summary>
        /// اگر چند جاب در يك زمان مشخص داشتيد، اين خاصيت ترتيب اجراي آن‌ها را مشخص خواهد كرد
        /// </summary>
        public override int Order
        {
            get { return 1; }
        }

        public override bool RunAt(DateTime utcNow)
        {
            if (this.IsShuttingDown || this.Pause)
                return false;

            var now = utcNow.AddHours(3.5);

            // هر چند وقت یکبار اجرا بشه رو اینجا مشخص می کنی
            return (now.DayOfYear / 2==0) && now.Minute == 46 && now.Second == 50;

        }

        public async override void Run()
        {
            if (this.IsShuttingDown || this.Pause)
                return;

            //کدهایی که باید اجرا شه اینجا بنویس
        }

        public override string Name
        {
            get { return "Account Charge"; }
        }
    }

}
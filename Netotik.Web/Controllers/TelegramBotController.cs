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

namespace Netotik.Web.Controllers
{
    public class TelegramBotController : ApiController
    {
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IUnitOfWork _uow;

        public TelegramBotController(
            IMikrotikServices mikrotikServices,
            IApplicationUserManager applicationUserManager,
            IUnitOfWork uow)
        {
            _mikrotikServices = mikrotikServices;
            _applicationUserManager = applicationUserManager;
            _uow = uow;
        }
        [Route(@"api/telegrambot/company/{CompanyCode}")]
        public async Task<OkResult> Client(string CompanyCode, [FromBody]Update update)
        {
            var company = await _applicationUserManager.FindByCompanyCodeAsync(CompanyCode);
            if (company == null) return Ok();

            var user = _applicationUserManager.FindUserById(company.Id);

            TelegramBotClient Api = new TelegramBotClient(user.UserCompany.TelegramBotToken);

            //Api.SetWebhookAsync("https://netotik.com:443/api/message/update").Wait();
            ForceReply markup = new ForceReply();
            markup.Force = true;

            var message = update.Message;

            if (message.Text == "درباره ما 📄")
            {

                await Api.SendChatActionAsync(message.Chat.Id, ChatAction.UploadPhoto);
                string file = HostingEnvironment.MapPath("\\Content\\Netotik.png");
                var fileName = file.Split('\\').Last();
                using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var fts = new FileToSend(fileName, fileStream);
                    await Api.SendPhotoAsync(message.Chat.Id, fts, "متن درباره ما شرکت به همراه آدرس لوگو کمپانی");
                }
            }

            else if (message.Text == "ارتباط با ما 📞")
            {
                await Api.SendTextMessageAsync(message.Chat.Id, "متن کمپانی جهت ارتباط با ما");
                await Api.SendContactAsync(message.Chat.Id, "00989333142211", "Ehsan", "Mirzaee");
            }
            else if (message.Text == "ورود کاربران 👱")
            {
                //اگر در جدول موقت زیر زمان معین برای این چت آی دی نوع پیام نام کاربری وجود نداشت
                await Api.SendTextMessageAsync(message.Chat.Id, text: "نام کاربری خود را وارد کنید 👱", replyMarkup: markup);//باید زمان فیلد زمان پیام بروز شود
                //در غیره این صورت به چک شود که در جدول موقت زیر زمان معین نوع پیام گذرواژه وجود دارد نداشت
                //به گذرواژه خود وارد کنید منتقل شود
                //درغیره این صورت به منوی کاربران منتقل شود و زمان فیلد پیام گذرواژه نیز به روز شود
            }
            else if (message.ReplyToMessage.Text != null && message.ReplyToMessage.Text == "نام کاربری خود را وارد کنید 👱")
            {
                //نام کاربری به جدول موقت اضافه شود 
                await Api.SendTextMessageAsync(message.Chat.Id, text: "گذرواژه خود را وارد کنید 🔐", replyMarkup: markup);
            }
            else if (message.ReplyToMessage.Text != null && message.ReplyToMessage.Text == "گذرواژه خود را وارد کنید 🔐")
            {
                //گذرواژه به جدول موقت اضافه شود
                //چک شود در صورتی که گذرواژه نام کاربری و گذرواژه درست نیست پیغام مورد نظر را بدهد
                //درغیره این صورت که درست بود منوی زیر برایش نمایش داده شود.
                await Api.SendTextMessageAsync(message.Chat.Id, "منوی کاربری 👱");
            }
            else if (message.Text == "اتصالات 👱")
            {
                //اعتبار نام کاربری و رمز عبور چک شود
                //نام کاربری و رمز عبور کاربر در جدول موقت به روز شود
                var keyboard = new ReplyKeyboardMarkup(new[]
               {
                    new [] // first row
                    {
                        new KeyboardButton("نمودار مصرف 7 روز 👱"),
                        new KeyboardButton("مصرف امروز 👱"),
                    },
                    new [] // last row
                    {
                        new KeyboardButton("فایل گزارش اتصالات 👱")
                    },
                    new [] // last row
                    {
                        new KeyboardButton("خروج 👱"),
                        new KeyboardButton("منوی کاربری 👱"),
                    }
                }, resizeKeyboard: true);
                await Api.SendTextMessageAsync(message.Chat.Id, " لطفا گزینه مورد نظر را انتخاب کنید",
                    replyMarkup: keyboard);
            }
            else if (message.Text == "فایل گزارش اتصالات 👱")
            {
                //چک شود نام کاربری و رمز عبور اعتبار دارد
                //بروزرسانی زمان در جدول موقت
                //برگرداندن فایل اتصالات
                await Api.SendTextMessageAsync(message.Chat.Id, "اتصالات 👱");
            }
            else if (message.Text == "مصرف امروز 👱")
            {
                //چک شود نام کاربری و رمز عبور اعتبار دارد
                //بروزرسانی زمان در جدول موقت
                //محاسبه مصرف امروز و برگرداندن آن
                await Api.SendTextMessageAsync(message.Chat.Id, "اتصالات 👱");
            }
            else if (message.Text == "نمودار مصرف 7 روز 👱")
            {
                //چک شود نام کاربری و رمز عبور اعتبار دارد
                //بروزرسانی زمان در جدول موقت
                //برگرداندن تصویر نمودار مصرف 7 روز گذشته
                await Api.SendTextMessageAsync(message.Chat.Id, "اتصالات 👱");
            }

            else if (message.Text == "زمان 👱")
            {
                //اعتبار نام کاربری و رمز عبور چک شود
                //نام کاربری و رمز عبور کاربر در جدول موقت به روز شود
                var keyboard = new ReplyKeyboardMarkup(new[]
               {
                    new [] // first row
                    {
                        new KeyboardButton("محدودیت اتصال 👱"),
                        new KeyboardButton("اعتبار زمانی 👱"),
                    },
                    new [] // last row
                    {
                        new KeyboardButton("زمان باقیمانده 👱"),
                        new KeyboardButton("مدت اتصال 👱")
                    },
                    new [] // last row
                    {
                        new KeyboardButton("آخرین اتصال 👱")
                    },
                    new [] // last row
                    {
                        new KeyboardButton("خروج 👱"),
                        new KeyboardButton("منوی کاربری 👱"),
                    }
                }, resizeKeyboard: true);
                await Api.SendTextMessageAsync(message.Chat.Id, " لطفا گزینه مورد نظر را انتخاب کنید",
                    replyMarkup: keyboard);
            }
            else if (message.Text == "آخرین اتصال 👱")
            {
                //چک شود نام کاربری و رمز عبور اعتبار دارد
                //بروزرسانی زمان در جدول موقت
                //محاسبه آخرین اتصال و برگرداندن آن
                await Api.SendTextMessageAsync(message.Chat.Id, "زمان 👱");
            }
            else if (message.Text == "مدت اتصال 👱")
            {
                //چک شود نام کاربری و رمز عبور اعتبار دارد
                //بروزرسانی زمان در جدول موقت
                //محاسبه مدت اتصال و برگرداندن آن
                await Api.SendTextMessageAsync(message.Chat.Id, "زمان 👱");
            }
            else if (message.Text == "زمان باقیمانده 👱")
            {
                //چک شود نام کاربری و رمز عبور اعتبار دارد
                //بروزرسانی زمان در جدول موقت
                //محاسبه زمان باقیمانده و برگرداندن آن
                await Api.SendTextMessageAsync(message.Chat.Id, "زمان 👱");
            }
            else if (message.Text == "اعتبار زمانی 👱")
            {
                //چک شود نام کاربری و رمز عبور اعتبار دارد
                //بروزرسانی زمان در جدول موقت
                //محاسبه اعتبار زمانی و برگرداندن آن
                await Api.SendTextMessageAsync(message.Chat.Id, "زمان 👱");
            }
            else if (message.Text == "محدودیت اتصال 👱")
            {
                //چک شود نام کاربری و رمز عبور اعتبار دارد
                //بروزرسانی زمان در جدول موقت
                //محاسبه محدودیت اتصال و برگرداندن آن
                await Api.SendTextMessageAsync(message.Chat.Id, "زمان 👱");
            }
            else if (message.Text == "حجم 👱")
            {
                //اعتبار نام کاربری و رمز عبور چک شود
                //نام کاربری و رمز عبور کاربر در جدول موقت به روز شود
                var keyboard = new ReplyKeyboardMarkup(new[]
               {
                    new [] // first row
                    {
                        new KeyboardButton("حجم باقیمانده 👱"),
                        new KeyboardButton("حجم مصرفی 👱"),
                    },
                    new [] // last row
                    {
                        new KeyboardButton("حجم کل 👱")
                    },
                    new [] // last row
                    {
                        new KeyboardButton("خروج 👱"),
                        new KeyboardButton("منوی کاربری 👱"),
                    }
                }, resizeKeyboard: true);
                await Api.SendTextMessageAsync(message.Chat.Id, " لطفا گزینه مورد نظر را انتخاب کنید",
                    replyMarkup: keyboard);
            }
            else if (message.Text == "حجم کل 👱")
            {
                //چک شود نام کاربری و رمز عبور اعتبار دارد
                //بروزرسانی زمان در جدول موقت
                //محاسبه حجم کل و برگرداندن آن
                await Api.SendTextMessageAsync(message.Chat.Id, "حجم 👱");
            }
            else if (message.Text == "حجم مصرفی 👱")
            {
                //چک شود نام کاربری و رمز عبور اعتبار دارد
                //بروزرسانی زمان در جدول موقت
                //محاسبه حجم مصرفی و برگرداندن آن
                await Api.SendTextMessageAsync(message.Chat.Id, "حجم 👱");
            }
            else if (message.Text == "حجم باقیمانده 👱")
            {
                //چک شود نام کاربری و رمز عبور اعتبار دارد
                //بروزرسانی زمان در جدول موقت
                //محاسبه حجم باقیمانده و برگرداندن آن
                await Api.SendTextMessageAsync(message.Chat.Id, "حجم 👱");
            }

            else if (message.Text == "مشخصات کاربری 👱")
            {
                //چک شود نام کاربری و رمز عبور اعتبار دارد
                //بروزرسانی زمان در جدول موقت
                //برگرداندن مشخصات کاربر
                await Api.SendTextMessageAsync(message.Chat.Id, "منوی کاربری 👱");
            }


            else if (message.Text == "خروج 👱")
            {
                //نام کاربری و رمز عبور از جدول موقت حذف شوند
                await Api.SendTextMessageAsync(message.Chat.Id, "منوی اصلی");
            }
            else if (message.Text == "منوی کاربری 👱")
            {
                //چک شود که نام کاربری و رمز عبور اعتبار زمانی دارن
                //نام کاربری و رمز عبور در جدول موقت به روز شود
                var keyboard = new ReplyKeyboardMarkup(new[]
               {
                    new [] // first row
                    {
                        new KeyboardButton("حجم 👱"),
                        new KeyboardButton("مشخصات کاربری 👱"),
                    },
                    new [] // last row
                    {
                        new KeyboardButton("زمان 👱"),
                        new KeyboardButton("اتصالات 👱"),
                    },
                    new [] // last row
                    {
                        new KeyboardButton("خروج 👱"),
                        new KeyboardButton("منوی اصلی"),
                    }
                }, resizeKeyboard: true);
                await Api.SendTextMessageAsync(message.Chat.Id, " لطفا گزینه مورد نظر را انتخاب کنید",
                    replyMarkup: keyboard);
            }




            //main menu
            else if (message.Text == "منوی اصلی")
            {
                var keyboard = new ReplyKeyboardMarkup(new[]
               {
                    new [] // first row
                    {
                        new KeyboardButton("درباره ما 📄"),
                        new KeyboardButton("ارتباط با ما 📞"),
                    },
                    new [] // last row
                    {
                        new KeyboardButton("ورود کاربران 👱"),
                        new KeyboardButton("ورود مدیر 👷"),
                    }
                }, resizeKeyboard: true);
                await Api.SendTextMessageAsync(message.Chat.Id, "به ربات تلگرامی " + user.UserCompany.CompanyCode + " خوش آمدید.لطفا یکی از گزینه ها را انتخاب کنید.",
                    replyMarkup: keyboard);
            }
            else
            {
                var keyboard = new ReplyKeyboardMarkup(new[]
              {
                    new [] // first row
                    {
                        new KeyboardButton("📄 درباره ما"),
                        new KeyboardButton("ارتباط با ما 📞"),
                    },
                    new [] // last row
                    {
                        new KeyboardButton("ورود کاربران 👱"),
                        new KeyboardButton("ورود مدیر 👷"),
                    }
                }, resizeKeyboard: true);
                await Api.SendTextMessageAsync(message.Chat.Id, "به ربات تلگرامی " + user.UserCompany.CompanyCode + " خوش آمدید.لطفا یکی از گزینه ها را انتخاب کنید.",
                    replyMarkup: keyboard);
            }
            return Ok();
        }

    }



}

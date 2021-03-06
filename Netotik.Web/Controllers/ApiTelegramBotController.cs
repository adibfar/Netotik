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
using Netotik.IocConfig;
using Netotik.Web.Infrastructure;

namespace Netotik.Web.Controllers
{
    public class ApiTelegramBotController : ApiController
    {
        private IApplicationUserManager _applicationUserManager;
        private IMikrotikServices _mikrotikServices;
        private ITelegramBotDataService _telegramBotDataService;
        private IUnitOfWork _uow;

        //[HttpGet]
        //public string test()
        //{
        //    _uow = ProjectObjectFactory.Container.GetInstance<IUnitOfWork>();
        //    return "okk";
        //}

        [Route(@"api/telegrambot/Router/{RouterCode}")]
        public async Task<OkResult> Client(string RouterCode, [FromBody]Update update)
        {
            _uow = ProjectObjectFactory.Container.GetInstance<IUnitOfWork>();
            _applicationUserManager = ProjectObjectFactory.Container.GetInstance<IApplicationUserManager>();
            _mikrotikServices = ProjectObjectFactory.Container.GetInstance<IMikrotikServices>();
            _telegramBotDataService = ProjectObjectFactory.Container.GetInstance<ITelegramBotDataService>();
            try
            {
                #region Detect Router
                var user = await _applicationUserManager.FindByRouterCodeAsync(RouterCode);
                if (user == null) return Ok();
                #endregion
                #region API Token
                TelegramBotClient Api = new TelegramBotClient(user.UserRouter.UserRouterTelegram.TelegramBotToken);
                #endregion
                #region StartUp
                //Api.SetWebhookAsync("https://netotik.com:443/api/message/update").Wait();
                ForceReply markup = new ForceReply();
                markup.Force = true;
                var message = update.Message;
                //var MessageId = await Api.SendTextMessageAsync(message.Chat.Id, "لطفا تا دریافت پاسخ شکیبا باشید.🌹");
                try
                {
                    using (StreamWriter _testData = new StreamWriter(HostingEnvironment.MapPath("~/TelegramBotErrors2.txt"), true))
                    {
                        _testData.WriteLine(update.CallbackQuery.Id + " \t "); // Write the file.
                    }
                }
                catch { }



                await Api.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                var TelegramBotDataTable = _telegramBotDataService.GetList(user.Id, message.Chat.Id).ToList();
                bool ReplyMessageFlag = false;
                try
                {
                    if (message.ReplyToMessage != null)
                    {
                        ReplyMessageFlag = true;
                    }
                }
                catch
                {
                    ReplyMessageFlag = false;
                }
                #endregion


                #region AboutUs
                if (message.Text == "درباره ما 📄")
                {

                    await Api.SendChatActionAsync(message.Chat.Id, ChatAction.UploadPhoto);
                    //string file = HostingEnvironment.MapPath("\\Content\\images\\logo\\NetotikLogo.png");
                    string file = HostingEnvironment.MapPath(
                        user.PictureId.HasValue ?
                        "\\Content\\Upload\\Image\\Avatar\\" + user.Picture.FileName
                        : "\\Content\\images\\logo\\NetotikLogo.png");
                    


                    var fileName = file.Split('\\').Last();
                    using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        var fts = new FileToSend(fileName, fileStream);
                        await Api.SendPhotoAsync(message.Chat.Id, fts, user.UserRouter.UserRouterTelegram.AboutMessage);
                    }
                }
                #endregion

                #region ContactUS
                else if (message.Text == "ارتباط با ما 📞")
                {
                    await Api.SendTextMessageAsync(message.Chat.Id, user.UserRouter.UserRouterTelegram.ContactUsMessage);
                    await Api.SendContactAsync(message.Chat.Id, user.UserRouter.UserRouterTelegram.ContactUsNumber, user.UserRouter.UserRouterTelegram.ContactUsFirstName, user.UserRouter.UserRouterTelegram.ContactUsLastName);
                }
                #endregion

                #region User
                #region Login
                else if (message.Text == "ورود کاربران 👱")
                {
                    var Username = TelegramBotDataTable.Where(x => x.MessageType == "Username").LastOrDefault();
                    var WrongUserPass = TelegramBotDataTable.Where(x => x.MessageType == "WrongUserPass");

                    if (Username == null || (WrongUserPass.Count() > 1 && WrongUserPass.Count() < 5 && WrongUserPass.LastOrDefault().MessageDate > Username.MessageDate))
                        await Api.SendTextMessageAsync(message.Chat.Id, text: "نام کاربری خود را وارد کنید 👱", replyMarkup: markup);//باید زمان فیلد زمان پیام بروز شود
                    else
                    {
                        Username.MessageDate = DateTime.Now;
                        _telegramBotDataService.Update(Username);

                        var Password = TelegramBotDataTable.Where(x => x.MessageType == "Password").LastOrDefault();
                        if (Password == null)
                            await Api.SendTextMessageAsync(message.Chat.Id, text: "گذرواژه خود را وارد کنید 🔐", replyMarkup: markup);//باید زمان فیلد زمان پیام بروز شود
                        else
                        {
                            Password.MessageDate = DateTime.Now;
                            _telegramBotDataService.Update(Password);
                            var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی کاربری 👱") } }, resizeKeyboard: true);
                            await Api.SendTextMessageAsync(message.Chat.Id, "جهت ورود به منوی کاربری ، گزینه مورد نظر را انتخاب کنید.", replyMarkup: keyboard);
                        }
                    }
                }
                else if (ReplyMessageFlag)
                {
                    if (message.ReplyToMessage.Text == "نام کاربری خود را وارد کنید 👱")
                    {
                        _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, RouterId = user.Id, Message = message.Text, MessageDate = DateTime.Now, MessageType = "Username" });
                        await Api.SendTextMessageAsync(message.Chat.Id, text: "گذرواژه خود را وارد کنید 🔐", replyMarkup: markup);
                    }
                    else if (message.ReplyToMessage.Text == "گذرواژه خود را وارد کنید 🔐")
                    {
                        _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, RouterId = user.Id, Message = message.Text, MessageDate = DateTime.Now, MessageType = "Password" });
                        var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی کاربری 👱") } }, resizeKeyboard: true);
                        await Api.SendTextMessageAsync(message.Chat.Id, "جهت ورود به منوی کاربری ، گزینه مورد نظر را انتخاب کنید.", replyMarkup: keyboard);
                    }
                }
                #endregion
                #region Connection
                else if (message.Text == "اتصالات2 📊")
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
                #endregion
                #region Sessions File
                else if (message.Text == "فایل گزارش اتصالات 👱")
                {
                    //چک شود نام کاربری و رمز عبور اعتبار دارد
                    //بروزرسانی زمان در جدول موقت
                    //برگرداندن فایل اتصالات
                }
                #endregion
                #region Today Usage
                else if (message.Text == "مصرف امروز 👱")
                {
                    //چک شود نام کاربری و رمز عبور اعتبار دارد
                    //بروزرسانی زمان در جدول موقت
                    //محاسبه مصرف امروز و برگرداندن آن
                }
                #endregion
                #region 7Day Chart
                else if (message.Text == "نمودار مصرف 7 روز 👱")
                {
                    //چک شود نام کاربری و رمز عبور اعتبار دارد
                    //بروزرسانی زمان در جدول موقت
                    //برگرداندن تصویر نمودار مصرف 7 روز گذشته
                }
                #endregion
                #region Time Menu
                else if (message.Text == "زمان 🕐")
                {
                    #region Find In DB
                    var Username = TelegramBotDataTable.Where(x => x.MessageType == "Username").LastOrDefault();
                    var Password = TelegramBotDataTable.Where(x => x.MessageType == "Password").LastOrDefault();
                    #endregion
                    #region Check DB Response
                    if (Username == null || Password == null)
                    {
                        var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                        await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور وارد نشده است.", replyMarkup: keyboard);
                    }
                    #endregion
                    else
                    {
                        #region Check Router IP Port
                        if (await _mikrotikServices.IP_Port_CheckAsync(user.UserRouter.R_Host, user.UserRouter.R_Port, user.UserRouter.R_User, user.UserRouter.R_Password) && await _mikrotikServices.User_Pass_CheckAsync(user.UserRouter.R_Host, user.UserRouter.R_Port, user.UserRouter.R_User, user.UserRouter.R_Password))
                        {
                            #endregion
                            #region Find In Router
                            var UsermanUser = await _mikrotikServices.Usermanager_GetUserAsync(user.UserRouter.R_Host, user.UserRouter.R_Port, user.UserRouter.R_User, user.UserRouter.R_Password, Username.Message);
                            if (UsermanUser == null)
                            {
                                _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, RouterId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
                                var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                                await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور اشتباه می باشد.", replyMarkup: keyboard);
                            }
                            if (UsermanUser != null)
                                if (UsermanUser.FirstOrDefault().username == Username.Message && UsermanUser.FirstOrDefault().password == Password.Message)
                                {
                                    #endregion
                                    #region Update DB
                                    Password.MessageDate = Username.MessageDate = DateTime.Now;
                                    _telegramBotDataService.Update(Username);
                                    _telegramBotDataService.Update(Password);
                                    string LastSeen = ""; string UptimeUsed = ""; string Validity = ""; string UpTimeLimit = "";
                                    #endregion
                                    //----------------------------------------
                                    if (UsermanUser.FirstOrDefault().last_seen == null || UsermanUser.FirstOrDefault().last_seen == "")
                                    {
                                        LastSeen = "شما هنوز اتصالی نداشته اید. \n";
                                    }
                                    else
                                    {
                                        LastSeen = "آخرین اتصال شما : " + Infrastructure.EnglishConvertDate.ConvertToFa(UsermanUser.FirstOrDefault().last_seen.Split(' ')[0], "D") + " " + UsermanUser.FirstOrDefault().last_seen.Split(' ')[1] + " می باشد. \n";
                                    }
                                    if (UsermanUser.FirstOrDefault().uptime_used == null || UsermanUser.FirstOrDefault().uptime_used == "")
                                    {
                                        UptimeUsed = "\n";
                                    }
                                    else
                                    {
                                        UptimeUsed = "شما به مدت : " + UsermanUser.FirstOrDefault().uptime_used.Replace("d", "روز").Replace("w", "هفته").Replace("h", "ساعت").Replace("m", "دقیقه").Replace("s", "ثانیه").Replace("never", "0ثانیه") + " به شبکه (اینترنت) متصل بوده اید.";
                                    }
                                    var ProfileLimitions = _mikrotikServices.Usermanager_GetAllProfileLimition(user.UserRouter.R_Host, user.UserRouter.R_Port, user.UserRouter.R_User, user.UserRouter.R_Password);
                                    var Profiles = _mikrotikServices.Usermanager_GetAllProfile(user.UserRouter.R_Host, user.UserRouter.R_Port, user.UserRouter.R_User, user.UserRouter.R_Password);
                                    var Limitions = _mikrotikServices.Usermanager_GetAllLimition(user.UserRouter.R_Host, user.UserRouter.R_Port, user.UserRouter.R_User, user.UserRouter.R_Password);
                                    foreach (var Profilelimition in ProfileLimitions)
                                    {

                                        if (Profilelimition.profile == UsermanUser.FirstOrDefault().actual_profile)
                                        {
                                            foreach (var Profile in Profiles)
                                            {
                                                if (Profilelimition.limitation == Profile.name)
                                                {
                                                    if (Profile.validity == null || Profile.validity == "" || Profile.validity == "0s")
                                                    {

                                                        Validity = "🕐 اعتبار اکانت شما به صورت نامحدود می باشد.";
                                                    }
                                                    else
                                                    {
                                                        Validity = "🕐 اعتبار اکانت شما: " + Profile.validity.Replace("d", "روز ").Replace("w", "هفته ").Replace("h", "ساعت ").Replace("m", "دقیقه ").Replace("s", "ثانیه ").Replace("never", "0ثانیه ") + " می باشد.";
                                                    }
                                                }

                                                foreach (var limition in Limitions)
                                                {
                                                    if (Profilelimition.limitation == limition.name)
                                                    {
                                                        if (limition.uptime_limit == null || limition.uptime_limit == "" || limition.uptime_limit == "0s")
                                                        {
                                                            UpTimeLimit = "🕐 محدودیت اتصالی برای شما وجود ندارد.";
                                                        }
                                                        else
                                                        {
                                                            UpTimeLimit = "🕐 شما میتوانید به مدت: " + limition.uptime_limit.Replace("d", "روز ").Replace("w", "هفته ").Replace("h", "ساعت ").Replace("m", "دقیقه ").Replace("s", "ثانیه ").Replace("never", "0ثانیه ") + " به شبکه(اینترنت)متصل باشید.";
                                                        }
                                                    }
                                                }
                                            }

                                        }
                                    }
                                    await Api.SendTextMessageAsync(message.Chat.Id,
                                        LastSeen + "\n" + UptimeUsed + "\n \n" + Validity + "\n \n" + UpTimeLimit
                                        );


                                    /*
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
                                        */
                                    //------------------------------------------
                                }
                                #region Error Response
                                else
                                {
                                    _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, RouterId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
                                    var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                                    await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور اشتباه می باشد.", replyMarkup: keyboard);
                                }
                            else
                            {
                                _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, RouterId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
                                var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                                await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور اشتباه می باشد.", replyMarkup: keyboard);
                            }

                        }
                        else
                        {
                            var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                            await Api.SendTextMessageAsync(message.Chat.Id, "خطا در اتصال به دستگاه", replyMarkup: keyboard);
                        }
                    }
                    #endregion
                    //اعتبار نام کاربری و رمز عبور چک شود
                    //نام کاربری و رمز عبور کاربر در جدول موقت به روز شود

                }
                #endregion
                #region Traffic
                else if (message.Text == "حجم ⬆⬇")
                {
                    var Username = TelegramBotDataTable.Where(x => x.MessageType == "Username").LastOrDefault();
                    var Password = TelegramBotDataTable.Where(x => x.MessageType == "Password").LastOrDefault();
                    if (Username == null || Password == null)
                    {
                        var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                        await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور وارد نشده است.", replyMarkup: keyboard);
                    }
                    else
                    {

                        if (_mikrotikServices.IP_Port_Check(user.UserRouter.R_Host, user.UserRouter.R_Port, user.UserRouter.R_User, user.UserRouter.R_Password) && _mikrotikServices.User_Pass_Check(user.UserRouter.R_Host, user.UserRouter.R_Port, user.UserRouter.R_User, user.UserRouter.R_Password))
                        {
                            var UsermanUser = _mikrotikServices.Usermanager_GetUser(user.UserRouter.R_Host, user.UserRouter.R_Port, user.UserRouter.R_User, user.UserRouter.R_Password, Username.Message);
                            if (UsermanUser == null)
                            {
                                _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, RouterId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
                                var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                                await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور اشتباه می باشد.", replyMarkup: keyboard);
                            }
                            if (UsermanUser != null)
                                if (UsermanUser.FirstOrDefault().username == Username.Message && UsermanUser.FirstOrDefault().password == Password.Message)
                                {
                                    _telegramBotDataService.Update(Username);
                                    _telegramBotDataService.Update(Password);
                                    string AllTraffic = "";
                                    string RemainTraffic = "";
                                    var ProfileLimitions = _mikrotikServices.Usermanager_GetAllProfileLimition(user.UserRouter.R_Host, user.UserRouter.R_Port, user.UserRouter.R_User, user.UserRouter.R_Password);
                                    var Limitions = _mikrotikServices.Usermanager_GetAllLimition(user.UserRouter.R_Host, user.UserRouter.R_Port, user.UserRouter.R_User, user.UserRouter.R_Password);
                                    foreach (var Profilelimition in ProfileLimitions)
                                    {

                                        if (Profilelimition.profile == UsermanUser.FirstOrDefault().actual_profile)
                                        {
                                            foreach (var limition in Limitions)
                                            {
                                                if (Profilelimition.limitation == limition.name)
                                                {
                                                    limition.transfer_limit = limition.transfer_limit == null ? "0" : limition.transfer_limit;
                                                    limition.download_limit = limition.download_limit == null ? "0" : limition.download_limit;
                                                    limition.upload_limit = limition.upload_limit == null ? "0" : limition.upload_limit;
                                                    UsermanUser.FirstOrDefault().upload_used = UsermanUser.FirstOrDefault().upload_used == null ? "0" : UsermanUser.FirstOrDefault().upload_used;
                                                    UsermanUser.FirstOrDefault().download_used = UsermanUser.FirstOrDefault().download_used == null ? "0" : UsermanUser.FirstOrDefault().download_used;
                                                    AllTraffic = ulong.Parse(limition.transfer_limit) + ulong.Parse(limition.upload_limit) + ulong.Parse(limition.download_limit) == 0 ? "نامحدود" : ((ulong.Parse(limition.transfer_limit) + ulong.Parse(limition.upload_limit) + ulong.Parse(limition.download_limit)) / 1048576).ToString() + " مگابایت ";
                                                    if ((ulong.Parse(limition.transfer_limit) + ulong.Parse(limition.upload_limit) + ulong.Parse(limition.download_limit)) < (ulong.Parse(UsermanUser.FirstOrDefault().download_used) + ulong.Parse(UsermanUser.FirstOrDefault().upload_used)))
                                                        RemainTraffic = "(غیره قابل محاسبه)";
                                                    else
                                                        RemainTraffic = AllTraffic == "نامحدود" ? "نامحدود" : (((ulong.Parse(limition.transfer_limit) + ulong.Parse(limition.upload_limit) + ulong.Parse(limition.download_limit)) - (ulong.Parse(UsermanUser.FirstOrDefault().download_used) + ulong.Parse(UsermanUser.FirstOrDefault().upload_used))) / 1048576).ToString() + " مگابایت";
                                                }
                                            }

                                        }
                                    }
                                    await Api.SendTextMessageAsync(
                                                    message.Chat.Id,
                                                    "میزان حجم کلی شما: " + AllTraffic + "می باشد." + "\n \n" +
                                                    " میزان دانلود⬇ شما " + (ulong.Parse(UsermanUser.FirstOrDefault().download_used) / 1048576).ToString() + " مگابایت می باشد " + "\n" +
                                                    " میزان آپلود⬆ شما " + (ulong.Parse(UsermanUser.FirstOrDefault().upload_used) / 1048576).ToString() + " مگابایت می باشد " + "\n" +
                                                    " میزان دانلود⬇ + آپلود⬆ شما " + ((ulong.Parse(UsermanUser.FirstOrDefault().download_used) + ulong.Parse(UsermanUser.FirstOrDefault().upload_used)) / 1048576).ToString() + " مگابایت می باشد " + "\n \n" +
                                                    "میزان حجم باقیمانده شما: " + RemainTraffic + "می باشد." + "\n"
                                                        );

                                    /*
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
                                        */
                                }
                                else
                                {
                                    _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, RouterId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
                                    var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                                    await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور اشتباه می باشد.", replyMarkup: keyboard);
                                }
                            else
                            {
                                _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, RouterId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
                                var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                                await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور اشتباه می باشد.", replyMarkup: keyboard);
                            }

                        }
                        else
                        {
                            var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                            await Api.SendTextMessageAsync(message.Chat.Id, "خطا در اتصال به دستگاه", replyMarkup: keyboard);
                        }
                    }
                }
                #endregion
                #region User Information
                else if (message.Text == "مشخصات کاربری 📋")
                {
                    var Username = TelegramBotDataTable.Where(x => x.MessageType == "Username").LastOrDefault();
                    var Password = TelegramBotDataTable.Where(x => x.MessageType == "Password").LastOrDefault();
                    if (Username == null || Password == null)
                    {
                        var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                        await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور وارد نشده است.", replyMarkup: keyboard);
                    }
                    else
                    {

                        if (_mikrotikServices.IP_Port_Check(user.UserRouter.R_Host, user.UserRouter.R_Port, user.UserRouter.R_User, user.UserRouter.R_Password) && _mikrotikServices.User_Pass_Check(user.UserRouter.R_Host, user.UserRouter.R_Port, user.UserRouter.R_User, user.UserRouter.R_Password))
                        {
                            var UsermanUser = _mikrotikServices.Usermanager_GetUser(user.UserRouter.R_Host, user.UserRouter.R_Port, user.UserRouter.R_User, user.UserRouter.R_Password, Username.Message);
                            if (UsermanUser == null)
                            {
                                _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, RouterId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
                                var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                                await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور اشتباه می باشد.", replyMarkup: keyboard);
                            }
                            if (UsermanUser != null)
                                if (UsermanUser.FirstOrDefault().username == Username.Message && UsermanUser.FirstOrDefault().password == Password.Message)
                                {
                                    _telegramBotDataService.Update(Username);
                                    _telegramBotDataService.Update(Password);
                                    await Api.SendTextMessageAsync(
                                        message.Chat.Id,
                                        "📋 مشخصات کاربری" + "\n" +
                                        "نام و نام خانوادگی: " + UsermanUser.FirstOrDefault().first_name + " " + UsermanUser.FirstOrDefault().last_name + "\n" +
                                        "نام کاربری: " + UsermanUser.FirstOrDefault().username + "\n" +
                                        "نام تعرفه: " + UsermanUser.FirstOrDefault().actual_profile + "\n" +
                                        "آدرس ایمیل: " + UsermanUser.FirstOrDefault().email + "\n" +
                                        "شماره تماس: " + UsermanUser.FirstOrDefault().phone + "\n"
                                        );
                                }
                                else
                                {
                                    _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, RouterId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
                                    var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                                    await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور اشتباه می باشد.", replyMarkup: keyboard);
                                }
                            else
                            {
                                _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, RouterId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
                                var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                                await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور اشتباه می باشد.", replyMarkup: keyboard);
                            }

                        }
                        else
                        {
                            var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                            await Api.SendTextMessageAsync(message.Chat.Id, "خطا در اتصال به دستگاه", replyMarkup: keyboard);
                        }
                    }

                }
                #endregion
                #region Exit User
                else if (message.Text == "خروج 🔚")
                {
                    var Username = TelegramBotDataTable.Where(x => x.MessageType == "Username").LastOrDefault();
                    var Password = TelegramBotDataTable.Where(x => x.MessageType == "Password").LastOrDefault();
                    _telegramBotDataService.Remove(Username);
                    _telegramBotDataService.Remove(Password);
                    _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, RouterId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "Exit" });
                    var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                    await Api.SendTextMessageAsync(message.Chat.Id, "خروج موفقیت آمیر بود.🌹", replyMarkup: keyboard);
                }
                #endregion
                #region UserMenu
                else if (message.Text == "منوی کاربری 👱")
                {
                    #region Find In DB
                    var Username = TelegramBotDataTable.Where(x => x.MessageType == "Username").LastOrDefault();
                    var Password = TelegramBotDataTable.Where(x => x.MessageType == "Password").LastOrDefault();
                    #endregion
                    #region Check DB Response
                    if (Username == null || Password == null)
                    {
                        var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                        await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور وارد نشده است.", replyMarkup: keyboard);
                    }
                    #endregion
                    else
                    {
                        #region Check Router IP Port
                        if (await _mikrotikServices.IP_Port_CheckAsync(user.UserRouter.R_Host, user.UserRouter.R_Port, user.UserRouter.R_User, user.UserRouter.R_Password) && await _mikrotikServices.User_Pass_CheckAsync(user.UserRouter.R_Host, user.UserRouter.R_Port, user.UserRouter.R_User, user.UserRouter.R_Password))
                        {
                            #endregion
                            #region Find In Router
                            var UsermanUser = await _mikrotikServices.Usermanager_GetUserAsync(user.UserRouter.R_Host, user.UserRouter.R_Port, user.UserRouter.R_User, user.UserRouter.R_Password, Username.Message);
                            if (UsermanUser == null)
                            {
                                _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, RouterId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
                                var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                                await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور اشتباه می باشد.", replyMarkup: keyboard);
                            }
                            if (UsermanUser != null)
                                if (UsermanUser.FirstOrDefault().username == Username.Message && UsermanUser.FirstOrDefault().password == Password.Message)
                                {
                                    #endregion
                                    #region Update DB
                                    Password.MessageDate = Username.MessageDate = DateTime.Now;
                                    _telegramBotDataService.Update(Username);
                                    _telegramBotDataService.Update(Password);
                                    #endregion
                                    var keyboard = new ReplyKeyboardMarkup(new[]
                                    {
                                    new [] // first row
                                    {
                                        new KeyboardButton("مشخصات کاربری 📋"),
                                    },
                                    new [] // last row
                                    {
                                        new KeyboardButton("زمان 🕐"),
                                        new KeyboardButton("حجم ⬆⬇"),
                                    },
                                    new [] // last row
                                    {
                                        new KeyboardButton("خروج 🔚"),
                                        new KeyboardButton("منوی اصلی"),
                                    }
                                }, resizeKeyboard: true);
                                    await Api.SendTextMessageAsync(message.Chat.Id, " لطفا گزینه مورد نظر را انتخاب کنید",
                                        replyMarkup: keyboard);
                                }
                                #region Error Response
                                else
                                {
                                    _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, RouterId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
                                    var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                                    await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور اشتباه می باشد.", replyMarkup: keyboard);
                                }
                            else
                            {
                                _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, RouterId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
                                var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                                await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور اشتباه می باشد.", replyMarkup: keyboard);
                            }

                        }
                        else
                        {
                            var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                            await Api.SendTextMessageAsync(message.Chat.Id, "خطا در اتصال به دستگاه", replyMarkup: keyboard);
                        }
                    }
                    #endregion

                }
                #endregion

                #endregion

                #region AdminMenu
                else if (message.Text == "ورود مدیر 👷")
                {

                    await Api.SendTextMessageAsync(message.Chat.Id, "این قسمت هنوز پیاده سازی نشده است.");
                }
                #endregion
                #region MainMenu
                else if (message.Text == "منوی اصلی")
                {

                    //    var keyboard2 = new InlineKeyboardMarkup(new[]
                    //   {
                    //    new [] // first row
                    //    {
                    //        new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("درباره ما 📄","1"),
                    //        new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("ارتباط با ما 📞","2"),
                    //    },
                    //    new [] // last row
                    //    {
                    //        new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("ورود کاربران 👱","3"),
                    //        new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("ورود مدیر 👷","4"),
                    //    }
                    //});
                    //    await Api.SendTextMessageAsync(message.Chat.Id, "به ربات تلگرامی " + RouterCode + " خوش آمدید.لطفا یکی از گزینه ها را انتخاب کنید.",
                    //        replyMarkup: keyboard2);




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
                        //new KeyboardButton("ورود مدیر 👷"),
                    }
                }, resizeKeyboard: true);
                    await Api.SendTextMessageAsync(message.Chat.Id, "به ربات تلگرامی " + RouterCode + " خوش آمدید.لطفا یکی از گزینه ها را انتخاب کنید.",
                        replyMarkup: keyboard);
                }

                //main menu

                else
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
                        //new KeyboardButton("ورود مدیر 👷"),
                    }
                }, resizeKeyboard: true);
                    await Api.SendTextMessageAsync(message.Chat.Id, "به ربات تلگرامی " + RouterCode + " خوش آمدید.لطفا یکی از گزینه ها را انتخاب کنید.",
                        replyMarkup: keyboard);
                }

                #endregion
                //await Api.DeleteMessageAsync(message.Chat.Id, MessageId.MessageId);
            }
            catch (Exception ex)
            {
                using (StreamWriter _testData = new StreamWriter(HostingEnvironment.MapPath("~/TelegramBotErrors.txt"), true))
                {
                    _testData.WriteLine(DateTime.Now + " \t " + ex); // Write the file.
                }
            }
            _uow.SaveAllChanges();
            return Ok();
        }

    }



}
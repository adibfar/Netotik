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
    public class TelegramBotController : ApiController
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

        [Route(@"api/telegrambot/company/{CompanyCode}")]
        public async Task<OkResult> Client(string CompanyCode, [FromBody]Update update)
        {
            _uow = ProjectObjectFactory.Container.GetInstance<IUnitOfWork>();
            _applicationUserManager = ProjectObjectFactory.Container.GetInstance<IApplicationUserManager>();
            _mikrotikServices = ProjectObjectFactory.Container.GetInstance<IMikrotikServices>();
            _telegramBotDataService = ProjectObjectFactory.Container.GetInstance<ITelegramBotDataService>();
            #region Detect Company
            var user = await _applicationUserManager.FindByCompanyCodeAsync(CompanyCode);
            if (user == null) return Ok();
            #endregion
            #region API Token
            TelegramBotClient Api = new TelegramBotClient(user.UserCompany.UserCompanyTelegram.TelegramBotToken);
            #endregion
            #region StartUp
            //Api.SetWebhookAsync("https://netotik.com:443/api/message/update").Wait();
            ForceReply markup = new ForceReply();
            markup.Force = true;

            var message = update.Message;
            var TelegramBotDataTable = _telegramBotDataService.GetList(user.Id, message.Chat.Id);
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

            try
            {
                #region AboutUs
                if (message.Text == "درباره ما 📄")
                {

                    await Api.SendChatActionAsync(message.Chat.Id, ChatAction.UploadPhoto);
                    string file = HostingEnvironment.MapPath("\\Content\\images\\logo\\NetotikLogo.png");
                    var fileName = file.Split('\\').Last();
                    using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        var fts = new FileToSend(fileName, fileStream);
                        await Api.SendPhotoAsync(message.Chat.Id, fts, user.UserCompany.UserCompanyTelegram.AboutMessage);
                    }
                }
                #endregion

                #region ContactUS
                else if (message.Text == "ارتباط با ما 📞")
                {
                    await Api.SendTextMessageAsync(message.Chat.Id, user.UserCompany.UserCompanyTelegram.ContactUsMessage);
                    await Api.SendContactAsync(message.Chat.Id, user.UserCompany.UserCompanyTelegram.ContactUsNumber, user.UserCompany.UserCompanyTelegram.ContactUsFirstName, user.UserCompany.UserCompanyTelegram.ContactUsLastName);
                }
                #endregion

                #region User
                #region Login
                else if (message.Text == "ورود کاربران 👱")
                {
                    var Username = TelegramBotDataTable.Where(x => x.MessageType == "Username").LastOrDefault();
                    if (Username == null)
                        await Api.SendTextMessageAsync(message.Chat.Id, text: "نام کاربری خود را وارد کنید 👱", replyMarkup: markup);//باید زمان فیلد زمان پیام بروز شود
                    else
                    {
                        TelegramBotData UsernameUpdate = new TelegramBotData() { ChatId = Username.ChatId, CompanyId = Username.CompanyId, Message = Username.Message, MessageType = Username.MessageType, MessageDate = DateTime.Now, Id = Username.Id };
                        _telegramBotDataService.Update(UsernameUpdate);

                        var Password = TelegramBotDataTable.Where(x => x.MessageType == "Password").LastOrDefault();
                        if (Password == null)
                            await Api.SendTextMessageAsync(message.Chat.Id, text: "گذرواژه خود را وارد کنید 🔐", replyMarkup: markup);//باید زمان فیلد زمان پیام بروز شود
                        else
                        {
                            TelegramBotData PasswordUpdate = new TelegramBotData() { ChatId = Password.ChatId, CompanyId = Password.CompanyId, Message = Password.Message, MessageType = Password.MessageType, MessageDate = DateTime.Now, Id = Password.Id };
                            _telegramBotDataService.Update(PasswordUpdate);
                        }
                    }
                }
                else if (ReplyMessageFlag)
                {
                    if (message.ReplyToMessage.Text == "نام کاربری خود را وارد کنید 👱")
                    {
                        //_telegramBotDataService.Add(new TelegramBotData { ChatID = message.Chat.Id, CompanyId = user.Id, Message = message.Text, MessageDate = DateTime.Now, MessageType = "Username" });
                        await Api.SendTextMessageAsync(message.Chat.Id, text: "گذرواژه خود را وارد کنید 🔐", replyMarkup: markup);
                    }
                    else if (message.ReplyToMessage.Text == "گذرواژه خود را وارد کنید 🔐")
                    {
                        //_telegramBotDataService.Add(new TelegramBotData { ChatID = message.Chat.Id, CompanyId = user.Id, Message = message.Text, MessageDate = DateTime.Now, MessageType = "Password" });
                        var keyboard = new ReplyKeyboardMarkup(new[] {new []{new KeyboardButton("منوی کاربری 👱") }}, resizeKeyboard: true);
                        await Api.SendTextMessageAsync(message.Chat.Id, "جهت ورود به منوی کاربری ، گزینه مورد نظر را انتخاب کنید.",replyMarkup: keyboard);
                    }
                }
                #endregion
                #region Connection
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
                #endregion
                #region Last Online Time
                else if (message.Text == "آخرین اتصال 👱")
                {
                    //چک شود نام کاربری و رمز عبور اعتبار دارد
                    //بروزرسانی زمان در جدول موقت
                    //محاسبه آخرین اتصال و برگرداندن آن
                }
                #endregion
                #region Time Online
                else if (message.Text == "مدت اتصال 👱")
                {
                    //چک شود نام کاربری و رمز عبور اعتبار دارد
                    //بروزرسانی زمان در جدول موقت
                    //محاسبه مدت اتصال و برگرداندن آن
                }
                #endregion
                #region Time Remain
                else if (message.Text == "زمان باقیمانده 👱")
                {
                    //چک شود نام کاربری و رمز عبور اعتبار دارد
                    //بروزرسانی زمان در جدول موقت
                    //محاسبه زمان باقیمانده و برگرداندن آن
                }
                #endregion
                #region Time Validity Limit
                else if (message.Text == "اعتبار زمانی 👱")
                {
                    //چک شود نام کاربری و رمز عبور اعتبار دارد
                    //بروزرسانی زمان در جدول موقت
                    //محاسبه اعتبار زمانی و برگرداندن آن
                }
                #endregion
                #region Time Online Limit
                else if (message.Text == "محدودیت اتصال 👱")
                {
                    //چک شود نام کاربری و رمز عبور اعتبار دارد
                    //بروزرسانی زمان در جدول موقت
                    //محاسبه محدودیت اتصال و برگرداندن آن
                }
                #endregion
                #region Traffic
                else if (message.Text == "حجم 👱")
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
                        var UsermanUser = _mikrotikServices.Usermanager_GetUser(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, Username.Message);
                        if (_mikrotikServices.IP_Port_Check(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password) && _mikrotikServices.User_Pass_Check(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password))
                        {
                            if (UsermanUser != null)
                                if (UsermanUser.FirstOrDefault().username == Username.Message && UsermanUser.FirstOrDefault().password == Password.Message)
                                {
                                    _telegramBotDataService.Update(Username);
                                    _telegramBotDataService.Update(Password);
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
                                else
                                {
                                    _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, CompanyId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
                                    var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                                    await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور اشتباه می باشد.", replyMarkup: keyboard);
                                }
                            else
                            {
                                _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, CompanyId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
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
                #region Traffic All
                else if (message.Text == "حجم کل 👱")
                {
                    //چک شود نام کاربری و رمز عبور اعتبار دارد
                    //بروزرسانی زمان در جدول موقت
                    //محاسبه حجم کل و برگرداندن آن
                }
                #endregion
                #region Traffic Use
                else if (message.Text == "حجم مصرفی 👱")
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
                        var UsermanUser = _mikrotikServices.Usermanager_GetUser(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, Username.Message);
                        if (_mikrotikServices.IP_Port_Check(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password) && _mikrotikServices.User_Pass_Check(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password))
                        {
                            if (UsermanUser != null)
                                if (UsermanUser.FirstOrDefault().username == Username.Message && UsermanUser.FirstOrDefault().password == Password.Message)
                                {
                                    _telegramBotDataService.Update(Username);
                                    _telegramBotDataService.Update(Password);
                                    await Api.SendTextMessageAsync(
                                        message.Chat.Id,
                                        " میزان دانلود شما " + (ulong.Parse(UsermanUser.FirstOrDefault().download_used) / 1048576).ToString() + " مگابایت می باشد " + "\n" +
                                        " میزان آپلود شما " + (ulong.Parse(UsermanUser.FirstOrDefault().upload_used) / 1048576).ToString() + " مگابایت می باشد "
                                        );
                                }
                                else
                                {
                                    _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, CompanyId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
                                    var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                                    await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور اشتباه می باشد.", replyMarkup: keyboard);
                                }
                            else
                            {
                                _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, CompanyId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
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
                #region Traffic Remain
                else if (message.Text == "حجم باقیمانده 👱")
                {
                    //چک شود نام کاربری و رمز عبور اعتبار دارد
                    //بروزرسانی زمان در جدول موقت
                    //محاسبه حجم باقیمانده و برگرداندن آن
                }
                #endregion
                #region User Information
                else if (message.Text == "مشخصات کاربری 👱")
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
                        var UsermanUser = _mikrotikServices.Usermanager_GetUser(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, Username.Message);
                        if (_mikrotikServices.IP_Port_Check(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password) && _mikrotikServices.User_Pass_Check(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password))
                        {
                            if (UsermanUser != null)
                                if (UsermanUser.FirstOrDefault().username == Username.Message && UsermanUser.FirstOrDefault().password == Password.Message)
                                {
                                    _telegramBotDataService.Update(Username);
                                    _telegramBotDataService.Update(Password);
                                    await Api.SendTextMessageAsync(
                                        message.Chat.Id,
                                        "نام و نام خانوادگی: " + UsermanUser.FirstOrDefault().first_name + " " + UsermanUser.FirstOrDefault().last_name + "\n" +
                                        "نام کاربری: " + UsermanUser.FirstOrDefault().username + "\n" +
                                        "نام تعرفه: " + UsermanUser.FirstOrDefault().actual_profile + "\n"
                                        );
                                }
                                else
                                {
                                    _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, CompanyId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
                                    var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                                    await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور اشتباه می باشد.", replyMarkup: keyboard);
                                }
                            else
                            {
                                _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, CompanyId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
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
                else if (message.Text == "خروج 👱")
                {
                    var Username = TelegramBotDataTable.Where(x => x.MessageType == "Username").LastOrDefault();
                    var Password = TelegramBotDataTable.Where(x => x.MessageType == "Password").LastOrDefault();
                    _telegramBotDataService.Remove(Username);
                    _telegramBotDataService.Remove(Password);
                    _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, CompanyId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "Exit" });
                    var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                    await Api.SendTextMessageAsync(message.Chat.Id, "خروج موفقیت آمیر بود.", replyMarkup: keyboard);
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
                        if (_mikrotikServices.IP_Port_Check(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password) && _mikrotikServices.User_Pass_Check(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password))
                        {
                            #endregion
                            #region Find In Router
                            var UsermanUser = _mikrotikServices.Usermanager_GetUser(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, Username.Message);
                            if (UsermanUser != null)
                                if (UsermanUser.FirstOrDefault().username == Username.Message && UsermanUser.FirstOrDefault().password == Password.Message)
                                {
                                    #endregion
                                    #region Update DB
                                    _telegramBotDataService.Update(Username);
                                    _telegramBotDataService.Update(Password);
                                    #endregion
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
                                #region Error Response
                                else
                                {
                                    _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, CompanyId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
                                    var keyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("منوی اصلی") } }, resizeKeyboard: true);
                                    await Api.SendTextMessageAsync(message.Chat.Id, "نام کاربری یا رمز عبور اشتباه می باشد.", replyMarkup: keyboard);
                                }
                            else
                            {
                                _telegramBotDataService.Add(new TelegramBotData { ChatId = message.Chat.Id, CompanyId = user.Id, Message = "", MessageDate = DateTime.Now, MessageType = "WrongUserPass" });
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

                #region MainMenu
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
                    await Api.SendTextMessageAsync(message.Chat.Id, "به ربات تلگرامی " + CompanyCode + " خوش آمدید.لطفا یکی از گزینه ها را انتخاب کنید.",
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
                        new KeyboardButton("ورود مدیر 👷"),
                    }
                }, resizeKeyboard: true);
                    await Api.SendTextMessageAsync(message.Chat.Id, "به ربات تلگرامی " + CompanyCode + " خوش آمدید.لطفا یکی از گزینه ها را انتخاب کنید.",
                        replyMarkup: keyboard);
                }

                #endregion
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

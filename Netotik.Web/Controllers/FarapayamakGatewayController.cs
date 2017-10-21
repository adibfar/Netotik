using Netotik.Data;
using Netotik.Services.Abstract;
using Netotik.Services.Identity;
using Netotik.ViewModels.Identity.UserClient;
using Netotik.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Netotik.Web.Controllers
{
    public partial class FarapayamakGatewayController : BasePanelController
    {
        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IUnitOfWork _uow;
        private readonly ISmsService _smsService;

        public FarapayamakGatewayController(
            IMikrotikServices mikrotikServices,
            IApplicationUserManager applicationUserManager,
            ISmsService smsService,
            IUnitOfWork uow)
        {
            _mikrotikServices = mikrotikServices;
            _applicationUserManager = applicationUserManager;
            _smsService = smsService;
            _uow = uow;
        }
        #endregion
        // GET: FarapayamakGateway
        public virtual async Task<ActionResult> SmsReceive(string fromNum, string toNumber, string textMessage)
        {
            var User = await _applicationUserManager.FindByRouterSMSCodeAsync(textMessage);
            if (User == null)
                return View();
            if (User.UserRouter.SmsActive && User.UserRouter.RegisterWithSms && User.UserRouter.SmsCharge > 0)
            {
                if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                {
                    if (User.UserRouter.SmsIfErrorInSms)
                        _smsService.SendSms(User.PhoneNumber, "در هنگام ثبت نام پیامکی خطای اتصال به روتر ایجاد شد.", User.Id);
                    return View();
                }
                if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                {
                    if (User.UserRouter.SmsIfErrorInSms)
                        _smsService.SendSms(User.PhoneNumber, "در هنگام ثبت نام پیامکی خطای نام کاربری و رمز عبور ایجاد شد.", User.Id);
                    return View();
                }
                if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                {
                    if (User.UserRouter.SmsIfErrorInSms)
                        _smsService.SendSms(User.PhoneNumber, "در هنگام ثبت نام پیامکی خطای اتصال یوزرمنیجر ایجاد شد.", User.Id);
                    return View();
                }
                var Profiles = _mikrotikServices.Usermanager_GetAllProfile(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
                bool Flag = false;
                foreach (var profile in Profiles)
                    if (profile.name == User.UserRouter.RegisterWithSmsRouterProfile)
                        Flag = true;
                if (!Flag)
                {
                    if (User.UserRouter.SmsIfErrorInSms)
                        _smsService.SendSms(User.PhoneNumber, "در هنگام ثبت نام پیامکی خطای عدم وجود تعرفه انتخابی ایجاد شد.", User.Id);
                    return View();
                }

                var Rand = new Random();
                UserRegisterModel UserClient = new UserRegisterModel();
                UserClient.customer = User.UserRouter.Userman_Customer;
                UserClient.phone = fromNum;
                UserClient.username = fromNum;
                UserClient.password = Rand.Next(6).ToString();
                UserClient.profile = User.UserRouter.RegisterWithSmsRouterProfile;

                _mikrotikServices.Usermanager_UserCreate(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, UserClient);
                string SmsText = User.UserRouter.RegisterWithSmsMessage + " User: " + fromNum + " Pass: " + Rand;
                _smsService.SendSms(fromNum, SmsText, User.Id);
            }
            return View();
        }
    }
}
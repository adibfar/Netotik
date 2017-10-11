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
    public class FarapayamakGatewayController : BasePanelController
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
        public async Task<ActionResult> SmsRecive(string fromNum, string to, string text)
        {
            var User = await _applicationUserManager.FindByCompanySMSCodeAsync(text);
            if (User == null)
                return View();
            if (User.UserCompany.SmsActive && User.UserCompany.RegisterWithSms && User.UserCompany.SmsCharge > 0)
            {
                if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
                {
                    if (User.UserCompany.SmsIfErrorInSms)
                        _smsService.SendSms(User.PhoneNumber, "در هنگام ثبت نام پیامکی خطای اتصال به روتر ایجاد شد.", User.Id);
                    return View();
                }
                if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
                {
                    if (User.UserCompany.SmsIfErrorInSms)
                        _smsService.SendSms(User.PhoneNumber, "در هنگام ثبت نام پیامکی خطای نام کاربری و رمز عبور ایجاد شد.", User.Id);
                    return View();
                }
                if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
                {
                    if (User.UserCompany.SmsIfErrorInSms)
                        _smsService.SendSms(User.PhoneNumber, "در هنگام ثبت نام پیامکی خطای اتصال یوزرمنیجر ایجاد شد.", User.Id);
                    return View();
                }
                var Profiles = _mikrotikServices.Usermanager_GetAllProfile(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
                bool Flag = false;
                foreach (var profile in Profiles)
                    if (profile.name == User.UserCompany.RegisterWithSmsRouterProfile)
                        Flag = true;
                if (!Flag)
                {
                    if (User.UserCompany.SmsIfErrorInSms)
                        _smsService.SendSms(User.PhoneNumber, "در هنگام ثبت نام پیامکی خطای عدم وجود تعرفه انتخابی ایجاد شد.", User.Id);
                    return View();
                }

                var Rand = new Random();
                UserRegisterModel UserClient = new UserRegisterModel();
                UserClient.customer = User.UserCompany.Userman_Customer;
                UserClient.phone = fromNum;
                UserClient.username = fromNum;
                UserClient.password = Rand.Next(6).ToString();
                UserClient.profile = User.UserCompany.RegisterWithSmsRouterProfile;

                _mikrotikServices.Usermanager_UserCreate(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, UserClient);
                string SmsText = User.UserCompany.RegisterWithSmsMessage + " User: " + fromNum + " Pass: " + Rand;
                _smsService.SendSms(fromNum, SmsText, User.Id);
            }
            return View();
        }
    }
}
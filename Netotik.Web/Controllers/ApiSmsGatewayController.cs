using Netotik.Data;
using Netotik.IocConfig;
using Netotik.Resources;
using Netotik.Services.Abstract;
using Netotik.Services.Identity;
using Netotik.ViewModels.Identity.UserClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Results;

namespace Netotik.Web.Controllers
{
    public class ApiSmsGatewayController : ApiController
    {
        private IApplicationUserManager _applicationUserManager;
        private IMikrotikServices _mikrotikServices;
        private IUnitOfWork _uow;
        private ISmsService _smsService;

        [HttpGet]
        [Route(@"api/SmsGateway/Farapayamak/{from}/{to}")]
        public async Task<string> Farapayamak(string from, string to, string text)
        {

            _uow = ProjectObjectFactory.Container.GetInstance<IUnitOfWork>();
            _applicationUserManager = ProjectObjectFactory.Container.GetInstance<IApplicationUserManager>();
            _mikrotikServices = ProjectObjectFactory.Container.GetInstance<IMikrotikServices>();
            _smsService = ProjectObjectFactory.Container.GetInstance<ISmsService>();

            var User = await _applicationUserManager.FindByRouterSMSCodeAsync(text.Replace("ي", "ی"));
            if (User == null)
                return "ok....";

            if (User.UserRouter.SmsActive && User.UserRouter.RegisterWithSms && User.UserRouter.SmsCharge > 0)
            {
                if (!_mikrotikServices.IP_Port_Check(User.UserRouter.R_Host, User.UserRouter.R_Port, User.UserRouter.R_User, User.UserRouter.R_Password))
                {
                    if (User.UserRouter.SmsIfErrorInSms)
                        _smsService.SendSms(User.PhoneNumber, Captions.ClientWithSmsError, User.Id);
                    return "ok.";
                }
                if (!_mikrotikServices.User_Pass_Check(User.UserRouter.R_Host, User.UserRouter.R_Port, User.UserRouter.R_User, User.UserRouter.R_Password))
                {
                    if (User.UserRouter.SmsIfErrorInSms)
                        _smsService.SendSms(User.PhoneNumber, Captions.ClientRegisterWithSmsUserPassError, User.Id);
                    return "ok..";
                }
                if (!_mikrotikServices.Usermanager_IsInstall(User.UserRouter.R_Host, User.UserRouter.R_Port, User.UserRouter.R_User, User.UserRouter.R_Password))
                {
                    if (User.UserRouter.SmsIfErrorInSms)
                        _smsService.SendSms(User.PhoneNumber, Captions.ClientRegisterWithSmsUsermanagerError, User.Id);
                    return "ok...";
                }

                //var Profiles = _mikrotikServices.Usermanager_GetAllProfile(User.UserRouter.R_Host, User.UserRouter.R_Port, User.UserRouter.R_User, User.UserRouter.R_Password);
                //bool Flag = false;
                //foreach (var profile in Profiles)
                //    if (profile.name == User.UserRouter.RegisterWithSmsRouterProfile)
                //        Flag = true;
                //if (!Flag)
                //{
                //    if (User.UserRouter.SmsIfErrorInSms)
                //        _smsService.SendSms(User.PhoneNumber, "در هنگام ثبت نام پیامکی خطای عدم وجود تعرفه انتخابی ایجاد شد.", User.Id);
                //    return "ok......";
                //}

                var Rand = new Random();
                UserRegisterModel UserClient = new UserRegisterModel();
                UserClient.customer = User.UserRouter.Userman_Customer;
                UserClient.phone = from;
                UserClient.username = from;
                UserClient.password = Rand.Next(10000, 99999).ToString();
                UserClient.profile = User.UserRouter.RegisterWithSmsRouterProfile;

                if (!_mikrotikServices.Usermanager_IsUserExist(User.UserRouter.R_Host, User.UserRouter.R_Port, User.UserRouter.R_User, User.UserRouter.R_Password, UserClient.username))
                {
                    _mikrotikServices.Usermanager_UserCreate(User.UserRouter.R_Host, User.UserRouter.R_Port, User.UserRouter.R_User, User.UserRouter.R_Password, UserClient);
                    string SmsText = User.UserRouter.RegisterWithSmsMessage + "\n User: " + UserClient.username + "\n Pass: " + UserClient.password;
                    _smsService.SendSms(from, SmsText, User.Id);
                    _uow.SaveAllChanges();
                }
                else
                {
                    var usermanuser = _mikrotikServices.Usermanager_GetUser(User.UserRouter.R_Host, User.UserRouter.R_Port, User.UserRouter.R_User, User.UserRouter.R_Password, UserClient.username);
                    if (Convert.ToDateTime((usermanuser.FirstOrDefault().EditDate.Value.Year == 1 ? usermanuser.FirstOrDefault().CreateDate : usermanuser.FirstOrDefault().EditDate)).AddHours(User.UserRouter.RegisterWithSmsAgainHour) <= DateTime.Now)
                    {
                        _mikrotikServices.Usermanager_ResetUserProfiles(User.UserRouter.R_Host, User.UserRouter.R_Port, User.UserRouter.R_User, User.UserRouter.R_Password, UserClient.username);
                        var UserEditmodel = new UserEditModel()
                        {
                            Age = usermanuser.FirstOrDefault().Age,
                            Birthday = usermanuser.FirstOrDefault().Birthday,
                            CreateDate = usermanuser.FirstOrDefault().CreateDate,
                            IsMale = usermanuser.FirstOrDefault().IsMale,
                            MarriageDate = usermanuser.FirstOrDefault().MarriageDate,
                            NationalCode = usermanuser.FirstOrDefault().NationalCode,
                            profile = UserClient.profile,
                            password = UserClient.password,
                            id = usermanuser.FirstOrDefault().id,
                            username = UserClient.username
                        };
                        _mikrotikServices.Usermanager_UserEdit(User.UserRouter.R_Host, User.UserRouter.R_Port, User.UserRouter.R_User, User.UserRouter.R_Password, UserEditmodel);
                        string SmsText = User.UserRouter.RegisterWithSmsMessage + "\n User: " + UserClient.username + "\n Pass: " + UserClient.password;
                        _smsService.SendSms(from, SmsText, User.Id);
                        _uow.SaveAllChanges();
                    }
                }
            }
            return "ok.......";

        }
    }
}

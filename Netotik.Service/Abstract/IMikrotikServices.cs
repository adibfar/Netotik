using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.ViewModels.Mikrotik;

namespace Netotik.Services.Abstract
{
    public interface IMikrotikServices
    {
        string GetRouterName(string ip,int port,string user, string pass);
        List<UsermanagerUser> GetAllUserManagerUsers(string ip, int port, string user, string pass);
        List<UsermanProfile> ProfileToDic(List<string> resault);
        List<UsermanProfileLimition> ProfileLimitationToDic(List<string> resault);
        List<UsermanLimition> LimitionToDic(List<string> resault);
        void DisableUsermanUser(string ip, int port, string user, string pass,string UsermanUser);
        List<UsermanagerUserSession> UsermanUserSession(string ip, int port, string user, string pass, string UsermanUser);
        void RemoveUsermanUser(string ip, int port, string user, string pass, string UsermanUser);
        void RemoveUsermanprofile(string ip, int port, string user, string pass, string UsermanProfile);
        void EnableUsermanUser(string ip, int port, string user, string pass, string UsermanUser);
        List<UsermanagerUser> UserToDic(List<string> resault);
        Boolean IsUsermangerInstall(string ip, int port, string user, string pass);
        Boolean IsUsermanUserExist(string ip, int port, string user, string pass,string username);
        
        List<UsermanProfile> GetAllProfile(string ip, int port, string user, string pass);
        List<UsermanagerCustomer> GetAllCustomers(string ip, int port, string user, string pass);
        
        List<UsermanProfileLimition> GetAllProfileLimition(string ip, int port, string user, string pass);
        List<UsermanLimition> GetAllLimition(string ip, int port, string user, string pass);
        void UsermanUserCreate(string ip, int port, string user, string pass, UsermanagerUserRegister usermanuser);
        void UsermanProfileCreate(string ip, int port, string user, string pass, UsermanProfileLimitionCreate usermanProfile);
        bool IsUsermanProfileExist(string ip, int port, string user, string pass, UsermanProfileLimitionCreate usermanProfile);
        List<RouterInterface> Interface(string ip, int port, string user, string pass);
        bool IP_Port_Check(string ip, int port, string user, string pass);
        bool User_Pass_Check(string ip, int port, string user, string pass);
        void Router_Info_Update(string ip, int port, string user, string pass);
        string EnableAndGetCloud(string ip, int port, string user, string pass);
        void UsermanUserEdit(string ip, int port, string user, string pass, UsermanagerUserEdit model);
        bool CheckMeliCode(string PersonCode, int? id);
    }
}

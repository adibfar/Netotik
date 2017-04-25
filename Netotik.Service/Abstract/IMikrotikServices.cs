using System;
using System.Collections.Generic;
using Netotik.ViewModels.Mikrotik;

namespace Netotik.Services.Abstract
{
    public interface IMikrotikServices
    {
        string GetRouterName(string ip, int port, string user, string pass);
        List<Usermanager_UserModel> Usermanager_GetAllUsers(string ip, int port, string user, string pass);
        void Usermanager_DisableUser(string ip, int port, string user, string pass, string UsermanUser);
        List<Usermanager_UserSessionModel> Usermanager_UserSession(string ip, int port, string user, string pass, string UsermanUser);
        void Usermanager_RemoveUser(string ip, int port, string user, string pass, string UsermanUser);
        void Usermanager_RemoveProfile(string ip, int port, string user, string pass, string UsermanProfile);
        void Usermanager_EnableUser(string ip, int port, string user, string pass, string UsermanUser);
        Boolean Usermanager_IsInstall(string ip, int port, string user, string pass);
        Boolean Usermanager_IsUserExist(string ip, int port, string user, string pass, string username);
        List<Usermanager_ProfileModel> Usermanager_GetAllProfile(string ip, int port, string user, string pass);
        List<Usermanager_CustomerModel> Usermanager_GetAllCustomers(string ip, int port, string user, string pass);
        List<Usermanager_ProfileLimitionModel> Usermanager_GetAllProfileLimition(string ip, int port, string user, string pass);
        List<Usermanager_LimitionModel> Usermanager_GetAllLimition(string ip, int port, string user, string pass);
        void Usermanager_UserCreate(string ip, int port, string user, string pass, Usermanager_UserRegisterModel usermanuser);
        void Usermanager_ProfileCreate(string ip, int port, string user, string pass, Usermanager_ProfileLimitionCreateModel usermanProfile);
        bool Usermanager_IsProfileExist(string ip, int port, string user, string pass, Usermanager_ProfileLimitionCreateModel usermanProfile);
        List<Router_InterfaceModel> Interface(string ip, int port, string user, string pass);
        bool IP_Port_Check(string ip, int port, string user, string pass);
        bool User_Pass_Check(string ip, int port, string user, string pass);
        void Usermanager_ResetCounter(string r_Host, int r_Port, string r_User, string r_Password, string user);
        void Router_Info_Update(string ip, int port, string user, string pass);
        string EnableAndGetCloud(string ip, int port, string user, string pass);
        void Usermanager_UserEdit(string ip, int port, string user, string pass, Usermanager_UserEditModel model);
        bool CheckMeliCode(string PersonCode, int? id);
        void Usermanager_CloseSession(string r_Host, int r_Port, string r_User, string r_Password, string user);
        List<Router_PackageUpdateModel> Router_PackageUpdate(string r_Host, int r_Port, string r_User, string r_Password);
        List<Router_ClockModel> Router_Clock(string r_Host, int r_Port, string r_User, string r_Password);
        List<Router_RouterBoardModel> Router_Routerboard(string r_Host, int r_Port, string r_User, string r_Password);
        List<Router_IdentityModel> Router_Identity(string r_Host, int r_Port, string r_User, string r_Password);
        List<Router_LicenseModel> Router_License(string r_Host, int r_Port, string r_User, string r_Password);
        List<Router_ResourceModel> Router_Resource(string r_Host, int r_Port, string r_User, string r_Password);
        void Router_InterfaceEnable(string r_Host, int r_Port, string r_User, string r_Password, string id);
        void Router_InterfaceDisable(string r_Host, int r_Port, string r_User, string r_Password, string id);
        Router_WirelessModel GetWirelessDetails(string r_Host, int r_Port, string r_User, string r_Password, string id);
        Router_EthernetModel GetEthernetDetails(string r_Host, int r_Port, string r_User, string r_Password, string id);

    }
}

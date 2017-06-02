using System;
using System.Collections.Generic;
using Netotik.ViewModels.Mikrotik;

namespace Netotik.Services.Abstract
{
    public interface IMikrotikServices
    {
        string GetRouterName(string ip, int port, string user, string pass);
        List<Usermanager_UserModel> Usermanager_GetAllUsers(string ip, int port, string user, string pass);
        List<Usermanager_UserModel> Usermanager_GetUser(string ip, int port, string user, string pass,string id);
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
        bool RebootRouter(string ip, int port, string user, string pass);
        bool ResetRouter(string ip, int port, string user, string pass, bool keepusers, bool nosettings);
        bool BackupRouter(string ip, int port, string user, string pass);
        bool BackupUsermanager(string ip, int port, string user, string pass);
        bool RemoveLogs(string ip, int port, string user, string pass);
        bool ResetUsermanager(string ip, int port, string user, string pass, bool users, bool logs,bool session,bool history,bool packages,bool db);
        bool User_Pass_Check(string ip, int port, string user, string pass);
        void Usermanager_ResetCounter(string r_Host, int r_Port, string r_User, string r_Password, string user);
        void Router_Info_Update(string ip, int port, string user, string pass);
        string EnableAndGetCloud(string ip, int port, string user, string pass);
        void Usermanager_UserEdit(string ip, int port, string user, string pass, Usermanager_UserEditModel model);
        void Hotspot_IpWalledGardenAdd(string r_Host, int r_Port, string r_User, string r_Password, Hotspot_IPWalledGardenModel temp);
        void Hotspot_IpBindingsAdd(string r_Host, int r_Port, string r_User, string r_Password, Hotspot_IPBindingsModel temp);
        bool CheckMeliCode(string PersonCode, int? id);
        void Usermanager_CloseSession(string r_Host, int r_Port, string r_User, string r_Password, string user);
        List<Router_PackageUpdateModel> Router_PackageUpdate(string r_Host, int r_Port, string r_User, string r_Password);
        List<Router_ClockModel> Router_Clock(string r_Host, int r_Port, string r_User, string r_Password);
        List<Router_RouterBoardModel> Router_Routerboard(string r_Host, int r_Port, string r_User, string r_Password);
        List<Router_IdentityModel> Router_Identity(string r_Host, int r_Port, string r_User, string r_Password);
        List<Hotspot_IPBindingsModel> Hotspot_IpBindings(string r_Host, int r_Port, string r_User, string r_Password);
        List<Hotspot_IPWalledGardenModel> Hotspot_IpWalledGarden(string r_Host, int r_Port, string r_User, string r_Password);
        List<Hotspot_WalledGardenModel> Hotspot_WalledGarden(string r_Host, int r_Port, string r_User, string r_Password);
        List<Router_LicenseModel> Router_License(string r_Host, int r_Port, string r_User, string r_Password);
        List<Router_ResourceModel> Router_Resource(string r_Host, int r_Port, string r_User, string r_Password);
        void Router_InterfaceEnable(string r_Host, int r_Port, string r_User, string r_Password, string id);
        void Router_InterfaceDisable(string r_Host, int r_Port, string r_User, string r_Password, string id);
        Router_WirelessModel GetWirelessDetails(string r_Host, int r_Port, string r_User, string r_Password, string id);
        Router_EthernetModel GetEthernetDetails(string r_Host, int r_Port, string r_User, string r_Password, string id);
        List<Router_FileModel> GetBackupRouterList(string r_Host, int r_Port, string r_User, string r_Password);
        List<Router_FileModel> GetBackupUsermanagerList(string r_Host, int r_Port, string r_User, string r_Password);
        List<Hotspot_ServerModel> Hotspot_ServersList(string r_Host, int r_Port, string r_User, string r_Password);
        void RestoreRouter(string r_Host, int r_Port, string r_User, string r_Password, string FileName);
        void RestoreUsermanager(string r_Host, int r_Port, string r_User, string r_Password, string FileName);

        void Hotspot_IpBindingsRemove(string ip, int port, string user, string pass, string id);
        void Hotspot_IpBindingsEnable(string ip, int port, string user, string pass, string id);
        void Hotspot_IpBindingsDisable(string ip, int port, string user, string pass, string id);

        void Hotspot_IpWalledGardenRemove(string ip, int port, string user, string pass, string id);
        void Hotspot_IpWalledGardenEnable(string ip, int port, string user, string pass, string id);
        void Hotspot_IpWalledGardenDisable(string ip, int port, string user, string pass, string id);
    }
}

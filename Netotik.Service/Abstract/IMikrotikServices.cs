using System;
using System.Collections.Generic;
using Netotik.ViewModels.Mikrotik;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface IMikrotikServices
    {
        string GetRouterName(string ip, int port, string user, string pass);
        List<Netotik.ViewModels.Identity.UserClient.UserModel> Usermanager_GetAllUsers(string ip, int port, string user, string pass);
        long Usermanager_GetUsersCount(string ip, int port, string user, string pass);
        long Usermanager_GetPackagesCount(string ip, int port, string user, string pass);
        long Usermanager_GetActiveSessionsCount(string ip, int port, string user, string pass);
        List<Netotik.ViewModels.Identity.UserClient.UserSessionModel> Usermanager_GetActiveSessions(string r_Host, int r_Port, string r_User, string r_Password);
        List<Netotik.ViewModels.Identity.UserClient.UserModel> Usermanager_GetUser(string ip, int port, string user, string pass,string id);
        Task<List<Netotik.ViewModels.Identity.UserClient.UserModel>> Usermanager_GetUserAsync(string ip, int port, string user, string pass, string id);
        void Usermanager_DisableUser(string ip, int port, string user, string pass, string UsermanUser);
        List<Netotik.ViewModels.Identity.UserClient.UserSessionModel> Usermanager_UserSession(string ip, int port, string user, string pass, string UsermanUser);
        List<Netotik.ViewModels.Identity.UserClient.UserSessionModel> Usermanager_GetAllUsersSessions(string ip, int port, string user, string pass);

        List<Netotik.ViewModels.Identity.UserClient.UserSessionModel> Usermanager_GetAllUsersSessions(string ip, int port, string user, string pass,DateTime From,DateTime To);
        List<Netotik.ViewModels.Identity.UserClient.PaymentModel> Usermanager_Payment(string ip, int port, string user, string pass, string UsermanUser);
        void Usermanager_RemoveUser(string ip, int port, string user, string pass, string UsermanUser);
        void Usermanager_ResetUserProfiles(string ip, int port, string user, string pass, string UsermanUser);
        void Usermanager_RemoveProfile(string ip, int port, string user, string pass, string UsermanProfile);
        void Usermanager_EnableUser(string ip, int port, string user, string pass, string UsermanUser);
        Boolean Usermanager_IsInstall(string ip, int port, string user, string pass);
        Boolean Usermanager_IsUserExist(string ip, int port, string user, string pass, string username);
        List<Netotik.ViewModels.Identity.UserClient.ProfileModel> Usermanager_GetAllProfile(string ip, int port, string user, string pass);
        List<Netotik.ViewModels.Identity.UserClient.CustomerModel> Usermanager_GetAllCustomers(string ip, int port, string user, string pass);
        List<Netotik.ViewModels.Identity.UserClient.ProfileLimitionModel> Usermanager_GetAllProfileLimition(string ip, int port, string user, string pass);
        List<Netotik.ViewModels.Identity.UserClient.LimitionModel> Usermanager_GetAllLimition(string ip, int port, string user, string pass);
        void Usermanager_UserCreate(string ip, int port, string user, string pass, Netotik.ViewModels.Identity.UserClient.UserRegisterModel usermanuser);
        void Usermanager_ProfileCreate(string ip, int port, string user, string pass, Netotik.ViewModels.Identity.UserClient.ProfileLimitionCreateModel usermanProfile);
        bool Usermanager_IsProfileExist(string ip, int port, string user, string pass, Netotik.ViewModels.Identity.UserClient.ProfileLimitionCreateModel usermanProfile);
        List<Router_InterfaceModel> Interface(string ip, int port, string user, string pass);
        bool IP_Port_Check(string ip, int port, string user, string pass);
        Task<bool> IP_Port_CheckAsync(string ip, int port, string user, string pass);
        bool RebootRouter(string ip, int port, string user, string pass);
        bool ResetRouter(string ip, int port, string user, string pass, bool keepusers, bool nosettings);
        bool BackupRouter(string ip, int port, string user, string pass);
        bool BackupUsermanager(string ip, int port, string user, string pass);
        bool RemoveLogs(string ip, int port, string user, string pass);
        bool ResetUsermanager(string ip, int port, string user, string pass, bool users, bool logs,bool session,bool history,bool packages,bool db);
        bool User_Pass_Check(string ip, int port, string user, string pass);
        Task<bool> User_Pass_CheckAsync(string ip, int port, string user, string pass);
        void Usermanager_ResetCounter(string r_Host, int r_Port, string r_User, string r_Password, string user);
        void Router_Info_Update(string ip, int port, string user, string pass);
        void DisableApiLoging(string r_Host, int r_Port, string r_User, string r_Password);
        string EnableAndGetCloud(string ip, int port, string user, string pass);
        void Usermanager_UserEdit(string ip, int port, string user, string pass, Netotik.ViewModels.Identity.UserClient.UserEditModel model);
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
        bool IsIpServiceEnable(string r_Host, int r_Port, string r_User, string r_Password, string v);
        Router_WirelessModel GetWirelessDetails(string r_Host, int r_Port, string r_User, string r_Password, string id);
        Router_EthernetModel GetEthernetDetails(string r_Host, int r_Port, string r_User, string r_Password, string id);
        List<Router_FileModel> GetBackupRouterList(string r_Host, int r_Port, string r_User, string r_Password);
        List<Router_FileModel> GetBackupUsermanagerList(string r_Host, int r_Port, string r_User, string r_Password);
        List<Hotspot_ServerModel> Hotspot_ServersList(string r_Host, int r_Port, string r_User, string r_Password);
        List<Hotspot_UsersModel> Hotspot_UsersList(string r_Host, int r_Port, string r_User, string r_Password);
        List<Hotspot_ActiveModel> Hotspot_ActiveList(string r_Host, int r_Port, string r_User, string r_Password);
        
        void RestoreRouter(string r_Host, int r_Port, string r_User, string r_Password, string FileName);
        void RestoreUsermanager(string r_Host, int r_Port, string r_User, string r_Password, string FileName);

        void Hotspot_IpBindingsRemove(string ip, int port, string user, string pass, string id);
        void Hotspot_IpBindingsEnable(string ip, int port, string user, string pass, string id);
        void Hotspot_IpBindingsDisable(string ip, int port, string user, string pass, string id);

        void Hotspot_IpWalledGardenRemove(string ip, int port, string user, string pass, string id);
        void Hotspot_IpWalledGardenEnable(string ip, int port, string user, string pass, string id);
        void Hotspot_IpWalledGardenDisable(string ip, int port, string user, string pass, string id);

        void Router_NatAdd(string ip, int port, string user, string pass, Router_NatModel model);
        List<Router_NatModel> Router_NatList(string r_Host, int r_Port, string r_User, string r_Password);
        void EnableIpService(string r_Host, int r_Port, string r_User, string r_Password, string ServiceName);
        void Router_NatDisable(string ip, int port, string user, string pass, string id);
        void Router_NatEnable(string ip, int port, string user, string pass, string id);
        void Router_NatRemove(string ip, int port, string user, string pass, string id);

        bool Usermanager_UserChangePassword(string ip, int port, string user, string pass, Netotik.ViewModels.Identity.UserClient.ChangePasswordModel model,string id);
        void Usermanager_UserRegKey(string ip, int port, string user, string pass, string UsermanUser , string RegKey);
        void Usermanager_UserRegDayte(string ip, int port, string user, string pass, string UsermanUser , string RegDate);
        List<Usermanager_LogModel> Usermanager_GetAllLogs(string r_Host, int r_Port, string r_User, string r_Password);
        void DisableIpService(string r_Host, int r_Port, string r_User, string r_Password, string ServiceName);
        void RemoveFile(string r_Host, int r_Port, string r_User, string r_Password, string v);
        bool FileExist(string r_Host, int r_Port, string r_User, string r_Password, string FileName);
        void CopyFileToFtp(string r_Host, int r_Port, string r_User, string r_Password, string FtpIP, string FtpPort, string FtpUser, string FtpPass, string SrcFile, string DstFile);
        int GetIpServicePortNumber(string r_Host, int r_Port, string r_User, string r_Password, string ServiceName);
        Task FetchUrlsAsync(string r_Host, int r_Port, string r_User, string r_Password, long id, List<FetchModel> fetch);
        void ChangeHotspotFolder(string r_Host, int r_Port, string r_User, string r_Password, string DirectoryName ,string ServerName);
        void SetDefaultNtpServers(string r_Host, int r_Port, string r_User, string r_Password);
        //void RouterAccessAdd(string r_Host, int r_Port, string r_User, string r_Password,RouterAccessModel Model);
        //List<RouterAccessModel> RouterAccessList(string r_Host, int r_Port, string r_User, string r_Password);
    }
}

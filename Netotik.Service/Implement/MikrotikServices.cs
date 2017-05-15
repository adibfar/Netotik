using Netotik.Services.Abstract;
using Netotik.Common.MikrotikAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using Netotik.ViewModels.Mikrotik;
using PersianDate;

namespace Netotik.Services.Implement
{
    public class MikrotikServices : IMikrotikServices
    {
        #region userman
        public void Usermanager_ResetCounter(string r_Host, int r_Port, string r_User, string r_Password, string user)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(r_Host, r_Port);
            if (!mikrotik.Login(r_User, r_Password)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/user/reset-counters");
            string temp = String.Format("=.id={0}", user);
            mikrotik.Send(temp, true);
        }
        public void Usermanager_CloseSession(string r_Host, int r_Port, string r_User, string r_Password, string user)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(r_Host, r_Port);
            if (!mikrotik.Login(r_User, r_Password)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/session/close-session");
            string temp = String.Format("=.id={0}", user);
            mikrotik.Send(temp, true);
        }
        public List<Usermanager_ProfileModel> Usermanager_GetAllProfile(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/profile/print", true);

            var profilemodel = new List<Usermanager_ProfileModel>();
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }

                    profilemodel.Add(new Usermanager_ProfileModel()
                    {
                        id = ColumnList.Any(x => x.Key == ".id") ? (ColumnList.FirstOrDefault(x => x.Key == ".id").Value) : "",
                        name = ColumnList.Any(x => x.Key == "name") ? (ColumnList.FirstOrDefault(x => x.Key == "name").Value) : "",
                        name_for_users = ColumnList.Any(x => x.Key == "name-for-users") ? (ColumnList.FirstOrDefault(x => x.Key == "name-for-users").Value) : "",
                        override_shared_users = ColumnList.Any(x => x.Key == "override-shared-users") ? (ColumnList.FirstOrDefault(x => x.Key == "override-shared-users").Value) : "",
                        owner = ColumnList.Any(x => x.Key == "owner") ? (ColumnList.FirstOrDefault(x => x.Key == "owner").Value) : "",
                        price = ColumnList.Any(x => x.Key == "price") ? (ColumnList.FirstOrDefault(x => x.Key == "price").Value) : "",
                        starts_at = ColumnList.Any(x => x.Key == "starts-at") ? (ColumnList.FirstOrDefault(x => x.Key == "starts-at").Value) : "",
                        validity = ColumnList.Any(x => x.Key == "validity") ? (ColumnList.FirstOrDefault(x => x.Key == "validity").Value) : "",
                    });
                }
            }

            return profilemodel;
        }
        public List<Usermanager_CustomerModel> Usermanager_GetAllCustomers(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/customer/print", true);

            var customermodel = new List<Usermanager_CustomerModel>();
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }

                    customermodel.Add(new Usermanager_CustomerModel()
                    {
                        id = ColumnList.Any(x => x.Key == ".id") ? (ColumnList.FirstOrDefault(x => x.Key == ".id").Value) : "",
                        login = ColumnList.Any(x => x.Key == "login") ? (ColumnList.FirstOrDefault(x => x.Key == "login").Value) : "",
                    });
                }
            }
            return customermodel;
        }
        public List<Usermanager_ProfileLimitionModel> Usermanager_GetAllProfileLimition(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/profile/profile-limitation/print", true);

            var profilemodel = new List<Usermanager_ProfileLimitionModel>();
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }

                    profilemodel.Add(new Usermanager_ProfileLimitionModel()
                    {
                        id = ColumnList.Any(x => x.Key == ".id") ? (ColumnList.FirstOrDefault(x => x.Key == ".id").Value) : "",
                        profile = ColumnList.Any(x => x.Key == "profile") ? (ColumnList.FirstOrDefault(x => x.Key == "profile").Value) : "",
                        from_time = ColumnList.Any(x => x.Key == "from-time") ? (ColumnList.FirstOrDefault(x => x.Key == "from-time").Value) : "",
                        limitation = ColumnList.Any(x => x.Key == "limitation") ? (ColumnList.FirstOrDefault(x => x.Key == "limitation").Value) : "",
                        till_time = ColumnList.Any(x => x.Key == "till-time") ? (ColumnList.FirstOrDefault(x => x.Key == "till-time").Value) : "",
                        weekdays = ColumnList.Any(x => x.Key == "weekdays") ? (ColumnList.FirstOrDefault(x => x.Key == "weekdays").Value) : "",
                    });
                }
            }
            return profilemodel;
        }
        public List<Usermanager_LimitionModel> Usermanager_GetAllLimition(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/profile/limitation/print", true);

            var profilemodel = new List<Usermanager_LimitionModel>();
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }

                    profilemodel.Add(new Usermanager_LimitionModel()
                    {
                        id = ColumnList.Any(x => x.Key == ".id") ? (ColumnList.FirstOrDefault(x => x.Key == ".id").Value) : "",
                        address_list = ColumnList.Any(x => x.Key == "address-list") ? (ColumnList.FirstOrDefault(x => x.Key == "address-list").Value) : "",
                        download_limit = ColumnList.Any(x => x.Key == "download-limit") ? (ColumnList.FirstOrDefault(x => x.Key == "download-limit").Value) : "",
                        group_name = ColumnList.Any(x => x.Key == "group-name") ? (ColumnList.FirstOrDefault(x => x.Key == "group-name").Value) : "",
                        ip_pool = ColumnList.Any(x => x.Key == "ip_pool") ? (ColumnList.FirstOrDefault(x => x.Key == "ip_pool").Value) : "",
                        name = ColumnList.Any(x => x.Key == "name") ? (ColumnList.FirstOrDefault(x => x.Key == "name").Value) : "",
                        owner = ColumnList.Any(x => x.Key == "owner") ? (ColumnList.FirstOrDefault(x => x.Key == "owner").Value) : "",
                        rate_limit_min_tx = ColumnList.Any(x => x.Key == "rate-limit-min-tx") ? (ColumnList.FirstOrDefault(x => x.Key == "rate-limit-min-tx").Value) : "",
                        rate_limit_rx = ColumnList.Any(x => x.Key == "rate-limit-rx") ? (ColumnList.FirstOrDefault(x => x.Key == "rate-limit-rx").Value) : "",
                        rate_limit_tx = ColumnList.Any(x => x.Key == "rate-limit-tx") ? (ColumnList.FirstOrDefault(x => x.Key == "rate-limit-tx").Value) : "",
                        transfer_limit = ColumnList.Any(x => x.Key == "transfer-limit") ? (ColumnList.FirstOrDefault(x => x.Key == "transfer-limit").Value) : "",
                        upload_limit = ColumnList.Any(x => x.Key == "upload-limit") ? (ColumnList.FirstOrDefault(x => x.Key == "upload-limit").Value) : "",
                        uptime_limit = ColumnList.Any(x => x.Key == "uptime-limit") ? (ColumnList.FirstOrDefault(x => x.Key == "uptime-limit").Value) : "",
                    });
                }
            }
            return profilemodel;
        }
        public Boolean Usermanager_IsInstall(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-------------------------------------------------
            mikrotik.Send("/system/package/print", true);

            foreach (var item in mikrotik.Read())
            {
                try
                {
                    if (item.Split('=')[4] == "user-manager" && item.Split('=')[12] == "false") return true;
                }
                catch { }
            }

            return false;
        }
        public List<Usermanager_UserModel> Usermanager_GetAllUsers(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/user/print", true);

            var usermodel = new List<Usermanager_UserModel>();
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }

                    usermodel.Add(new Usermanager_UserModel()
                    {
                        id = ColumnList.Any(x => x.Key == ".id") ? (ColumnList.FirstOrDefault(x => x.Key == ".id").Value) : "",
                        customer = ColumnList.Any(x => x.Key == "customer") ? (ColumnList.FirstOrDefault(x => x.Key == "customer").Value) : "",
                        actual_profile = ColumnList.Any(x => x.Key == "actual-profile") ? (ColumnList.FirstOrDefault(x => x.Key == "actual-profile").Value) : "",
                        username = ColumnList.Any(x => x.Key == "username") ? (ColumnList.FirstOrDefault(x => x.Key == "username").Value) : "",
                        password = ColumnList.Any(x => x.Key == "password") ? (ColumnList.FirstOrDefault(x => x.Key == "password").Value) : "",
                        caller_id = ColumnList.Any(x => x.Key == "caller-id") ? (ColumnList.FirstOrDefault(x => x.Key == "caller-id").Value) : "",
                        first_name = ColumnList.Any(x => x.Key == "first-name") ? (ColumnList.FirstOrDefault(x => x.Key == "first-name").Value) : "",
                        last_name = ColumnList.Any(x => x.Key == "last-name") ? (ColumnList.FirstOrDefault(x => x.Key == "last-name").Value) : "",
                        phone = ColumnList.Any(x => x.Key == "phone") ? (ColumnList.FirstOrDefault(x => x.Key == "phone").Value) : "",
                        location = ColumnList.Any(x => x.Key == "location") ? (ColumnList.FirstOrDefault(x => x.Key == "location").Value) : "",
                        email = ColumnList.Any(x => x.Key == "email") ? (ColumnList.FirstOrDefault(x => x.Key == "email").Value) : "",
                        ip_address = ColumnList.Any(x => x.Key == "ip-address") ? (ColumnList.FirstOrDefault(x => x.Key == "ip-address").Value) : "",
                        shared_users = ColumnList.Any(x => x.Key == "shared-users") ? (ColumnList.FirstOrDefault(x => x.Key == "shared-users").Value) : "",
                        wireless_psk = ColumnList.Any(x => x.Key == "wireless-psk") ? (ColumnList.FirstOrDefault(x => x.Key == "wireless-psk").Value) : "",
                        wireless_enc_key = ColumnList.Any(x => x.Key == "wireless-enc-key") ? (ColumnList.FirstOrDefault(x => x.Key == "wireless-enc-key").Value) : "",
                        wireless_enc_algo = ColumnList.Any(x => x.Key == "wireless-enc-algo") ? (ColumnList.FirstOrDefault(x => x.Key == "wireless-enc-algo").Value) : "",
                        last_seen = ColumnList.Any(x => x.Key == "last-seen") ? (ColumnList.FirstOrDefault(x => x.Key == "last-seen").Value) : "",
                        active = ColumnList.Any(x => x.Key == "active") ? (ColumnList.FirstOrDefault(x => x.Key == "active").Value) : "",
                        incomplete = ColumnList.Any(x => x.Key == "incomplete") ? (ColumnList.FirstOrDefault(x => x.Key == "incomplete").Value) : "",
                        disabled = ColumnList.Any(x => x.Key == "disabled") ? (ColumnList.FirstOrDefault(x => x.Key == "disabled").Value) : "",
                        comment = ColumnList.Any(x => x.Key == "comment") ? (ColumnList.FirstOrDefault(x => x.Key == "comment").Value) : "",
                        uptime_used = ColumnList.Any(x => x.Key == "uptime-used") ? (ColumnList.FirstOrDefault(x => x.Key == "uptime-used").Value) : "",
                        download_used = ColumnList.Any(x => x.Key == "download-used") ? (ColumnList.FirstOrDefault(x => x.Key == "download-used").Value) : "",
                        upload_used = ColumnList.Any(x => x.Key == "upload-used") ? (ColumnList.FirstOrDefault(x => x.Key == "upload-used").Value) : ""
                    });
                }
            }
            return usermodel;
        }
        public void Usermanager_DisableUser(string ip, int port, string user, string pass, string id)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/user/disable");
            string temp = String.Format("=.id={0}", id);
            mikrotik.Send(temp, true);
        }
        public void Usermanager_EnableUser(string ip, int port, string user, string pass, string id)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/user/enable");
            mikrotik.Send(String.Format("=.id={0}", id), true);
        }
        public void Usermanager_RemoveUser(string ip, int port, string user, string pass, string id)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/user/remove");
            string temp = String.Format("=.id={0}", id);
            mikrotik.Send(temp, true);
        }
        public void Usermanager_RemoveProfile(string ip, int port, string user, string pass, string id)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            var temp = Usermanager_GetAllProfile(ip, port, user, pass);
            foreach (var item in temp)
            {
                if (item.id == id)
                {
                    foreach (var item2 in Usermanager_GetAllLimition(ip, port, user, pass))
                    {
                        if (item2.name == item.name)
                        {
                            mikrotik.Send("/tool/user-manager/profile/limitation/remove");
                            string temp3 = String.Format("=.id={0}", item2.id);
                            mikrotik.Send(temp3, true);
                        }
                    }
                    foreach (var item2 in Usermanager_GetAllProfileLimition(ip, port, user, pass))
                    {
                        if (item2.profile == item.name && item2.limitation == item.name)
                        {
                            mikrotik.Send("/tool/user-manager/profile/profile-limitation/remove");
                            string temp3 = String.Format("=.id={0}", item2.id);
                            mikrotik.Send(temp3, true);
                        }
                    }
                    mikrotik.Send("/tool/user-manager/profile/remove");
                    string temp2 = String.Format("=.id={0}", id);
                    mikrotik.Send(temp2, true);
                }
            }

        }
        public bool Usermanager_IsUserExist(string ip, int port, string user, string pass, string username)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-------------------------------------------------
            foreach (var item in Usermanager_GetAllUsers(ip, port, user, pass))
            {
                if (item.username == username) return true;
            }
            return false;
        }
        public void Usermanager_UserCreate(string ip, int port, string user, string pass, Usermanager_UserRegisterModel usermanuser)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-------------------------------------------------
            mikrotik.Send("/tool/user-manager/user/add");
            string temp = String.Format("=phone={0}", usermanuser.phone);
            if (usermanuser.phone != "")
                mikrotik.Send(temp);
            temp = String.Format("=comment={0}", usermanuser.comment);
            if (usermanuser.comment != "")
                mikrotik.Send(temp);
            temp = String.Format("=customer={0}", usermanuser.customer);
            if (usermanuser.customer != "")
                mikrotik.Send(temp);
            temp = String.Format("=email={0}", usermanuser.email);
            if (usermanuser.email != "")
                mikrotik.Send(temp);
            temp = String.Format("=first-name={0}", usermanuser.first_name);
            if (usermanuser.first_name != "")
                mikrotik.Send(temp);
            temp = String.Format("=last-name={0}", usermanuser.last_name);
            if (usermanuser.last_name != "")
                mikrotik.Send(temp);
            temp = String.Format("=location={0}", usermanuser.location);
            if (usermanuser.location != "")
                mikrotik.Send(temp);
            temp = String.Format("=password={0}", usermanuser.password);
            if (usermanuser.password != "")
                mikrotik.Send(temp);
            temp = String.Format("=username={0}", usermanuser.username);
            mikrotik.Send(temp, true);
            foreach (var item in Usermanager_GetAllUsers(ip, port, user, pass))
                if (item.username == usermanuser.username)
                    temp = String.Format("=.id={0}", item.id);
            mikrotik.Send("/tool/user-manager/user/create-and-activate-profile");
            mikrotik.Send(temp);
            temp = String.Format("=customer={0}", usermanuser.customer);
            mikrotik.Send(temp);
            temp = String.Format("=profile={0}", usermanuser.profile);
            mikrotik.Send(temp, true);
        }
        public void Usermanager_UserEdit(string ip, int port, string user, string pass, Usermanager_UserEditModel model)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/user/set");
            string temp = String.Format("=.id={0}", model.id);
            mikrotik.Send(temp);
            temp = String.Format("=comment={0}", model.comment);
            mikrotik.Send(temp);
            temp = String.Format("=customer={0}", model.customer);
            mikrotik.Send(temp);
            temp = String.Format("=email={0}", model.email);
            mikrotik.Send(temp);
            temp = String.Format("=first-name={0}", model.first_name);
            mikrotik.Send(temp);
            temp = String.Format("=last-name={0}", model.last_name);
            mikrotik.Send(temp);
            temp = String.Format("=location={0}", model.location);
            mikrotik.Send(temp);
            temp = String.Format("=password={0}", model.password);
            mikrotik.Send(temp);
            temp = String.Format("=phone={0}", model.phone);
            mikrotik.Send(temp);
            temp = String.Format("=username={0}", model.username);
            mikrotik.Send(temp, true);
            var temp96 = mikrotik.Read();
            if (model.profile != "")
            {
                mikrotik.Send("/tool/user-manager/user/create-and-activate-profile");
                temp = String.Format("=.id={0}", model.username);
                mikrotik.Send(temp);
                temp = String.Format("=customer={0}", model.customer);
                mikrotik.Send(temp);
                temp = String.Format("=profile={0}", model.profile);
                mikrotik.Send(temp, true);
                var temp55 = mikrotik.Read();
            }
        }
        public List<Usermanager_UserSessionModel> Usermanager_UserSession(string ip, int port, string user, string pass, string UsermanUser)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/session/print");
            //  mikrotik.Send("where");
            string temp = String.Format("?=user={0}", UsermanUser);
            mikrotik.Send(temp, true);

            var usersessionmodel = new List<Usermanager_UserSessionModel>();
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }
                    usersessionmodel.Add(new Usermanager_UserSessionModel()
                    {
                        id = ColumnList.Any(x => x.Key == ".id") ? (ColumnList.FirstOrDefault(x => x.Key == ".id").Value) : "",
                        customer = ColumnList.Any(x => x.Key == "customer") ? (ColumnList.FirstOrDefault(x => x.Key == "customer").Value) : "",
                        user = ColumnList.Any(x => x.Key == "user") ? (ColumnList.FirstOrDefault(x => x.Key == "user").Value) : "",
                        acct_session_id = ColumnList.Any(x => x.Key == "acct-session-id") ? (ColumnList.FirstOrDefault(x => x.Key == "acct-session-id").Value) : "",
                        calling_station_id = ColumnList.Any(x => x.Key == "calling-station-id") ? (ColumnList.FirstOrDefault(x => x.Key == "calling-station-id").Value) : "",
                        download = ColumnList.Any(x => x.Key == "download") ? (ColumnList.FirstOrDefault(x => x.Key == "download").Value) : "",
                        from_time = ColumnList.Any(x => x.Key == "from-time") ? (ColumnList.FirstOrDefault(x => x.Key == "from-time").Value) : "",
                        host_ip = ColumnList.Any(x => x.Key == "host-ip") ? (ColumnList.FirstOrDefault(x => x.Key == "host-ip").Value) : "",
                        nas_port = ColumnList.Any(x => x.Key == "nas-port") ? (ColumnList.FirstOrDefault(x => x.Key == "nas-port").Value) : "",
                        nas_port_id = ColumnList.Any(x => x.Key == "nas-port-id") ? (ColumnList.FirstOrDefault(x => x.Key == "nas-port-id").Value) : "",
                        nas_port_type = ColumnList.Any(x => x.Key == "nas-port-type") ? (ColumnList.FirstOrDefault(x => x.Key == "nas-port-type").Value) : "",
                        status = ColumnList.Any(x => x.Key == "status") ? (ColumnList.FirstOrDefault(x => x.Key == "status").Value) : "",
                        terminate_cause = ColumnList.Any(x => x.Key == "terminate-cause") ? (ColumnList.FirstOrDefault(x => x.Key == "terminate-cause").Value) : "",
                        till_time = ColumnList.Any(x => x.Key == "till-time") ? (ColumnList.FirstOrDefault(x => x.Key == "till-time").Value) : "",
                        upload = ColumnList.Any(x => x.Key == "upload") ? (ColumnList.FirstOrDefault(x => x.Key == "upload").Value) : "",
                        uptime = ColumnList.Any(x => x.Key == "uptime") ? (ColumnList.FirstOrDefault(x => x.Key == "uptime").Value) : "",
                        user_ip = ColumnList.Any(x => x.Key == "user-ip") ? (ColumnList.FirstOrDefault(x => x.Key == "user-ip").Value) : "",
                        active = ColumnList.Any(x => x.Key == "active") ? (ColumnList.FirstOrDefault(x => x.Key == "active").Value) : ""
                    });

                }
            }
            var usersessionmodelresualt = new List<Usermanager_UserSessionModel>();
            foreach (var item in usersessionmodel)
            {
                if (item.user == UsermanUser)
                {
                    usersessionmodelresualt.Add(item);
                }
            }
            return usersessionmodelresualt;
        }
        public void Usermanager_ProfileCreate(string ip, int port, string user, string pass, Usermanager_ProfileLimitionCreateModel usermanProfile)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-------------------------------------------------
            mikrotik.Send("/tool/user-manager/profile/add");
            string temp = String.Format("=price={0}", usermanProfile.profile_price);
            if (usermanProfile.profile_price == null || usermanProfile.profile_price == "")
                temp = String.Format("=price={0}", "0");
            mikrotik.Send(temp);
            temp = String.Format("=validity={0}", usermanProfile.profile_validity);
            if (usermanProfile.profile_validity != null && usermanProfile.profile_validity != "")
                mikrotik.Send(temp);
            temp = String.Format("=starts-at={0}", usermanProfile.profile_starts_at);
            if (usermanProfile.profile_starts_at == null || usermanProfile.profile_starts_at == "")
                temp = String.Format("=starts-at={0}", "Now");
            mikrotik.Send(temp);
            temp = String.Format("=name-for-users={0}", usermanProfile.profile_name_for_users);
            if (usermanProfile.profile_name_for_users == null)
                temp = String.Format("=name-for-users={0}", usermanProfile.profile_name);
            mikrotik.Send(temp);
            temp = String.Format("=override-shared-users={0}", usermanProfile.profile_override_shared_users);
            if (usermanProfile.profile_override_shared_users == null || usermanProfile.limition_ip_pool == "")
                temp = String.Format("=override-shared-users={0}", "1");
            mikrotik.Send(temp);
            temp = String.Format("=owner={0}", usermanProfile.profile_owner);
            if (usermanProfile.profile_owner == null)
                temp = String.Format("=owner={0}", "admin");
            mikrotik.Send(temp);
            temp = String.Format("=name={0}", usermanProfile.profile_name);
            mikrotik.Send(temp, true);
            var temp66 = mikrotik.Read();
            //---------------------
            mikrotik.Send("/tool/user-manager/profile/limitation/add");
            temp = String.Format("=address-list={0}", usermanProfile.limition_address_list);
            if (usermanProfile.limition_address_list != null && usermanProfile.limition_address_list != "")
                mikrotik.Send(temp);
            temp = String.Format("=group-name={0}", usermanProfile.limition_group_name);
            if (usermanProfile.limition_group_name != null && usermanProfile.limition_group_name != "")
                mikrotik.Send(temp);
            temp = String.Format("=ip-pool={0}", usermanProfile.limition_ip_pool);
            if (usermanProfile.limition_ip_pool != null && usermanProfile.limition_ip_pool != "")
                mikrotik.Send(temp);
            temp = String.Format("=rate-limit-rx={0}", usermanProfile.limition_rate_limit_rx);
            if (usermanProfile.limition_rate_limit_rx != null && usermanProfile.limition_rate_limit_rx != "")
                mikrotik.Send(temp);
            temp = String.Format("=rate-limit-tx={0}", usermanProfile.limition_rate_limit_tx);
            if (usermanProfile.limition_rate_limit_tx != null && usermanProfile.limition_rate_limit_tx != "")
                mikrotik.Send(temp);
            temp = String.Format("=transfer-limit={0}", usermanProfile.limition_transfer_limit);
            if (usermanProfile.limition_transfer_limit != null && usermanProfile.limition_transfer_limit != "")
                mikrotik.Send(temp);
            temp = String.Format("=upload-limit={0}", usermanProfile.limition_upload_limit);
            if (usermanProfile.limition_upload_limit != null && usermanProfile.limition_upload_limit != "")
                mikrotik.Send(temp);
            temp = String.Format("=uptime-limit={0}", usermanProfile.limition_uptime_limit);
            if (usermanProfile.limition_uptime_limit != null && usermanProfile.limition_uptime_limit != "")
                mikrotik.Send(temp);
            temp = String.Format("=download-limit={0}", usermanProfile.limition_download_limit);
            if (usermanProfile.limition_download_limit != null && usermanProfile.limition_download_limit != "")
                mikrotik.Send(temp);
            temp = String.Format("=owner={0}", usermanProfile.limition_owner);
            if (usermanProfile.limition_owner == null)
                temp = String.Format("=owner={0}", "admin");
            mikrotik.Send(temp);
            temp = String.Format("=name={0}", usermanProfile.profile_name);
            mikrotik.Send(temp, true);
            var temp65 = mikrotik.Read();
            //----------------------
            mikrotik.Send("/tool/user-manager/profile/profile-limitation/add");
            temp = String.Format("=from-time={0}", usermanProfile.profilelimition_from_time);
            if (usermanProfile.profilelimition_from_time == null || usermanProfile.limition_ip_pool == "")
                temp = String.Format("=from-time={0}", "0s");
            mikrotik.Send(temp);
            temp = String.Format("=weekdays={0}", usermanProfile.profilelimition_weekdays);
            if (usermanProfile.profilelimition_weekdays != null && usermanProfile.profilelimition_weekdays != "")
                mikrotik.Send(temp);
            temp = String.Format("=till-time={0}", usermanProfile.profilelimition_till_time);
            if (usermanProfile.profilelimition_till_time == null || usermanProfile.limition_ip_pool == "")
                temp = String.Format("=till-time={0}", "1d");
            mikrotik.Send(temp);
            temp = String.Format("=limitation={0}", usermanProfile.profile_name);
            mikrotik.Send(temp);
            temp = String.Format("=profile={0}", usermanProfile.profile_name);
            mikrotik.Send(temp, true);
            var temp64 = mikrotik.Read();



            mikrotik.Send("/tool/user-manager/profile/profile-limitation/add");
            temp = String.Format("=from-time={0}", usermanProfile.profilelimition_from_time);
            if (usermanProfile.profilelimition_from_time == null || usermanProfile.limition_ip_pool == "")
                temp = String.Format("=from-time={0}", "0s");
            mikrotik.Send(temp);
            temp = String.Format("=weekdays={0}", usermanProfile.profilelimition_weekdays);
            if (usermanProfile.profilelimition_weekdays != null && usermanProfile.profilelimition_weekdays != "")
                mikrotik.Send(temp);
            temp = String.Format("=till-time={0}", usermanProfile.profilelimition_till_time);
            if (usermanProfile.profilelimition_till_time == null || usermanProfile.limition_ip_pool == "")
                temp = String.Format("=till-time={0}", "1d");
            mikrotik.Send(temp);
            temp = String.Format("=limitation={0}", usermanProfile.profile_name);
            mikrotik.Send(temp);
            temp = String.Format("=profile={0}", usermanProfile.profile_name);
            mikrotik.Send(temp, true);
            var temp63 = mikrotik.Read();
        }
        public bool Usermanager_IsProfileExist(string ip, int port, string user, string pass, Usermanager_ProfileLimitionCreateModel usermanProfile)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-------------------------------------------------
            foreach (var item in Usermanager_GetAllProfile(ip, port, user, pass))
            {
                if (item.name == usermanProfile.profile_name) return true;
            }
            return false;
        }
#endregion

        #region Router
        public Router_WirelessModel GetWirelessDetails(string r_Host, int r_Port, string r_User, string r_Password, string id)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(r_Host, r_Port);
            if (!mikrotik.Login(r_User, r_Password)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/interface/wireless/print");
            string temp = String.Format("?=.id={0}", id);
            mikrotik.Send(temp, true);

            var Wireless = new Router_WirelessModel();
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }
                    Wireless.adaptive_noise_immunity = ColumnList.Any(x => x.Key == "adaptive-noise-immunity") ? (ColumnList.FirstOrDefault(x => x.Key == "adaptive-noise-immunity").Value) : "";
                    Wireless.allow_sharedkey = ColumnList.Any(x => x.Key == "allow-sharedkey") ? (ColumnList.FirstOrDefault(x => x.Key == "allow-sharedkey").Value) : "";
                    Wireless.ampdu_priorities = ColumnList.Any(x => x.Key == "ampdu-priorities") ? (ColumnList.FirstOrDefault(x => x.Key == "ampdu-priorities").Value) : "";
                    Wireless.amsdu_limit = ColumnList.Any(x => x.Key == "amsdu-limit") ? (ColumnList.FirstOrDefault(x => x.Key == "amsdu-limit").Value) : "";
                    Wireless.amsdu_threshold = ColumnList.Any(x => x.Key == "amsdu-threshold") ? (ColumnList.FirstOrDefault(x => x.Key == "amsdu-threshold").Value) : "";
                    Wireless.antenna_gain = ColumnList.Any(x => x.Key == "antenna-gain") ? (ColumnList.FirstOrDefault(x => x.Key == "antenna-gain").Value) : "";
                    Wireless.area = ColumnList.Any(x => x.Key == "area") ? (ColumnList.FirstOrDefault(x => x.Key == "area").Value) : "";
                    Wireless.arp = ColumnList.Any(x => x.Key == "arp") ? (ColumnList.FirstOrDefault(x => x.Key == "arp").Value) : "";
                    Wireless.arp_timeout = ColumnList.Any(x => x.Key == "arp-timeout") ? (ColumnList.FirstOrDefault(x => x.Key == "arp-timeout").Value) : "";
                    Wireless.band = ColumnList.Any(x => x.Key == "band") ? (ColumnList.FirstOrDefault(x => x.Key == "band").Value) : "";
                    Wireless.basic_rates_a_g = ColumnList.Any(x => x.Key == "basic-rates-a/g") ? (ColumnList.FirstOrDefault(x => x.Key == "basic-rates-a/g").Value) : "";
                    Wireless.basic_rates_b = ColumnList.Any(x => x.Key == "basic-rates-b") ? (ColumnList.FirstOrDefault(x => x.Key == "basic-rates-b").Value) : "";
                    Wireless.bridge_mode = ColumnList.Any(x => x.Key == "bridge-mode") ? (ColumnList.FirstOrDefault(x => x.Key == "bridge-mode").Value) : "";
                    Wireless.channel_width = ColumnList.Any(x => x.Key == "channel-width") ? (ColumnList.FirstOrDefault(x => x.Key == "channel-width").Value) : "";
                    Wireless.compression = ColumnList.Any(x => x.Key == "compression") ? (ColumnList.FirstOrDefault(x => x.Key == "compression").Value) : "";
                    Wireless.country = ColumnList.Any(x => x.Key == "country") ? (ColumnList.FirstOrDefault(x => x.Key == "country").Value) : "";
                    Wireless.default_ap_tx_limit = ColumnList.Any(x => x.Key == "default-ap-tx-limit") ? (ColumnList.FirstOrDefault(x => x.Key == "default-ap-tx-limit").Value) : "";
                    Wireless.default_authentication = ColumnList.Any(x => x.Key == "default-authentication") ? (ColumnList.FirstOrDefault(x => x.Key == "default-authentication").Value) : "";
                    Wireless.default_client_tx_limit = ColumnList.Any(x => x.Key == "default-client-tx-limit") ? (ColumnList.FirstOrDefault(x => x.Key == "default-client-tx-limit").Value) : "";
                    Wireless.default_forwarding = ColumnList.Any(x => x.Key == "default-forwarding") ? (ColumnList.FirstOrDefault(x => x.Key == "default-forwarding").Value) : "";
                    Wireless.default_name = ColumnList.Any(x => x.Key == "default-name") ? (ColumnList.FirstOrDefault(x => x.Key == "default-name").Value) : "";
                    Wireless.disabled = ColumnList.Any(x => x.Key == "disabled") ? (ColumnList.FirstOrDefault(x => x.Key == "disabled").Value) : "";
                    Wireless.disable_running_check = ColumnList.Any(x => x.Key == "disable-running-check") ? (ColumnList.FirstOrDefault(x => x.Key == "disable-running-check").Value) : "";
                    Wireless.disconnect_timeout = ColumnList.Any(x => x.Key == "disconnect-timeout") ? (ColumnList.FirstOrDefault(x => x.Key == "disconnect-timeout").Value) : "";
                    Wireless.distance = ColumnList.Any(x => x.Key == "distance") ? (ColumnList.FirstOrDefault(x => x.Key == "distance").Value) : "";
                    Wireless.frame_lifetime = ColumnList.Any(x => x.Key == "frame-lifetime") ? (ColumnList.FirstOrDefault(x => x.Key == "frame-lifetime").Value) : "";
                    Wireless.frequency = ColumnList.Any(x => x.Key == "frequency") ? (ColumnList.FirstOrDefault(x => x.Key == "frequency").Value) : "";
                    Wireless.frequency_mode = ColumnList.Any(x => x.Key == "frequency-mode") ? (ColumnList.FirstOrDefault(x => x.Key == "frequency-mode").Value) : "";
                    Wireless.frequency_offset = ColumnList.Any(x => x.Key == "frequency-offset") ? (ColumnList.FirstOrDefault(x => x.Key == "frequency-offset").Value) : "";
                    Wireless.guard_interval = ColumnList.Any(x => x.Key == "guard-interval") ? (ColumnList.FirstOrDefault(x => x.Key == "guard-interval").Value) : "";
                    Wireless.hide_ssid = ColumnList.Any(x => x.Key == "hide-ssid") ? (ColumnList.FirstOrDefault(x => x.Key == "hide-ssid").Value) : "";
                    Wireless.ht_supported_mcs = ColumnList.Any(x => x.Key == "ht-supported-mcs") ? (ColumnList.FirstOrDefault(x => x.Key == "ht-supported-mcs").Value) : "";
                    Wireless.hw_fragmentation_threshold = ColumnList.Any(x => x.Key == "hw-fragmentation-threshold") ? (ColumnList.FirstOrDefault(x => x.Key == "hw-fragmentation-threshold").Value) : "";
                    Wireless.hw_protection_mode = ColumnList.Any(x => x.Key == "hw-protection-mode") ? (ColumnList.FirstOrDefault(x => x.Key == "hw-protection-mode").Value) : "";
                    Wireless.hw_protection_threshold = ColumnList.Any(x => x.Key == "hw-protection-threshold") ? (ColumnList.FirstOrDefault(x => x.Key == "hw-protection-threshold").Value) : "";
                    Wireless.hw_retries = ColumnList.Any(x => x.Key == "hw-retries") ? (ColumnList.FirstOrDefault(x => x.Key == "hw-retries").Value) : "";
                    Wireless.id = ColumnList.Any(x => x.Key == ".id") ? (ColumnList.FirstOrDefault(x => x.Key == ".id").Value) : "";
                    Wireless.interface_type = ColumnList.Any(x => x.Key == "interface-type") ? (ColumnList.FirstOrDefault(x => x.Key == "interface-type").Value) : "";
                    Wireless.interworking_profile = ColumnList.Any(x => x.Key == "interworking-profile") ? (ColumnList.FirstOrDefault(x => x.Key == "interworking-profile").Value) : "";
                    Wireless.keepalive_frames = ColumnList.Any(x => x.Key == "keepalive-frames") ? (ColumnList.FirstOrDefault(x => x.Key == "keepalive-frames").Value) : "";
                    Wireless.l2mtu = ColumnList.Any(x => x.Key == "l2mtu") ? (ColumnList.FirstOrDefault(x => x.Key == "l2mtu").Value) : "";
                    Wireless.mac_address = ColumnList.Any(x => x.Key == "mac-address") ? (ColumnList.FirstOrDefault(x => x.Key == "mac-address").Value) : "";
                    Wireless.max_station_count = ColumnList.Any(x => x.Key == "max-station-count") ? (ColumnList.FirstOrDefault(x => x.Key == "max-station-count").Value) : "";
                    Wireless.mode = ColumnList.Any(x => x.Key == "mode") ? (ColumnList.FirstOrDefault(x => x.Key == "mode").Value) : "";
                    Wireless.mtu = ColumnList.Any(x => x.Key == "mtu") ? (ColumnList.FirstOrDefault(x => x.Key == "mtu").Value) : "";
                    Wireless.multicast_buffering = ColumnList.Any(x => x.Key == "multicast-buffering") ? (ColumnList.FirstOrDefault(x => x.Key == "multicast-buffering").Value) : "";
                    Wireless.multicast_helper = ColumnList.Any(x => x.Key == "multicast-helper") ? (ColumnList.FirstOrDefault(x => x.Key == "multicast-helper").Value) : "";
                    Wireless.name = ColumnList.Any(x => x.Key == "name") ? (ColumnList.FirstOrDefault(x => x.Key == "name").Value) : "";
                    Wireless.noise_floor_threshold = ColumnList.Any(x => x.Key == "noise-floor-threshold") ? (ColumnList.FirstOrDefault(x => x.Key == "noise-floor-threshold").Value) : "";
                    Wireless.nv2_cell_radius = ColumnList.Any(x => x.Key == "nv2-cell-radius") ? (ColumnList.FirstOrDefault(x => x.Key == "nv2-cell-radius").Value) : "";
                    Wireless.nv2_noise_floor_offset = ColumnList.Any(x => x.Key == "nv2-noise-floor-offset") ? (ColumnList.FirstOrDefault(x => x.Key == "nv2-noise-floor-offset").Value) : "";
                    Wireless.nv2_preshared_key = ColumnList.Any(x => x.Key == "nv2-preshared-key") ? (ColumnList.FirstOrDefault(x => x.Key == "nv2-presharedkey").Value) : "";
                    Wireless.nv2_qos = ColumnList.Any(x => x.Key == "nv2-qos") ? (ColumnList.FirstOrDefault(x => x.Key == "nv2-qos").Value) : "";
                    Wireless.nv2_queue_count = ColumnList.Any(x => x.Key == "nv2-queue-count") ? (ColumnList.FirstOrDefault(x => x.Key == "nv2-queue-count").Value) : "";
                    Wireless.nv2_security = ColumnList.Any(x => x.Key == "nv2-security") ? (ColumnList.FirstOrDefault(x => x.Key == "nv2-security").Value) : "";
                    Wireless.on_fail_retry_time = ColumnList.Any(x => x.Key == "on-fail-retry-time") ? (ColumnList.FirstOrDefault(x => x.Key == "on-fail-retry-time").Value) : "";
                    Wireless.preamble_mode = ColumnList.Any(x => x.Key == "preamble-mode") ? (ColumnList.FirstOrDefault(x => x.Key == "preamble-mode").Value) : "";
                    Wireless.radio_name = ColumnList.Any(x => x.Key == "radio-name") ? (ColumnList.FirstOrDefault(x => x.Key == "radio-name").Value) : "";
                    Wireless.rate_selection = ColumnList.Any(x => x.Key == "rate-selection") ? (ColumnList.FirstOrDefault(x => x.Key == "rate-selection").Value) : "";
                    Wireless.rate_set = ColumnList.Any(x => x.Key == "rate-set") ? (ColumnList.FirstOrDefault(x => x.Key == "rate-set").Value) : "";
                    Wireless.running = ColumnList.Any(x => x.Key == "running") ? (ColumnList.FirstOrDefault(x => x.Key == "running").Value) : "";
                    Wireless.rx_chains = ColumnList.Any(x => x.Key == "rx-chains") ? (ColumnList.FirstOrDefault(x => x.Key == "rx-chains").Value) : "";
                    Wireless.scan_list = ColumnList.Any(x => x.Key == "scan-list") ? (ColumnList.FirstOrDefault(x => x.Key == "scan-list").Value) : "";
                    Wireless.security_profile = ColumnList.Any(x => x.Key == "security-profile") ? (ColumnList.FirstOrDefault(x => x.Key == "security-profile").Value) : "";
                    Wireless.ssid = ColumnList.Any(x => x.Key == "ssid") ? (ColumnList.FirstOrDefault(x => x.Key == "ssid").Value) : "";
                    Wireless.station_bridge_clone_mac = ColumnList.Any(x => x.Key == "station-bridge-clone-mac") ? (ColumnList.FirstOrDefault(x => x.Key == "station-bridge-clone-mac").Value) : "";
                    Wireless.station_roaming = ColumnList.Any(x => x.Key == "station-roaming") ? (ColumnList.FirstOrDefault(x => x.Key == "station-roaming").Value) : "";
                    Wireless.supported_rates_a_g = ColumnList.Any(x => x.Key == "supported-rates-a/g") ? (ColumnList.FirstOrDefault(x => x.Key == "supported-rates-a/g").Value) : "";
                    Wireless.supported_rates_b = ColumnList.Any(x => x.Key == "supported-rates-b") ? (ColumnList.FirstOrDefault(x => x.Key == "supported-rates-b").Value) : "";
                    Wireless.tdma_period_size = ColumnList.Any(x => x.Key == "tdma-period-size") ? (ColumnList.FirstOrDefault(x => x.Key == "tdma-period-size").Value) : "";
                    Wireless.tx_chains = ColumnList.Any(x => x.Key == "tx-chains") ? (ColumnList.FirstOrDefault(x => x.Key == "tx-chains").Value) : "";
                    Wireless.tx_power_mode = ColumnList.Any(x => x.Key == "tx-power-mode") ? (ColumnList.FirstOrDefault(x => x.Key == "tx-power-mode").Value) : "";
                    Wireless.update_stats_interval = ColumnList.Any(x => x.Key == "update-stats-interval") ? (ColumnList.FirstOrDefault(x => x.Key == "update-stats-interval").Value) : "";
                    Wireless.vlan_id = ColumnList.Any(x => x.Key == "vlan-id") ? (ColumnList.FirstOrDefault(x => x.Key == "vlan-id").Value) : "";
                    Wireless.vlan_mode = ColumnList.Any(x => x.Key == "vlan-mode") ? (ColumnList.FirstOrDefault(x => x.Key == "vlan-mode").Value) : "";
                    Wireless.wds_cost_range = ColumnList.Any(x => x.Key == "wds-cost-range") ? (ColumnList.FirstOrDefault(x => x.Key == "wds-cost-range").Value) : "";
                    Wireless.wds_default_bridge = ColumnList.Any(x => x.Key == "wds-default-bridge") ? (ColumnList.FirstOrDefault(x => x.Key == "wds-default-bridge").Value) : "";
                    Wireless.wds_default_cost = ColumnList.Any(x => x.Key == "wds-default-cost") ? (ColumnList.FirstOrDefault(x => x.Key == "wds-default-cost").Value) : "";
                    Wireless.wds_ignore_ssid = ColumnList.Any(x => x.Key == "wds-ignore-ssid") ? (ColumnList.FirstOrDefault(x => x.Key == "wds-ignore-ssid").Value) : "";
                    Wireless.wds_mode = ColumnList.Any(x => x.Key == "wds-mode") ? (ColumnList.FirstOrDefault(x => x.Key == "wds-mode").Value) : "";
                    Wireless.wireless_protocol = ColumnList.Any(x => x.Key == "wireless-protocol") ? (ColumnList.FirstOrDefault(x => x.Key == "wireless-protocol").Value) : "";
                    Wireless.wmm_support = ColumnList.Any(x => x.Key == "wmm-support") ? (ColumnList.FirstOrDefault(x => x.Key == "wmm-support").Value) : "";
                    Wireless.wps_mode = ColumnList.Any(x => x.Key == "wps-mode") ? (ColumnList.FirstOrDefault(x => x.Key == "wps-mode").Value) : "";
                }
            }

            return Wireless;
        }

        public void Router_InterfaceEnable(string r_Host, int r_Port, string r_User, string r_Password, string id)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(r_Host, r_Port);
            if (!mikrotik.Login(r_User, r_Password)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/interface/enable");
            string temp = String.Format("=.id={0}", id);
            mikrotik.Send(temp, true);
        }

        public void Router_InterfaceDisable(string r_Host, int r_Port, string r_User, string r_Password, string id)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(r_Host, r_Port);
            if (!mikrotik.Login(r_User, r_Password)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/interface/disable");
            string temp = String.Format("=.id={0}", id);
            mikrotik.Send(temp, true);
        }

        public void Router_Info_Update(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/system/package/update/install", true);
        }
        public string EnableAndGetCloud(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/ip/cloud/set");
            mikrotik.Send("=ddns-enabled=yes", true);
            mikrotik.Send("/ip/cloud/set");
            mikrotik.Send("=update-time=yes", true);
            mikrotik.Send("/ip/cloud/force-update", true);
            mikrotik.Send("/ip/cloud/print", true);
            string dns_name = "";
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }
                    dns_name = ColumnList.Any(x => x.Key == "dns-name") ? (ColumnList.FirstOrDefault(x => x.Key == "dns-name").Value) : "";
                }
            }
            if (IP_Port_Check(dns_name, port, user, pass))
                return dns_name;
            else
                return ip;
        }
        public string GetRouterName(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            mikrotik.Send("/system/identity/getall", true);
            return (mikrotik.Read())[0].Split('=')[2].Replace('!', ' ');
        }
        public List<Router_InterfaceModel> Interface(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/interface/print", true);
            var interfacemodel = new List<Router_InterfaceModel>();

            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }

                    interfacemodel.Add(new Router_InterfaceModel()
                    {
                        id = ColumnList.Any(x => x.Key == ".id") ? (ColumnList.FirstOrDefault(x => x.Key == ".id").Value) : "",
                        name = ColumnList.Any(x => x.Key == "name") ? (ColumnList.FirstOrDefault(x => x.Key == "name").Value.Replace("d", " ").Replace("h", ":").Replace("m", ":").Replace("s", ":")) : "",
                        default_name = ColumnList.Any(x => x.Key == "default-name") ? (ColumnList.FirstOrDefault(x => x.Key == "default-name").Value) : "",
                        type = ColumnList.Any(x => x.Key == "type") ? (ColumnList.FirstOrDefault(x => x.Key == "type").Value) : "",
                        mtu = ColumnList.Any(x => x.Key == "mtu") ? (ColumnList.FirstOrDefault(x => x.Key == "mtu").Value) : "",
                        actual_mtu = ColumnList.Any(x => x.Key == "actual-mtu") ? (ColumnList.FirstOrDefault(x => x.Key == "actual-mtu").Value) : "",
                        mac_address = ColumnList.Any(x => x.Key == "mac-address") ? (ColumnList.FirstOrDefault(x => x.Key == "mac-address").Value) : "",
                        last_link_up_time = ColumnList.Any(x => x.Key == "last-link-up-time") ? (ColumnList.FirstOrDefault(x => x.Key == "last-link-up-time").Value) : "",
                        link_downs = ColumnList.Any(x => x.Key == "link-downs") ? (ColumnList.FirstOrDefault(x => x.Key == "link-downs").Value) : "",
                        rx_byte = ColumnList.Any(x => x.Key == "rx-byte") ? (ColumnList.FirstOrDefault(x => x.Key == "rx-byte").Value) : "",
                        tx_byte = ColumnList.Any(x => x.Key == "tx-byte") ? (ColumnList.FirstOrDefault(x => x.Key == "tx-byte").Value) : "",
                        rx_packet = ColumnList.Any(x => x.Key == "rx-packet") ? (ColumnList.FirstOrDefault(x => x.Key == "rx-packet").Value) : "",
                        tx_packet = ColumnList.Any(x => x.Key == "tx-packet") ? (ColumnList.FirstOrDefault(x => x.Key == "tx-packet").Value) : "",
                        disabled = ColumnList.Any(x => x.Key == "disabled") ? (ColumnList.FirstOrDefault(x => x.Key == "disabled").Value) : "",
                        running = ColumnList.Any(x => x.Key == "running") ? (ColumnList.FirstOrDefault(x => x.Key == "running").Value) : "",
                        rx_drop = ColumnList.Any(x => x.Key == "rx-drop") ? (ColumnList.FirstOrDefault(x => x.Key == "rx-drop").Value) : "",
                        rx_error = ColumnList.Any(x => x.Key == "rx-error") ? (ColumnList.FirstOrDefault(x => x.Key == "rx-error").Value) : "",
                        tx_drop = ColumnList.Any(x => x.Key == "tx-drop") ? (ColumnList.FirstOrDefault(x => x.Key == "tx-drop").Value) : "",
                        tx_error = ColumnList.Any(x => x.Key == "tx-error") ? (ColumnList.FirstOrDefault(x => x.Key == "tx-error").Value) : "",
                    });
                }
            }
            return interfacemodel;
        }



        public List<Router_PackageUpdateModel> Router_PackageUpdate(string r_Host, int r_Port, string r_User, string r_Password)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(r_Host, r_Port);
            if (!mikrotik.Login(r_User, r_Password)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/system/package/update/print", true);
            var Router_PackageUpdate = new List<Router_PackageUpdateModel>();
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }

                    Router_PackageUpdate.Add(new Router_PackageUpdateModel()
                    {
                        Channel = ColumnList.Any(x => x.Key == "channel") ? (ColumnList.FirstOrDefault(x => x.Key == "channel").Value) : "",
                        Update_status = ColumnList.Any(x => x.Key == "status") ? (ColumnList.FirstOrDefault(x => x.Key == "status").Value) : "",
                        Latest_version = ColumnList.Any(x => x.Key == "latest-version") ? (ColumnList.FirstOrDefault(x => x.Key == "latest-version").Value) : "",
                        Installed_version = ColumnList.Any(x => x.Key == "installed-version") ? (ColumnList.FirstOrDefault(x => x.Key == "installed-version").Value) : "",
                    });

                }
            }
            return Router_PackageUpdate;

        }

        public List<Router_ClockModel> Router_Clock(string r_Host, int r_Port, string r_User, string r_Password)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(r_Host, r_Port);
            if (!mikrotik.Login(r_User, r_Password)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/system/clock/print", true);
            var Router_Clock = new List<Router_ClockModel>();
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }

                    Router_Clock.Add(new Router_ClockModel()
                    {
                        Router_time = ColumnList.Any(x => x.Key == "time") ? (ColumnList.FirstOrDefault(x => x.Key == "time").Value) : "",
                        Router_date = ColumnList.Any(x => x.Key == "date") ? (ColumnList.FirstOrDefault(x => x.Key == "date").Value) : ""
                    });

                }
            }
            return Router_Clock;
        }

        public List<Router_RouterBoardModel> Router_Routerboard(string r_Host, int r_Port, string r_User, string r_Password)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(r_Host, r_Port);
            if (!mikrotik.Login(r_User, r_Password)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/system/routerboard/print", true);
            var Router_RouterBoard = new List<Router_RouterBoardModel>();
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }

                    Router_RouterBoard.Add(new Router_RouterBoardModel()
                    {
                        Router_model = ColumnList.Any(x => x.Key == "model") ? (ColumnList.FirstOrDefault(x => x.Key == "model").Value) : "",
                        Serial_number = ColumnList.Any(x => x.Key == "serial-number") ? (ColumnList.FirstOrDefault(x => x.Key == "serial-number").Value) : ""
                    });

                }
            }
            return Router_RouterBoard;
        }

        public List<Router_IdentityModel> Router_Identity(string r_Host, int r_Port, string r_User, string r_Password)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(r_Host, r_Port);
            if (!mikrotik.Login(r_User, r_Password)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/system/identity/print", true);
            var Router_Identity = new List<Router_IdentityModel>();
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }

                    Router_Identity.Add(new Router_IdentityModel()
                    {
                        RouterName = ColumnList.Any(x => x.Key == "name") ? (ColumnList.FirstOrDefault(x => x.Key == "name").Value) : ""
                    });

                }
            }
            return Router_Identity;


        }

        public List<Router_LicenseModel> Router_License(string r_Host, int r_Port, string r_User, string r_Password)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(r_Host, r_Port);
            if (!mikrotik.Login(r_User, r_Password)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/system/license/print", true);
            var Router_License = new List<Router_LicenseModel>();
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }

                    Router_License.Add(new Router_LicenseModel()
                    {
                        Software_id = ColumnList.Any(x => x.Key == "software-id") ? (ColumnList.FirstOrDefault(x => x.Key == "software-id").Value) : "",
                        nlevel = ColumnList.Any(x => x.Key == "nlevel") ? (ColumnList.FirstOrDefault(x => x.Key == "nlevel").Value) : ""
                    });

                }
            }
            return Router_License;

        }
        public Router_EthernetModel GetEthernetDetails(string r_Host, int r_Port, string r_User, string r_Password, string id)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(r_Host, r_Port);
            if (!mikrotik.Login(r_User, r_Password)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/interface/ethernet/print");
            string temp = String.Format("?=.id={0}", id);
            mikrotik.Send(temp, true);

            var Ethernet = new Router_EthernetModel();
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }
                    Ethernet.advertise = ColumnList.Any(x => x.Key == "advertise") ? (ColumnList.FirstOrDefault(x => x.Key == "advertise").Value) : "";
                    Ethernet.arp = ColumnList.Any(x => x.Key == "arp") ? (ColumnList.FirstOrDefault(x => x.Key == "arp").Value) : "";
                    Ethernet.arp_timeout = ColumnList.Any(x => x.Key == "arp-timeout") ? (ColumnList.FirstOrDefault(x => x.Key == "arp-timeout").Value) : "";
                    Ethernet.auto_negotiation = ColumnList.Any(x => x.Key == "auto-negotiation") ? (ColumnList.FirstOrDefault(x => x.Key == "auto-negotiation").Value) : "";
                    Ethernet.cable_settings = ColumnList.Any(x => x.Key == "cable-settings") ? (ColumnList.FirstOrDefault(x => x.Key == "cable-settings").Value) : "";
                    Ethernet.default_name = ColumnList.Any(x => x.Key == "default-name") ? (ColumnList.FirstOrDefault(x => x.Key == "default-name").Value) : "";
                    Ethernet.disabled = ColumnList.Any(x => x.Key == "disabled") ? (ColumnList.FirstOrDefault(x => x.Key == "disabled").Value) : "";
                    Ethernet.disable_running_check = ColumnList.Any(x => x.Key == "disable_running_check") ? (ColumnList.FirstOrDefault(x => x.Key == "disable_running_check").Value) : "";
                    Ethernet.full_duplex = ColumnList.Any(x => x.Key == "full-duplex") ? (ColumnList.FirstOrDefault(x => x.Key == "full-duplex").Value) : "";
                    Ethernet.id = ColumnList.Any(x => x.Key == ".id") ? (ColumnList.FirstOrDefault(x => x.Key == ".id").Value) : "";
                    Ethernet.loop_protect = ColumnList.Any(x => x.Key == "loop-protect") ? (ColumnList.FirstOrDefault(x => x.Key == "loop-protect").Value) : "";
                    Ethernet.loop_protect_disable_time = ColumnList.Any(x => x.Key == "loop-protect-disable-time") ? (ColumnList.FirstOrDefault(x => x.Key == "loop-protect-disable-time").Value) : "";
                    Ethernet.loop_protect_send_interval = ColumnList.Any(x => x.Key == "loop-protect-send-interval") ? (ColumnList.FirstOrDefault(x => x.Key == "loop-protect-send-interval").Value) : "";
                    Ethernet.loop_protect_status = ColumnList.Any(x => x.Key == "loop-protect-status") ? (ColumnList.FirstOrDefault(x => x.Key == "loop-protect-status").Value) : "";
                    Ethernet.mac_address = ColumnList.Any(x => x.Key == "mac-address") ? (ColumnList.FirstOrDefault(x => x.Key == "mac-address").Value) : "";
                    Ethernet.mtu = ColumnList.Any(x => x.Key == "mtu") ? (ColumnList.FirstOrDefault(x => x.Key == "mtu").Value) : "";
                    Ethernet.name = ColumnList.Any(x => x.Key == "name") ? (ColumnList.FirstOrDefault(x => x.Key == "name").Value) : "";
                    Ethernet.orig_mac_address = ColumnList.Any(x => x.Key == "orig-mac-address") ? (ColumnList.FirstOrDefault(x => x.Key == "orig-mac-address").Value) : "";
                    Ethernet.running = ColumnList.Any(x => x.Key == "running") ? (ColumnList.FirstOrDefault(x => x.Key == "running").Value) : "";
                    Ethernet.rx_flow_control = ColumnList.Any(x => x.Key == "rx-flow-control") ? (ColumnList.FirstOrDefault(x => x.Key == "rx-flow-control").Value) : "";
                    Ethernet.speed = ColumnList.Any(x => x.Key == "speed") ? (ColumnList.FirstOrDefault(x => x.Key == "speed").Value) : "";
                    Ethernet.tx_flow_control = ColumnList.Any(x => x.Key == "tx-flow-control") ? (ColumnList.FirstOrDefault(x => x.Key == "tx-flow-control").Value) : "";
                }
            }

            return Ethernet;
        }
        public List<Router_ResourceModel> Router_Resource(string r_Host, int r_Port, string r_User, string r_Password)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(r_Host, r_Port);
            if (!mikrotik.Login(r_User, r_Password)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/system/resource/print", true);
            var Router_Resource = new List<Router_ResourceModel>();
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }

                    Router_Resource.Add(new Router_ResourceModel()
                    {
                        Uptime = ColumnList.Any(x => x.Key == "uptime") ? (ColumnList.FirstOrDefault(x => x.Key == "uptime").Value.Replace("d", " ").Replace("h", ":").Replace("m", ":").Replace("s", "")) : "",
                        Version = ColumnList.Any(x => x.Key == "version") ? (ColumnList.FirstOrDefault(x => x.Key == "version").Value) : "",
                        Build_time = ColumnList.Any(x => x.Key == "build-time") ? (ColumnList.FirstOrDefault(x => x.Key == "build-time").Value) : "",
                        Free_memory = ColumnList.Any(x => x.Key == "free-memory") ? (ColumnList.FirstOrDefault(x => x.Key == "free-memory").Value) : "",
                        Total_memory = ColumnList.Any(x => x.Key == "total-memory") ? (ColumnList.FirstOrDefault(x => x.Key == "total-memory").Value) : "",
                        Cpu = ColumnList.Any(x => x.Key == "cpu") ? (ColumnList.FirstOrDefault(x => x.Key == "cpu").Value) : "",
                        Cpu_count = ColumnList.Any(x => x.Key == "cpu-count") ? (ColumnList.FirstOrDefault(x => x.Key == "cpu-count").Value) : "",
                        Cpu_frequency = ColumnList.Any(x => x.Key == "cpu-frequency") ? (ColumnList.FirstOrDefault(x => x.Key == "cpu-frequency").Value) : "",
                        Cpu_load = ColumnList.Any(x => x.Key == "cpu-load") ? (ColumnList.FirstOrDefault(x => x.Key == "cpu-load").Value) : "",
                        Free_hdd_space = ColumnList.Any(x => x.Key == "free-hdd-space") ? (ColumnList.FirstOrDefault(x => x.Key == "free-hdd-space").Value) : "",
                        Total_hdd_space = ColumnList.Any(x => x.Key == "total-hdd-space") ? (ColumnList.FirstOrDefault(x => x.Key == "total-hdd-space").Value) : "",
                        Architecture_name = ColumnList.Any(x => x.Key == "architecture-name") ? (ColumnList.FirstOrDefault(x => x.Key == "architecture-name").Value) : "",
                        Board_name = ColumnList.Any(x => x.Key == "board-name") ? (ColumnList.FirstOrDefault(x => x.Key == "board-name").Value) : "",
                        Platform = ColumnList.Any(x => x.Key == "platform") ? (ColumnList.FirstOrDefault(x => x.Key == "platform").Value) : "",
                    });

                }
            }
            return Router_Resource;

        }


        #endregion

        #region Other
        public bool ResetUsermanager(string ip, int port, string user, string pass, bool users, bool logs, bool session, bool history, bool packages,bool db)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            var temp = "";
            mikrotik.Send("/tool/user-manager/database/save");
            temp = ConvertDate.ToFa(DateTime.Now, "yyyy-MM-dd ").ToString() + " " + ConvertDate.ToFa(DateTime.Now, "T").ToString();
            temp = String.Format("=name=\"Netotik.({0}).UsermanagerReset\"", temp);
            mikrotik.Send(temp,true);
            mikrotik.Send("/tool/user-manager/database/save-logs");
            temp = ConvertDate.ToFa(DateTime.Now, "yyyy-MM-dd ").ToString() + " " + ConvertDate.ToFa(DateTime.Now, "T").ToString();
            temp = String.Format("=name=\"Netotik.({0}).UsermanagerResetLog\"", temp);
            mikrotik.Send(temp, true);
            //------------------------------------------------
            if (logs)
            {
                mikrotik.Send("/tool/user-manager/database/clear-log");
                mikrotik.Send("yes", true);
            }
            if (db)
            {
                mikrotik.Send("/tool/user-manager/database/clear");
                mikrotik.Send("yes", true);
            }
            if (history)
            {
                mikrotik.Send("/tool/user-manager/history/clear");
                mikrotik.Send("yes", true);
            }
            var count = 0;
            if (packages)
            {
                mikrotik.Send("/tool/user-manager/profile/print");
                mikrotik.Send("=count-only", true);
                var tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/profile/print");
                mikrotik.Send("?count-only", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/profile/print");
                mikrotik.Send("=count-only=", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/profile/print");
                mikrotik.Send("?count-only=", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/profile/print");
                mikrotik.Send("=count-only=yes", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/profile/print");
                mikrotik.Send("?count-only=yes", true);
                tep = mikrotik.Read();
                foreach (var item in tep)
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }
                    count = Int32.Parse(ColumnList.Any(x => x.Key == "ret") ? (ColumnList.FirstOrDefault(x => x.Key == "ret").Value) : "");
                }
                for (int i = 0;i<count;i++)
                {
                    mikrotik.Send("/tool/user-manager/profile/remove");
                    temp = String.Format("=numbers={0}", i);
                    mikrotik.Send(temp, true);
                }
                mikrotik.Send("/tool/user-manager/profile/limitation/print");
                mikrotik.Send("=count-only", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/profile/limitation/print");
                mikrotik.Send("?count-only", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/profile/limitation/print");
                mikrotik.Send("=count-only=", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/profile/limitation/print");
                mikrotik.Send("?count-only=", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/profile/limitation/print");
                mikrotik.Send("=count-only=yes", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/profile/limitation/print");
                mikrotik.Send("?count-only=yes", true);
                tep = mikrotik.Read();
                foreach (var item in tep)
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }
                    count = Int32.Parse(ColumnList.Any(x => x.Key == "ret") ? (ColumnList.FirstOrDefault(x => x.Key == "ret").Value) : "");
                }
                for (int i = 0; i < count; i++)
                {
                    mikrotik.Send("/tool/user-manager/profile/limitation/remove");
                    temp = String.Format("=numbers={0}", i);
                    mikrotik.Send(temp, true);
                }

                mikrotik.Send("/tool/user-manager/profile/profile-limitation/print");
                mikrotik.Send("=count-only", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/profile/profile-limitation/print");
                mikrotik.Send("?count-only", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/profile/profile-limitation/print");
                mikrotik.Send("=count-only=", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/profile/profile-limitation/print");
                mikrotik.Send("?count-only=", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/profile/profile-limitation/print");
                mikrotik.Send("=count-only=yes", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/profile/profile-limitation/print");
                mikrotik.Send("?count-only=yes", true);
                tep = mikrotik.Read();
                foreach (var item in tep)
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }
                    count = Int32.Parse(ColumnList.Any(x => x.Key == "ret") ? (ColumnList.FirstOrDefault(x => x.Key == "ret").Value) : "");
                }
                for (int i = 0; i < count; i++)
                {
                    mikrotik.Send("/tool/user-manager/profile/profile-limitation/remove");
                    temp = String.Format("=numbers={0}", i);
                    mikrotik.Send(temp, true);
                }

            }
            if (users)
            {
                mikrotik.Send("/tool/user-manager/user/print");
                mikrotik.Send("=count-only", true);
                var tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/user/print");
                mikrotik.Send("?count-only", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/user/print");
                mikrotik.Send("=count-only=", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/user/print");
                mikrotik.Send("?count-only=", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/user/print");
                mikrotik.Send("=count-only=yes", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/user/print");
                mikrotik.Send("?count-only=yes", true);
                tep = mikrotik.Read();
                foreach (var item in tep)
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }
                    count = Int32.Parse(ColumnList.Any(x => x.Key == "ret") ? (ColumnList.FirstOrDefault(x => x.Key == "ret").Value) : "");
                }
                for (int i = 0; i < count; i++)
                {
                    mikrotik.Send("/tool/user-manager/user/remove");
                    temp = String.Format("=numbers={0}", i);
                    mikrotik.Send(temp, true);
                }
            }
            if (session)
            {
                mikrotik.Send("/tool/user-manager/session/print");
                mikrotik.Send("=count-only", true);
                var tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/session/print");
                mikrotik.Send("?count-only", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/session/print");
                mikrotik.Send("=count-only=", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/session/print");
                mikrotik.Send("?count-only=", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/session/print");
                mikrotik.Send("=count-only=yes", true);
                tep = mikrotik.Read();
                mikrotik.Send("/tool/user-manager/session/print");
                mikrotik.Send("?count-only=yes", true);
                tep = mikrotik.Read();
                foreach (var item in tep)
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }
                    count = Int32.Parse(ColumnList.Any(x => x.Key == "ret") ? (ColumnList.FirstOrDefault(x => x.Key == "ret").Value) : "");
                }
                for (int i = 0; i < count; i++)
                {
                    mikrotik.Send("/tool/user-manager/session/remove");
                    temp = String.Format("=numbers={0}", i);
                    mikrotik.Send(temp, true);
                }
            }
            return true;

        }
        public bool RemoveLogs(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/system/logging/action/add");
            mikrotik.Send("=name=NetotikLog");
            mikrotik.Send("=memory-lines=1");
            mikrotik.Send("=target=memory",true);
            mikrotik.Read();
            mikrotik.Send("/system/logging/print", true);
            var LoggingList = mikrotik.Read();
            foreach(var item in LoggingList)
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }
                    mikrotik.Send("/system/logging/set");
                    var temp = String.Format("=.id={0}", ColumnList.Any(x => x.Key == ".id") ? (ColumnList.FirstOrDefault(x => x.Key == ".id").Value) : "".ToString());
                    mikrotik.Send(temp);
                    mikrotik.Send("=action=NetotikLog", true);
                }
            }
            foreach (var item in LoggingList)
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }
                    var LastAction = ColumnList.Any(x => x.Key == "action") ? (ColumnList.FirstOrDefault(x => x.Key == "action").Value) : "".ToString();
                    mikrotik.Send("/system/logging/set");
                    var temp = String.Format("=.id={0}", ColumnList.Any(x => x.Key == ".id") ? (ColumnList.FirstOrDefault(x => x.Key == ".id").Value) : "".ToString());
                    mikrotik.Send(temp);
                    temp = String.Format("=action={0}", LastAction);
                    mikrotik.Send(temp, true);
                }
            }
            mikrotik.Send("/system/logging/action/remove");
            mikrotik.Send("?name=NetotikLog",true);

            return true;

        }
        public bool ResetRouter(string ip, int port, string user, string pass,bool keepusers,bool nosettings)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            var temp = "";
            mikrotik.Send("/system/backup/save");
            temp = ConvertDate.ToFa(DateTime.Now, "yyyy-MM-dd ").ToString() + " " + ConvertDate.ToFa(DateTime.Now, "T").ToString();
            temp = String.Format("=name=\"Netotik.({0}).RouterReset\"", temp);
            mikrotik.Send(temp);
            mikrotik.Send("dont-encrypt=yes",true);
            mikrotik.Send("/system/reset-configuration");
            if (true)
                mikrotik.Send("keep-users=yes");
            if (nosettings)
                mikrotik.Send("no-defaults=yes");
            mikrotik.Send("yes", true);
            return true;

        }

        public bool BackupRouter(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            var temp = "";
            mikrotik.Send("/system/backup/save");
            temp = ConvertDate.ToFa(DateTime.Now, "yyyy-MM-dd ").ToString() + " "+ ConvertDate.ToFa(DateTime.Now, "T").ToString();
            temp = String.Format("=name=\"Netotik.({0}).Router\"", temp);
            mikrotik.Send(temp);
            mikrotik.Send("dont-encrypt=yes", true);
            return true;

        }
        public bool RebootRouter(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            
            mikrotik.Send("/system/reboot");
            mikrotik.Send("yes",true);
            return true;

        }
        public void RestoreRouter(string r_Host, int r_Port, string r_User, string r_Password, string FileName)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(r_Host, r_Port);
            if (!mikrotik.Login(r_User, r_Password)) mikrotik.Close();
            //-----------------------------------------------
            var temp = "";
            mikrotik.Send("/system/backup/load");
            temp = String.Format("=name='{0}'", FileName);
            mikrotik.Send(temp,true);
        }
        public List<Router_FileModel> GetBackupRouterList(string r_Host, int r_Port, string r_User, string r_Password)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(r_Host, r_Port);
            if (!mikrotik.Login(r_User, r_Password)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/file/print", true);
            var Router_File = new List<Router_FileModel>();
            var list = mikrotik.Read();
            foreach (var item in list)
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < 9; i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }
                    if ((ColumnList.Any(x => x.Key == "type") ? (ColumnList.FirstOrDefault(x => x.Key == "type").Value) : "")== "backup")
                    {
                        Router_File.Add(new Router_FileModel()
                        {
                            CreateTime = ColumnList.Any(x => x.Key == "creation-time") ? (ColumnList.FirstOrDefault(x => x.Key == "creation-time").Value) : "",
                            Name = ColumnList.Any(x => x.Key == "name") ? (ColumnList.FirstOrDefault(x => x.Key == "name").Value) : "",
                            Size = ColumnList.Any(x => x.Key == "size") ? (ColumnList.FirstOrDefault(x => x.Key == "size").Value) : "",
                            Type = ColumnList.Any(x => x.Key == "type") ? (ColumnList.FirstOrDefault(x => x.Key == "type").Value) : ""
                        });
                    }

                }
            }
            return Router_File;
        }
        public bool IP_Port_Check(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            try
            {
                mikrotik.MK(ip, port);
                return true;
            }
            catch { return false; }
        }
        public bool User_Pass_Check(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            try
            {
                if (!mikrotik.Login(user, pass))
                {
                    mikrotik.Close();
                    return false;
                }
                return true;
            }
            catch { return false; }

        }
        public bool CheckMeliCode(string PersonCode, int? id)
        {
            if (PersonCode.Length == 10)
            {
                if (PersonCode == "1111111111" ||
                    PersonCode == "0000000000" ||
                    PersonCode == "2222222222" ||
                    PersonCode == "3333333333" ||
                    PersonCode == "4444444444" ||
                    PersonCode == "5555555555" ||
                    PersonCode == "6666666666" ||
                    PersonCode == "7777777777" ||
                    PersonCode == "8888888888" ||
                    PersonCode == "9999999999" ||
                    PersonCode == "0123456789"
                    )
                {
                    //Response.Write("كد ملي صحيح نمي باشد");          
                    return false;
                }

                int c = Convert.ToInt16(PersonCode.Substring(9, 1));

                int n = Convert.ToInt16(PersonCode.Substring(0, 1)) * 10 +
                     Convert.ToInt16(PersonCode.Substring(1, 1)) * 9 +
                     Convert.ToInt16(PersonCode.Substring(2, 1)) * 8 +
                     Convert.ToInt16(PersonCode.Substring(3, 1)) * 7 +
                     Convert.ToInt16(PersonCode.Substring(4, 1)) * 6 +
                     Convert.ToInt16(PersonCode.Substring(5, 1)) * 5 +
                     Convert.ToInt16(PersonCode.Substring(6, 1)) * 4 +
                     Convert.ToInt16(PersonCode.Substring(7, 1)) * 3 +
                     Convert.ToInt16(PersonCode.Substring(8, 1)) * 2;
                int r = n - (n / 11) * 11;
                if ((r == 0 && r == c) || (r == 1 && c == 1) || (r > 1 && c == 11 - r))
                {
                    //Response.Write(" کد ملی صحیح است");                
                    return true;
                }
                else
                {
                    //Response.Write("كد ملي صحيح نمي باشد");         
                    return false;
                }
            }
            else
            {
                //Response.Write("طول کد ملی وارد شده باید 10 کاراکتر باشد");           
                return false;
            }
        }

        #endregion

    }
}

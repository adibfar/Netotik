using Netotik.Services.Abstract;
using Netotik.Common.MikrotikAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.ViewModels.Mikrotik;

namespace Netotik.Services.Implement
{
    public class MikrotikServices : IMikrotikServices
    {

        public string GetRouterName(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            mikrotik.Send("/system/identity/getall", true);
            return (mikrotik.Read())[0].Split('=')[2].Replace('!', ' ');
        }

        #region userman
        public List<UsermanProfile> ProfileToDic(List<string> resault)
        {
            var profilemodel = new List<UsermanProfile>();
            foreach (var item in resault)
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }

                    profilemodel.Add(new UsermanProfile()
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
        public List<UsermanProfile> GetAllProfile(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/profile/print", true);

            return ProfileToDic(mikrotik.Read());
        }
        public List<UsermanagerCustomer> GetAllCustomers(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/customer/print", true);

            return CustomerToDic(mikrotik.Read());
        }
        public List<UsermanagerCustomer> CustomerToDic(List<string> resault)
        {
            var customermodel = new List<UsermanagerCustomer>();
            foreach (var item in resault)
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }

                    customermodel.Add(new UsermanagerCustomer()
                    {
                        id = ColumnList.Any(x => x.Key == ".id") ? (ColumnList.FirstOrDefault(x => x.Key == ".id").Value) : "",
                        login = ColumnList.Any(x => x.Key == "login") ? (ColumnList.FirstOrDefault(x => x.Key == "login").Value) : "",
                    });
                }
            }
            return customermodel;
        }
        public List<UsermanProfileLimition> GetAllProfileLimition(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/profile/profile-limitation/print", true);

            return ProfileLimitationToDic(mikrotik.Read());
        }
        public List<UsermanLimition> GetAllLimition(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/profile/limitation/print", true);

            return LimitionToDic(mikrotik.Read());
        }
        public List<UsermanProfileLimition> ProfileLimitationToDic(List<string> resault)
        {
            var profilemodel = new List<UsermanProfileLimition>();
            foreach (var item in resault)
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }

                    profilemodel.Add(new UsermanProfileLimition()
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
        public List<UsermanLimition> LimitionToDic(List<string> resault)
        {
            var profilemodel = new List<UsermanLimition>();
            foreach (var item in resault)
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }

                    profilemodel.Add(new UsermanLimition()
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
        public List<UsermanagerUser> UserToDic(List<string> resault)
        {
            var usermodel = new List<UsermanagerUser>();
            foreach (var item in resault)
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }

                    usermodel.Add(new UsermanagerUser()
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
        public Boolean IsUsermangerInstall(string ip, int port, string user, string pass)
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
        public List<UsermanagerUser> GetAllUserManagerUsers(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/user/print", true);

            return UserToDic(mikrotik.Read());
        }
        public void DisableUsermanUser(string ip, int port, string user, string pass, string id)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/user/disable");
            string temp = String.Format("=.id={0}", id);
            mikrotik.Send(temp, true);
        }
        public void EnableUsermanUser(string ip, int port, string user, string pass, string id)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/user/enable");
            mikrotik.Send(String.Format("=.id={0}", id), true);
        }
        public void RemoveUsermanUser(string ip, int port, string user, string pass, string id)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/user/remove");
            string temp = String.Format("=.id={0}", id);
            mikrotik.Send(temp, true);
        }
        public void RemoveUsermanprofile(string ip, int port, string user, string pass, string id)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            var temp = GetAllProfile(ip, port, user, pass);
            foreach (var item in temp)
            {
                if (item.id == id)
                {
                    foreach (var item2 in GetAllLimition(ip, port, user, pass))
                    {
                        if (item2.name == item.name)
                        {
                            mikrotik.Send("/tool/user-manager/profile/limitation/remove");
                            string temp3 = String.Format("=.id={0}", item2.id);
                            mikrotik.Send(temp3, true);
                        }
                    }
                    foreach (var item2 in GetAllProfileLimition(ip, port, user, pass))
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

        public bool IsUsermanUserExist(string ip, int port, string user, string pass, string username)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-------------------------------------------------
            foreach (var item in GetAllUserManagerUsers(ip, port, user, pass))
            {
                if (item.username == username) return true;
            }
            return false;
        }

        public void UsermanUserCreate(string ip, int port, string user, string pass, UsermanagerUserRegister usermanuser)
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
            foreach (var item in GetAllUserManagerUsers(ip, port, user, pass))
                if (item.username == usermanuser.username)
                    temp = String.Format("=.id={0}", item.id);
            mikrotik.Send("/tool/user-manager/user/create-and-activate-profile");
            mikrotik.Send(temp);
            temp = String.Format("=customer={0}", usermanuser.customer);
            mikrotik.Send(temp);
            temp = String.Format("=profile={0}", usermanuser.profile);
            mikrotik.Send(temp, true);
        }
        public void UsermanUserEdit(string ip, int port, string user, string pass, UsermanagerUserEdit model)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/user/set");
            string temp = String.Format("=.id={0}", model.username);
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
            mikrotik.Send(temp,true);
            if (model.profile != "")
            {
                mikrotik.Send("/tool/user-manager/user/create-and-activate-profile");
                temp = String.Format("=.id={0}", model.id);
                mikrotik.Send(temp);
                temp = String.Format("=customer={0}", model.customer);
                mikrotik.Send(temp);
                temp = String.Format("=profile={0}", model.profile);
                mikrotik.Send(temp,true);
            }
        }
        public List<UsermanagerUserSession> UsermanUserSession(string ip, int port, string user, string pass, string UsermanUser)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/session/print");
            //  mikrotik.Send("where");
            string temp = String.Format("?=user={0}", UsermanUser);
            mikrotik.Send(temp, true);

            var usersessionmodel = new List<UsermanagerUserSession>();
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
                    usersessionmodel.Add(new UsermanagerUserSession()
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
            var usersessionmodelresualt = new List<UsermanagerUserSession>();
            foreach (var item in usersessionmodel)
            {
                if (item.user == UsermanUser)
                {
                    usersessionmodelresualt.Add(item);
                }
            }
            return usersessionmodelresualt;
        }

        public void UsermanProfileCreate(string ip, int port, string user, string pass, UsermanProfileLimitionCreate usermanProfile)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-------------------------------------------------
            mikrotik.Send("/tool/user-manager/profile/add");
            string temp = String.Format("=price={0}", usermanProfile.profile_price);
            if (usermanProfile.profile_price == null)
                temp = String.Format("=price={0}", usermanProfile.profile_price);
            mikrotik.Send(temp);
            temp = String.Format("=validity={0}", usermanProfile.profile_validity);
            if (usermanProfile.profile_validity != null)
                mikrotik.Send(temp);
            temp = String.Format("=starts-at={0}", usermanProfile.profile_starts_at);
            if (usermanProfile.profile_starts_at == null)
                temp = String.Format("=starts-at={0}", usermanProfile.profile_starts_at);
            mikrotik.Send(temp);
            temp = String.Format("=name-for-users={0}", usermanProfile.profile_name_for_users);
            if (usermanProfile.profile_name_for_users == null)
                temp = String.Format("=name-for-users={0}", usermanProfile.profile_name);
            mikrotik.Send(temp);
            temp = String.Format("=override-shared-users={0}", usermanProfile.profile_override_shared_users);
            if (usermanProfile.profile_override_shared_users == null)
                temp = String.Format("=override-shared-users={0}", usermanProfile.profile_override_shared_users);
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
            if (usermanProfile.limition_address_list != null)
                mikrotik.Send(temp);
            temp = String.Format("=group-name={0}", usermanProfile.limition_group_name);
            if (usermanProfile.limition_group_name != null)
                mikrotik.Send(temp);
            temp = String.Format("=ip-pool={0}", usermanProfile.limition_ip_pool);
            if (usermanProfile.limition_ip_pool != null)
                mikrotik.Send(temp);
            temp = String.Format("=rate-limit-rx={0}", usermanProfile.limition_rate_limit_rx);
            if (usermanProfile.limition_rate_limit_rx != null)
                mikrotik.Send(temp);
            temp = String.Format("=rate-limit-tx={0}", usermanProfile.limition_rate_limit_tx);
            if (usermanProfile.limition_rate_limit_tx != null)
                mikrotik.Send(temp);
            temp = String.Format("=transfer-limit={0}", usermanProfile.limition_transfer_limit);
            if (usermanProfile.limition_transfer_limit != null)
                mikrotik.Send(temp);
            temp = String.Format("=upload-limit={0}", usermanProfile.limition_upload_limit);
            if (usermanProfile.limition_upload_limit != null)
                mikrotik.Send(temp);
            temp = String.Format("=uptime-limit={0}", usermanProfile.limition_uptime_limit);
            if (usermanProfile.limition_uptime_limit != null)
                mikrotik.Send(temp);
            temp = String.Format("=download-limit={0}", usermanProfile.limition_download_limit);
            if (usermanProfile.limition_download_limit != null)
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
            if (usermanProfile.profilelimition_from_time == null)
                temp = String.Format("=from-time={0}", usermanProfile.profilelimition_from_time);
            mikrotik.Send(temp);
            temp = String.Format("=weekdays={0}", usermanProfile.profilelimition_weekdays);
            if (usermanProfile.profilelimition_weekdays != null)
                mikrotik.Send(temp);
            temp = String.Format("=till-time={0}", usermanProfile.profilelimition_till_time);
            if (usermanProfile.profilelimition_till_time == null)
                temp = String.Format("=till-time={0}", usermanProfile.profilelimition_till_time);
            mikrotik.Send(temp);
            temp = String.Format("=limitation={0}", usermanProfile.profile_name);
            mikrotik.Send(temp);
            temp = String.Format("=profile={0}", usermanProfile.profile_name);
            mikrotik.Send(temp, true);
            var temp64 = mikrotik.Read();



            mikrotik.Send("/tool/user-manager/profile/profile-limitation/add");
            temp = String.Format("=from-time={0}", usermanProfile.profilelimition_from_time);
            if (usermanProfile.profilelimition_from_time == null)
                temp = String.Format("=from-time={0}", usermanProfile.profilelimition_from_time);
            mikrotik.Send(temp);
            temp = String.Format("=weekdays={0}", usermanProfile.profilelimition_weekdays);
            if (usermanProfile.profilelimition_weekdays != null)
                mikrotik.Send(temp);
            temp = String.Format("=till-time={0}", usermanProfile.profilelimition_till_time);
            if (usermanProfile.profilelimition_till_time == null)
                temp = String.Format("=till-time={0}", usermanProfile.profilelimition_till_time);
            mikrotik.Send(temp);
            temp = String.Format("=limitation={0}", usermanProfile.profile_name);
            mikrotik.Send(temp);
            temp = String.Format("=profile={0}", usermanProfile.profile_name);
            mikrotik.Send(temp, true);
            var temp63 = mikrotik.Read();
        }

        public bool IsUsermanProfileExist(string ip, int port, string user, string pass, UsermanProfileLimitionCreate usermanProfile)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-------------------------------------------------
            foreach (var item in GetAllProfile(ip, port, user, pass))
            {
                if (item.name == usermanProfile.profile_name) return true;
            }
            return false;
        }

        #endregion

        public List<RouterInterface> Interface(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/interface/print", true);
            var interfacemodel = new List<RouterInterface>();

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

                    interfacemodel.Add(new RouterInterface()
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

        public void Router_Info_Update(string ip, int port, string user, string pass)
        {
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(ip, port);
            if (!mikrotik.Login(user, pass)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/system/package/update/install",true);
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
            mikrotik.Send("/ip/cloud/force-update",true);
            mikrotik.Send("/ip/cloud/print",true);
            string dns_name="";
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

    }
}

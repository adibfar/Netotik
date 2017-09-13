using Netotik.Common;
using Netotik.Data;
using Netotik.Domain.Entity;
using Netotik.ViewModels.Common.Setting;
using Netotik.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Implement
{
    public class SettingService : ISettingService
    {
        internal IDbSet<Setting> dbSet;
        public SettingService(IUnitOfWork unitOfWork)
        {
            this.dbSet = unitOfWork.Set<Setting>();
        }


        public GeneralSettingModel GetAll()
        {
            var list = dbSet.ToList();

            GeneralSettingModel model = new GeneralSettingModel();

            model.HomePageTitle = list.FirstOrDefault(x => x.Name == "HomePageTitle").Value;
            model.HomePageDescription = list.FirstOrDefault(x => x.Name == "HomePageDescription").Value;
            model.HomePageKeywords = list.FirstOrDefault(x => x.Name == "HomePageKeywords").Value;

            model.SmsPassword = list.FirstOrDefault(x => x.Name == "SmsPassword").Value;
            model.SmsUsername = list.FirstOrDefault(x => x.Name == "SmsUsername").Value;
            model.SmsNumber = list.FirstOrDefault(x => x.Name == "SmsNumber").Value;

            model.Facebook = list.FirstOrDefault(x => x.Name == "Facebook").Value;
            model.Twitter = list.FirstOrDefault(x => x.Name == "Twitter").Value;
            model.Instagram = list.FirstOrDefault(x => x.Name == "Instagram").Value;
            model.GooglePlus = list.FirstOrDefault(x => x.Name == "GooglePlus").Value;

            model.CompanyName = list.FirstOrDefault(x => x.Name == "CompanyName").Value;
            model.CompanyAddress = list.FirstOrDefault(x => x.Name == "CompanyAddress").Value;
            model.CompanyTel1 = list.FirstOrDefault(x => x.Name == "CompanyTel1").Value;
            model.CompanyTel2 = list.FirstOrDefault(x => x.Name == "CompanyTel2").Value;
            model.CompanyEmail1 = list.FirstOrDefault(x => x.Name == "CompanyEmail1").Value;
            model.CompanyEmail2 = list.FirstOrDefault(x => x.Name == "CompanyEmail2").Value;


            return model;
        }

        public void Update(GeneralSettingModel model)
        {
            var list = dbSet.ToList();

            list.First(x => x.Name.Equals("HomePageTitle")).Value = model.HomePageTitle;
            list.First(x => x.Name.Equals("HomePageDescription")).Value = model.HomePageDescription;
            list.First(x => x.Name.Equals("HomePageKeywords")).Value = model.HomePageKeywords;

            list.First(x => x.Name.Equals("SmsUsername")).Value = model.SmsUsername;
            list.First(x => x.Name.Equals("SmsPassword")).Value = model.SmsPassword;
            list.First(x => x.Name.Equals("SmsNumber")).Value = model.SmsNumber;

            list.First(x => x.Name.Equals("Facebook")).Value = model.Facebook;
            list.First(x => x.Name.Equals("Twitter")).Value = model.Twitter;
            list.First(x => x.Name.Equals("Instagram")).Value = model.Instagram;
            list.First(x => x.Name.Equals("GooglePlus")).Value = model.GooglePlus;

            list.First(x => x.Name.Equals("CompanyName")).Value = model.CompanyName;
            list.First(x => x.Name.Equals("CompanyAddress")).Value = model.CompanyAddress;
            list.First(x => x.Name.Equals("CompanyTel1")).Value = model.CompanyTel1;
            list.First(x => x.Name.Equals("CompanyTel2")).Value = model.CompanyTel2;
            list.First(x => x.Name.Equals("CompanyEmail1")).Value = model.CompanyEmail1;
            list.First(x => x.Name.Equals("CompanyEmail2")).Value = model.CompanyEmail2;

        }
    }
}

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

            model.SiteName = list.FirstOrDefault(x => x.Name == "SiteName").Value;

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

            list.First(x => x.Name.Equals("SiteName")).Value = model.SiteName;
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

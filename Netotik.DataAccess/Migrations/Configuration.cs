namespace Netotik.Data.Migrations
{
    using Netotik.Common.Security;
    using Netotik.Domain.Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Netotik.Data.Context.NetotikDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Netotik.Data.Context.NetotikDBContext context)
        {
            #region Site Setting
            context.Settings.AddOrUpdate(op => new { op.Name },
                 new Setting { Name = "SmsUsername", Value = "" },
                 new Setting { Name = "SmsPassword", Value = "" },
                 new Setting { Name = "SmsNumber", Value = "" },

                 new Setting { Name = "HomePageTitle", Value = "نتوتیک" },
                 new Setting { Name = "HomePageDescription", Value = "نتوتیک، سامانه مدیریت هوشمند میکروتیک" },
                 new Setting { Name = "HomePageKeywords", Value = "" },

                new Setting { Name = "Facebook", Value = "" },
                new Setting { Name = "Twitter", Value = "" },
                new Setting { Name = "GooglePlus", Value = "" },
                new Setting { Name = "Instagram", Value = "" },
                  new Setting { Name = "LinkedIn", Value = "" },

                new Setting { Name = "CompanyName", Value = "NETOTIK.COM" },
                new Setting { Name = "CompanyAddress", Value = "" },
                new Setting { Name = "CompanyTel1", Value = "09174077833" },
                new Setting { Name = "CompanyTel2", Value = "09366882388" },
                new Setting { Name = "CompanyEmail1", Value = "" },
                new Setting { Name = "CompanyEmail2", Value = "" }
                );
            #endregion

            context.SaveChanges();
        }
    }
}

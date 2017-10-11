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
                 new Setting { Name = "SmsUsername", Value = "adibfar" },
                 new Setting { Name = "SmsPassword", Value = "3295" },
                 new Setting { Name = "SmsNumber", Value = "50001000150001" },

                 new Setting { Name = "HomePageTitle", Value = "" },
                 new Setting { Name = "HomePageDescription", Value = "" },
                 new Setting { Name = "HomePageKeywords", Value = "" },

                new Setting { Name = "Facebook", Value = "" },
                new Setting { Name = "Twitter", Value = "" },
                new Setting { Name = "GooglePlus", Value = "" },
                new Setting { Name = "Instagram", Value = "" },

                new Setting { Name = "CompanyName", Value = "" },
                new Setting { Name = "CompanyAddress", Value = "" },
                new Setting { Name = "CompanyTel1", Value = "" },
                new Setting { Name = "CompanyTel2", Value = "" },
                new Setting { Name = "CompanyEmail1", Value = "" },
                new Setting { Name = "CompanyEmail2", Value = "" }
                );
            #endregion

            context.SaveChanges();
        }
    }
}

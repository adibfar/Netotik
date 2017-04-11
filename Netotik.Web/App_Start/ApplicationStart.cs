using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using CaptchaMvc.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using Netotik.IocConfig;
using Karenbic.IocConfig;
using Netotik.Data.Migrations;
using Netotik.Data.Context;
using Netotik.Services.Contracts;

namespace Netotik.Web
{
    public static class ApplicationStart
    {
        public static void Config()
        {
            // disable response header for protection  attak
            MvcHandler.DisableMvcResponseHeader = true;

            // change captcha provider for using cookie
            CaptchaUtils.CaptchaManager.StorageProvider = new CookieStorageProvider();

            //Set current Controller factory as StructureMapControllerFactory
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());

            //set current Filter factory as StructureMapFitlerProvider
            var filterProider = FilterProviders.Providers.Single(p => p is FilterAttributeFilterProvider);
            FilterProviders.Providers.Remove(filterProider);
            FilterProviders.Providers.Add(ProjectObjectFactory.Container.GetInstance<StructureMapFilterProvider>());

            //Database.SetInitializer<ApplicationDbContext>(null);
            Database.SetInitializer<NetotikDBContext>(new MigrateDatabaseToLatestVersion<NetotikDBContext, Configuration>());
            //           ProjectObjectFactory.Container.GetInstance<IUnitOfWork>().ForceDatabaseInitialize();

            var defaultJsonFactory = ValueProviderFactories.Factories
                .OfType<JsonValueProviderFactory>().FirstOrDefault();
            var index = ValueProviderFactories.Factories.IndexOf(defaultJsonFactory);
            ValueProviderFactories.Factories.Remove(defaultJsonFactory);
            //ValueProviderFactories.Factories.Insert(index, new JsonNetValueProviderFactory());


            //ad interception for logg EF errors
            //DbInterception.Add(new ElmahEfInterceptor());

            foreach (var task in ProjectObjectFactory.Container.GetAllInstances<IRunAtInit>())
            {
                task.Execute();
            }

            // Microsoft.AspNet.SignalR.GlobalHost.DependencyResolver = ProjectObjectFactory.Container.GetInstance<Microsoft.AspNet.SignalR.IDependencyResolver>();
        }

           
    }
}
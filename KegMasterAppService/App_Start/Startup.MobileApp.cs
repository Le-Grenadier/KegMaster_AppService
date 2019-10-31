using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using KegMasterAppService.DataObjects;
using KegMasterAppService.Models;
using Owin;

namespace KegMasterAppService
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new KegMasterAppInitializer());

            // To prevent Entity Framework from modifying your database schema, use a null database initializer
            // Database.SetInitializer<KegMasterAppContext>(null);

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                // This middleware is intended to be used locally for debugging. By default, HostName will
                // only have a value when running in an App Service application.
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }
            app.UseWebApi(config);
        }
    }

    public class KegMasterAppInitializer : CreateDatabaseIfNotExists<KegMasterAppContext>
    {
        protected override void Seed(KegMasterAppContext context)
        {
            KegItem KegSeed = new KegItem { Id = Guid.NewGuid().ToString(),
                                            Alerts = "",
                                            Name = "My Keg",
                                            Description = "Best Keg Ever",
                                            DateKegged = new DateTime(1516, 04, 23),
                                            DateAvail = new DateTime(1516, 04, 23),
                                            PourEn = true,
                                            PourNotification = false,
                                            PourQtyGlass = 0.0f,
                                            PourQtySample = 0.0f,
                                            PressureCrnt = 0.0f,
                                            PressureDsrd = 0.0f,
                                            PressureDwellTime = 0.0f,
                                            PressureEn = true,
                                            QtyAvailable = 0.0f,
                                            QtyReserve = 0.0f
                                            };
            
            context.Set<KegItem>().Add(KegSeed);

            base.Seed(context);
        }
    }
}


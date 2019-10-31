namespace KegMasterAppService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using System.Linq;

    using KegMasterAppService.DataObjects;

    internal sealed class Configuration : DbMigrationsConfiguration<KegMasterAppService.Models.KegMasterAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(KegMasterAppService.Models.KegMasterAppContext context)
        {
            //  This method will be called after migrating to the latest version.
            KegItem[] KegSeed = {
                new KegItem() {
                    Id = Guid.NewGuid().ToString(),
                    Alerts = "",
                    TapNo = 0,
                    Name = "My Keg",
                    Style = "Homebrew",
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
                },
            };

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Set<KegItem>().AddOrUpdate(KegSeed);
        }
    }
}

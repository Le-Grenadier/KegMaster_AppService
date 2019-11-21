using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using System.Collections.Generic;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.Mobile.Server;

using KegMasterAppService.DataObjects;
using KegMasterAppService.Models;
using System.Dynamic;

namespace KegMasterAppService.Controllers
{
    public class KegItemController : TableController<KegItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            KegMasterAppContext context = new KegMasterAppContext();
            DomainManager = new EntityDomainManager<KegItem>(context, Request);
        }

        // GET tables/KegItem
        public IQueryable<KegItem> GetAllKegItems()
        {
            return Query();
        }

        // GET tables/KegItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<KegItem> GetKegItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/KegItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<KegItem> PatchKegItem(string id, Delta<KegItem> patch)
        {
            // Loosly following tutorial @ https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-xamarin-forms-get-started-push
            PushNotification_proc(id, patch);

            return UpdateAsync(id, patch);
        }

        // POST tables/KegItem
        public async Task<IHttpActionResult> PostKegItem(KegItem item)
        {
            KegItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/KegItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteKegItem(string id)
        {
            return DeleteAsync(id);
        }

        private async Task PushNotification_proc(string id, Delta<KegItem> patch)
        {
            // Get the settings for the server project.
            HttpConfiguration config = this.Configuration;
            MobileAppSettingsDictionary settings =
                this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();

            // Get the Notification Hubs credentials for the mobile app.
            string notificationHubName = settings.NotificationHubName;
            string notificationHubConnection = settings
                .Connections[MobileAppSettingsKeys.NotificationHubConnectionString].ConnectionString;

            // Create a new Notification Hub client.
            NotificationHubClient hub = NotificationHubClient
            .CreateClientFromConnectionString(notificationHubConnection, notificationHubName);

            // Send the message so that all template registrations that contain "messageParam"
            // receive the notifications. This includes APNS, GCM, WNS, and MPNS template registrations.
            Dictionary<string, string> templateParams = new Dictionary<string, string>();
            string alerts;
            if( patch.GetChangedPropertyNames().Contains("Alerts"))
            {
                KegItem k = new KegItem();
                patch.CopyChangedValues(k);
                templateParams["messageParam"] = k.Alerts;

                try
                {
                    // Send the push notification and log the results.
                    var result = await hub.SendTemplateNotificationAsync(templateParams);

                    // Write the success result to the logs.
                    config.Services.GetTraceWriter().Info(result.State.ToString());
                }
                catch (System.Exception ex)
                {
                    // Write the failure result to the logs.
                    config.Services.GetTraceWriter()
                        .Error(ex.Message, null, "Push.SendAsync Error");
                }
            }



            return;
        }
    }
}
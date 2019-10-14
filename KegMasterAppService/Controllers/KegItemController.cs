using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using KegMasterAppService.DataObjects;
using KegMasterAppService.Models;

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
    }
}
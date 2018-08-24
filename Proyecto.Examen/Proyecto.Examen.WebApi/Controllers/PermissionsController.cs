using Proyecto.Examen.WebApi._Owin;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Proyecto.Examen.WebApi.Controllers
{
    [RoutePrefix("api/permisos")]
    public class PermissionsController : ApiController
    {
        [Route(""), HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var context = Request.GetOwinContext().Get<UserDBContext>();
            var items = await context.Permissions.ToListAsync();
            return Ok(new { items });
        }
    }
}

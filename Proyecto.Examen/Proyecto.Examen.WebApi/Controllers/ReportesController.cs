using Microsoft.AspNet.Identity;
using Proyecto.Examen.WebApi._Models;
using Proyecto.Examen.WebApi._Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Proyecto.Examen.WebApi.Controllers
{
    [RoutePrefix("api/reportes")]
    public class ReportesController : BaseController
    {
        /// <summary>
        /// Uri que retorna los inicio de session dependiendo el rol.
        /// </summary>
        /// <returns></returns>
        [Route(""),HttpGet]
        public IHttpActionResult Get()
        {
            var principal = RequestContext.Principal as ClaimsPrincipal;
            var userId = principal.Identity.GetUserId();
            using (var login=new LoginProvider())
            {
                return Ok(new { items=login.Get(principal.IsInRole("Master") ? userId : null) });
            }

        }

    }
}

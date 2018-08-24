using Microsoft.AspNet.Identity.Owin;
using Proyecto.Examen.WebApi._Owin;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Proyecto.Examen.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        private RoleManager _AppRoleManager = null;
        private UserManager _AppUserManager = null;

        /// <summary>
        /// Retorna una instancia de Administrador de usuarios.
        /// </summary>
        protected UserManager UserManager
        {
            get
            {
                try
                {
                    return _AppUserManager ?? Request.GetOwinContext().GetUserManager<UserManager>();

                }
                catch (Exception e )
                {

                    throw;
                }
            }
        }
        /// <summary>
        /// Retorna una instancia de Administrador de Roles.
        /// </summary>
        protected RoleManager AppRoleManager
        {
            get
            {
                return _AppRoleManager ?? Request.GetOwinContext().GetUserManager<RoleManager>();
            }
        }
    }
}

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Proyecto.Examen.WebApi._Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Examen.WebApi._Owin
{
    public class RoleManager : RoleManager<Role>
    {
        public RoleManager(IRoleStore<Role, string> store) : base(store)
        {
        }
        public static RoleManager Create(IdentityFactoryOptions<RoleManager> options, IOwinContext context)
        {
            var appRoleManager = new RoleManager(new RoleStore<Role>(context.Get<UserDBContext>()));
            return appRoleManager;
        }
    }
}
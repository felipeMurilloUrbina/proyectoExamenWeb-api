using Microsoft.AspNet.Identity.EntityFramework;
using Proyecto.Examen.WebApi._Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Examen.WebApi._Owin
{
    public class UserDBContext: IdentityDbContext<User>
    {
        public UserDBContext()
            : base("IdentityConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        //public DbSet<Conexion> Conexion { get; set; }
        public static UserDBContext Create()
        {
            return new UserDBContext();
        }
    }
}
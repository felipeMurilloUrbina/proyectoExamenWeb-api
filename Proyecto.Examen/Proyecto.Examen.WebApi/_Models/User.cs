using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Proyecto.Examen.WebApi._Models
{
    public class User: IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public Boolean Active { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            //var usuario = manager.Users.Include(u => u.Conexiones).FirstOrDefault(u => u.Id == this.Id);
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
}
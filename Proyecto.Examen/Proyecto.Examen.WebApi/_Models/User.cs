using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Examen.WebApi._Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public Boolean Active { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{this.Name} {this.LastName}";
            }
        }
        [NotMapped]
        public Role Rol { get; set; }
        [NotMapped]
        public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            //var usuario = manager.Users.Include(u => u.Conexiones).FirstOrDefault(u => u.Id == this.Id);
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
}
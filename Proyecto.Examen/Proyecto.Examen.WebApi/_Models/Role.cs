using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Examen.WebApi._Models
{
    /// <summary>
    /// Roles para los usuarios Hereda de identity.
    /// </summary>
    public class Role: IdentityRole
    {
        public Role() : base() {
            this.Permissions = new HashSet<Permission>();
        }
        public Role(string name, string description) : base(name)
        {
            this.Description = description;
            this.Permissions = new HashSet<Permission>();

        }
        public virtual string Description { get; set; }
        public bool Active { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}
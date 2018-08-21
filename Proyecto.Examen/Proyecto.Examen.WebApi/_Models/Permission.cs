using Proyecto.Examen.WebApi._Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Examen.WebApi._Models
{
    public class Permission: IObject
    {
        public Permission()
        {
            this.Roles = new HashSet<Role>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }

        public ICollection<Role> Roles{ get; set; }
    }
}
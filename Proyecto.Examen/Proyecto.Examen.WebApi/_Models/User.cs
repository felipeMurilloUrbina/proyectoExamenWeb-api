using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Examen.WebApi._Models
{
    public class User: IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public Boolean Active { get; set; }
    }
}
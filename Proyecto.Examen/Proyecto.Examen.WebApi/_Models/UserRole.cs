using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Proyecto.Examen.WebApi._Models
{
    public class UserRole: IdentityUserRole<int>
    {
        public virtual IRole<int> Role { get; set; }
    }
}
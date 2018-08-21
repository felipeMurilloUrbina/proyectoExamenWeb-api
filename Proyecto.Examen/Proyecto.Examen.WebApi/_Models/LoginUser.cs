using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Examen.WebApi._Models
{
    public class LoginUser
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string AppId { get; set; }
        public DateTime Date { get; set; }
    }
}
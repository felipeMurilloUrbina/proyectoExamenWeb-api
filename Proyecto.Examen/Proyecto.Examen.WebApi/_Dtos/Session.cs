using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Examen.WebApi._Dtos
{
    public class Session
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }
}
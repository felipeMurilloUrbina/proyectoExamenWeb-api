using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Examen.WebApi._Dtos
{
    public class UserDTO
    {
        [Required(ErrorMessage ="El nombre es requerido.")]
        public string Name { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "El email es requerido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Rol { get; set; }
        public string Avatar { get; set; }
        [Required(ErrorMessage = "El password es requerido.")]
        public string Password { get; set; }
    }
}
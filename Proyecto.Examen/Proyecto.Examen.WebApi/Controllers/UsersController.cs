using AutoMapper;
using Microsoft.AspNet.Identity;
using Proyecto.Examen.WebApi._Attributes;
using Proyecto.Examen.WebApi._Dtos;
using Proyecto.Examen.WebApi._Models;
using Proyecto.Examen.WebApi.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Proyecto.Examen.WebApi.Controllers
{
    [RoutePrefix("api/usuarios") ]
    public class UsersController : BaseController
    {
        [CustomAuthorize(Role = "Normal"), Route("{username}"), HttpGet]
        public IHttpActionResult GetUserByUserName(string username)
        {
            var user =this.UserManager.FindByName(username);
            if (user == null)
                return BadRequest(Resources.NoFound);
            return Ok(user);
        }
        [CustomAuthorize(Role = "Normal"), Route("{id}"), HttpGet]
        public IHttpActionResult GetUserById(string id)
        {
            var user = this.UserManager.FindById(id);
            if (user == null)
                return BadRequest(Resources.NoFound);
            return Ok(user);
        }
        /// <summary>
        /// Uri para crear un nuevo usuario normal.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route(""), HttpPost]
        public async Task<IHttpActionResult> Create(UserDTO user) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userDB = Mapper.Map<UserDTO, User>(user);
            userDB.Active = true;
            userDB.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole() { RoleId = "ABC" });
            IdentityResult addUserResult = await this.UserManager.CreateAsync(userDB, user.Password);
            if (!addUserResult.Succeeded)
            {
                foreach(var error in addUserResult.Errors)
                {
                    return BadRequest(error);
                }
            }
                return Ok(Resources.SaveOk);
        }
    }
}

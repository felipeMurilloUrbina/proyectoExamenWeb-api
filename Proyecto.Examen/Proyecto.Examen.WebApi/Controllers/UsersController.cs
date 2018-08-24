﻿using AutoMapper;
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
using System.Data.Entity;
using System.IO;
using System.Web;
using Proyecto.Examen.WebApi._Providers;
using System.Security.Claims;

namespace Proyecto.Examen.WebApi.Controllers
{
    [RoutePrefix("api/usuarios") ]
    public class UsersController : BaseController
    {
        /// <summary>
        /// Uri para obtener perfil por nombre de usuario.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        //[CustomAuthorize(Role = "Normal"), Route("{username}"), HttpGet]
        //public IHttpActionResult GetUserByUserName(string username)
        //{
        //    var user =this.UserManager.FindByName(username);
        //    if (user == null)
        //        return BadRequest(Resources.NotFound);
        //    return Ok(user);
        //}
        /// <summary>
        /// Uri para obtener perfil por id de usuario.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CustomAuthorize(Role = "Normal"), Route("{id}"), HttpGet]
        public IHttpActionResult GetUserById(string id)
        {
            var user = this.UserManager.Users.Include(u=>u.Roles).FirstOrDefault(u=>u.Id==id);
            var roleId = user.Roles.FirstOrDefault().RoleId;
            //var roles = this.AppRoleManager.Roles.Include("Permissions").FirstOrDefault(r=>r.Id==user.Roles.FirstOrDefault().RoleId);
            user.Rol = this.AppRoleManager.Roles.Include("Permissions").FirstOrDefault(r => r.Id == roleId);

            if (user == null)
                return BadRequest(Resources.NotFound);
            return Ok(new { profile = user });
        }
        /// <summary>
        /// Uri para obtener lista de usuarios.
        /// </summary>
        /// <returns></returns>
        [Route(""), HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var users = this.UserManager.Users.Include(u=>u.Roles).ToList();
            if (users.Count == 0)
                return BadRequest(Resources.NotFound);

            return Ok(new { items=Mapper.Map<List<User>, List<UserDTO>>(users) });
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
            userDB.Avatar = "avatars/default.png";
            userDB.Avatar = "data:image/png;base64,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";
            //userDB.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole() { RoleId = "ABC" });
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
        [Route("avatar"), HttpPost]
        public IHttpActionResult UploadAvatar(Avatar avatar)
        {
            var principal = RequestContext.Principal as ClaimsPrincipal;
            var user = this.UserManager.FindById(principal.Identity.GetUserId());
            user.Avatar = avatar.Image64;
            this.UserManager.Update(user);
            return Ok(Resources.AvatarOk);
        }
    }
}

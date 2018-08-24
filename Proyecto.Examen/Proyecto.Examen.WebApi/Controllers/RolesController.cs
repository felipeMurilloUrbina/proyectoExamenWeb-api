using Microsoft.AspNet.Identity.Owin;
using Proyecto.Examen.WebApi._Attributes;
using Proyecto.Examen.WebApi._Dtos;
using Proyecto.Examen.WebApi._Owin;
using System.Web.Http;
using System.Net.Http;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using Proyecto.Examen.WebApi._Models;
using System;

namespace Proyecto.Examen.WebApi.Controllers
{
    [RoutePrefix("api/roles"), CustomAuthorize(Role ="Administrador")]
    public class RolesController : BaseController
    {
        /// <summary>
        /// Uri que retorna todos los roles activos.
        /// </summary>
        /// <returns></returns>
        [Route(""), HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var context = Request.GetOwinContext().Get<UserDBContext>();
            var items = await context.Roles.ToListAsync();
            return Ok(new { items});
        }
        /// <summary>
        /// Uri que retorna un role con un id especifico.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}"), HttpGet]
        public IHttpActionResult GetId(string id)
        {
            return Ok();

        }
        /// <summary>
        /// Uri que guarda un rol.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [Route(""), HttpPost]
        public IHttpActionResult Post(RoleDTO role)
        {
            using (var context = Request.GetOwinContext().Get<UserDBContext>())
            {
                var roleDB = Mapper.Map<RoleDTO, Role>(role);
                roleDB.Id = Guid.NewGuid().ToString();
                string builkQuery = "";
                foreach(var permission in roleDB.Permissions)
                {
                    builkQuery += $"INSERT INTO RolePermissions(RoleId, PermissionId) VALUES ('{roleDB.Id}', {permission.Id});";
                }
                context.Roles.Add(roleDB);
                
                try
                {
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand($"delete from RolePermissions where RoleId='{roleDB.Id}'");
                    context.Database.ExecuteSqlCommand(builkQuery);
                }
                catch (System.Exception e )
                {
                }
            }
            return Ok();

        }
        /// <summary>
        /// Uri que actualiza un rol en especifico.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [Route("{id}"), HttpPut]
        public IHttpActionResult Put(int id, RoleDTO role)
        {
            return Ok();
        }
        /// <summary>
        /// Uri que elimina un rol en especifico.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}"), HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            return Ok();
        }

    }
}

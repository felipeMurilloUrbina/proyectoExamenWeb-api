using Proyecto.Examen.WebApi._Attributes;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using Proyecto.Examen.WebApi._Dtos;

namespace Proyecto.Examen.WebApi.Controllers
{
    [RoutePrefix("api/roles"), CustomAuthorize(Role ="Administrador")]
    public class RolesController : ApiController
    {
        /// <summary>
        /// Uri que retorna todos los roles activos.
        /// </summary>
        /// <returns></returns>
        [Route(""), HttpGet]
        public IHttpActionResult Get()
        {
            return Ok();
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

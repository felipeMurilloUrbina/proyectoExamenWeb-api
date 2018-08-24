using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Proyecto.Examen.WebApi._Models;
using Proyecto.Examen.WebApi._Owin;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Proyecto.Examen.WebApi._Providers
{
    public class CustomOAuthProvider: OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            string symmetricKeyAsBase64 = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }

            //var audience = AudiencesStore.FindAudience(context.ClientId);

            if(context.ClientId != Startup.clave)
            {
                context.SetError("App No registrada", string.Format("Clave para la app no valida '{0}'", context.ClientId));
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var userManager = context.OwinContext.GetUserManager<UserManager>();
            User user = null;
            if (context.UserName.Contains("@"))
                user = await userManager.FindByEmailAsync(context.UserName);
            if (user == null)
                user = await userManager.FindAsync(context.UserName, context.Password);
            else
                user = await userManager.FindAsync(user.UserName, context.Password);
            if (user == null)
            {
                context.SetError("incorrecto", "El usuario o contraseña son incorrectos");
                return;
            }

            if (!user.EmailConfirmed)
            {
                context.SetError("incorrecto", "Usuario no registrado.");
                return;
            }
            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "audience", (context.ClientId == null) ? string.Empty : context.ClientId
                    }
                });
            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, "JWT");
            LoginProvider login = new LoginProvider();
            login.Insert(new LoginUser()
            {
                AppId = context.ClientId,
                Date = DateTime.Now,
                UserId = user.Id,
                Id = Guid.NewGuid().ToString()
            });
            try
            {
                userManager.AddLoginAsync(user.Id, new UserLoginInfo("App Web", context.ClientId) { });
            }
            catch
            {
            }
            var ticket = new AuthenticationTicket(oAuthIdentity, props);
            context.Validated(ticket);
        }
    }
}